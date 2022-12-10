using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using InformaticsCertificationExamSystem.Services;
using System.Security.Claims;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly ICSVService _csvService;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper, ICSVService csvService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _csvService = csvService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudent()
        {
            var AllStudent = _unitOfWork.StudentRepository.GetAll().ToList();
            return Ok(AllStudent);
        }
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetAllByIdExamination")]
        public async Task<IActionResult> GetAllStudentByIdExamination(int id)
        {
            var AllStudents = _unitOfWork.StudentRepository.GetAllByIdExamination(id);
            return Ok(AllStudents.ToArray());
        }
        [HttpGet("GetAllByIdTestSchedule")]
        //public async Task<IActionResult> GetAllByIdTestSchedule(int id)
        //{
        //    var AllStudents = _unitOfWork.StudentRepository.GetAllByIdTestSchedule(id);
        //    return Ok(AllStudents.ToArray());
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {

            var Student = _unitOfWork.StudentRepository.GetByID(id);
            if (Student == null)
            {
                return NotFound();
            }
            return Ok(Student);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNew(StudentModel NewStudent)
        {
            try
            {
                //Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
                //Console.WriteLine(NewStudent.Name);
                //Console.WriteLine(NewExamination.GradingDeadline);
                var t = _mapper.Map<Student>(NewStudent);
                Student studentadd = new Student()
                {
                    Name = t.Name,
                    BirthDay = t.BirthDay,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    IdentifierCode = t.IdentifierCode,
                    Password = t.Password,
                    NumberOfCheats = t.NumberOfCheats,
                    ExaminationId = t.ExaminationId,
                    FinalResult = new FinalResult(),
                    StudentTypeId = t.StudentTypeId,
                    FileSubmitted = new FileSubmitted(),
                    TestScheduleId = t.TestScheduleId,
                    ExaminationRoom_TestScheduleId = t.ExaminationRoom_TestScheduleId

                };
                FileSubmitted filesubmit = new FileSubmitted();
                FinalResult finalresult = new FinalResult();
                //filesubmit.Student = t;
                t.FileSubmitted = filesubmit;
                t.FinalResult = finalresult;
                _unitOfWork.StudentRepository.Insert(t);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("CreateStudentSCV")]
        public async Task<IActionResult> CreateStudentSCV(IFormFile file, int idExam = 0)
        {
            try
            {
                var SCV_students = _csvService.ReadCSV<SCV_AddStudentModel>(file.OpenReadStream());
                List<Student> students = new List<Student>();
                List<string> emailStu = new List<string>();
                foreach (var SCV_student in SCV_students)
                {
                    emailStu.Add(SCV_student.Email);
                    FileSubmitted filesubmit = new FileSubmitted();
                    FinalResult finalresult = new FinalResult();
                    var stud = _mapper.Map<StudentModel>(SCV_student);
                    stud.ExaminationId = idExam;
                    // create Student
                    Student student = _mapper.Map<Student>(stud);
                    student.FileSubmitted = filesubmit;
                    student.FinalResult = finalresult;
                    if (student.IdentifierCode == "" || student.IdentifierCode == null)
                    {
                        student.StudentTypeId = 2;
                    }
                    students.Add(student);
                    _unitOfWork.StudentRepository.Insert(student);
                }
                var Stu_DB = _unitOfWork.StudentRepository.GetAllByIdExamination(idExam);
                //check email in list student of exam is Duplicates

                var emailStu_DB = (from studentDB in Stu_DB select studentDB.Email).ToList();
                emailStu_DB.AddRange(emailStu);
                var knownKeys = new HashSet<string>();
                if (emailStu_DB.Any(item => !knownKeys.Add(item)))
                {
                    return BadRequest("Duplicates Email");
                }
                var tin = 0;
                _unitOfWork.SaveChange();
                return Ok(students);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentModel StudentUpdate)
        {
            try
            {
                _unitOfWork.StudentRepository.Update(_mapper.Map<Student>(StudentUpdate));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                _unitOfWork.StudentRepository.Delete(id);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetAllByRoomAndTestSchedule")]
        public IActionResult GetAllByRoomAndTestSchedule(int ExamRom_TestScheid)
        {
            var Students = from student in _unitOfWork.DbContext.Students
                           where student.ExaminationRoom_TestScheduleId == ExamRom_TestScheid
                           select student;
            return Ok(Students);
        }
        //Get all student by id ExamRom_TestSche and info file submited
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetAllInforSubmitFile")]
        public IActionResult GetAllInforSubmitFile(int ExamRom_TestScheid)
        {
            var students = from student in _unitOfWork.DbContext.Students
                           join filesubmitted in _unitOfWork.FileSubmittedRepository.GetAll()
                           on student.FileSubmittedId equals filesubmitted.Id
                           where student.ExaminationRoom_TestScheduleId == ExamRom_TestScheid
                           select new { student, filesubmitted };
            List<StudentInfoSubmitFileModel> result = new List<StudentInfoSubmitFileModel>();
            foreach (var student in students)
            {

                var k = _mapper.Map<StudentInfoSubmitFileModel>(student.student);
                k.FileSubmitted = _mapper.Map<FileSubmittedModel>(student.filesubmitted);
                result.Add(k);
            }
            return Ok(result);
        }
        //-----------------mở khóa cho sinh viên làm bài------------------
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("UnlockStudent")]
        public IActionResult UnlockStudent(int ExamRom_TestScheid)
        {
            var students = from student in _unitOfWork.DbContext.Students
                           join filesubmitted in _unitOfWork.FileSubmittedRepository.GetAll()
                           on student.FileSubmittedId equals filesubmitted.Id
                           where student.ExaminationRoom_TestScheduleId == ExamRom_TestScheid
                           select student;
            foreach (var student in students)
            {
                student.Locked = false;
                _unitOfWork.StudentRepository.Update(student);
            }
            _unitOfWork.SaveChange();
            return Ok();
        }
        //-------------Khóa tài khoản làm bài sinh viên----------------
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("LockStudent")]
        public IActionResult LockStudent(int ExamRom_TestScheid)
        {
            var students = from student in _unitOfWork.DbContext.Students
                           join filesubmitted in _unitOfWork.FileSubmittedRepository.GetAll()
                           on student.FileSubmittedId equals filesubmitted.Id
                           where student.ExaminationRoom_TestScheduleId == ExamRom_TestScheid
                           select student;
            foreach (var student in students)
            {
                student.Locked = true;
                _unitOfWork.StudentRepository.Update(student);
            }
            _unitOfWork.SaveChange();
            return Ok();
        }
        [HttpGet("CheckStudentIsLocked")]
        public IActionResult CheckStudentIsLocked(int ExamRom_TestScheid)
        {
            Boolean ListStudentIsLocked = false;
            var students = from student in _unitOfWork.DbContext.Students
                           join filesubmitted in _unitOfWork.FileSubmittedRepository.GetAll()
                           on student.FileSubmittedId equals filesubmitted.Id
                           where student.ExaminationRoom_TestScheduleId == ExamRom_TestScheid
                           select student;
            foreach (var student in students)
            {
                if (student.Locked) ListStudentIsLocked = true;
            }
            return Ok(ListStudentIsLocked);
        }
        //==========================================================

        [HttpGet("CreatePassWord")]
        public IActionResult CreatePassWord(int IdExam)
        {
            try
            {
                var students = SummaryService.Randomize(_unitOfWork.StudentRepository.GetAllByIdExamination(IdExam));
                //students = students.OrderBy(item => item.ExaminationRoom_TestScheduleId);
                var listRoom_Schedule = from testschedule in _unitOfWork.TestScheduleRepository.GetAll()
                                        join examroom_schedule in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                                        on testschedule.Id equals examroom_schedule.TestScheduleId
                                        where testschedule.ExaminationId == IdExam
                                        select examroom_schedule;

                //ramdom number
                var rdom = new Random();
                //int t = (IdExam % 10*10000)+ IdExam*3;

                if (!students.Any())
                {
                    return BadRequest("Examination not have Student");
                }
                foreach (var student in students)
                {
                    int hashCodeFinal = rdom.Next(10000, 19999);
                    var studentTemp = student;
                    if (student.HashCode == null)
                    {
                        var hashCodeFirst = SummaryService.IntToBase32(student.Id * 3 + 5);
                        if (hashCodeFirst.Length <= 8)
                        {
                            studentTemp.HashCode = hashCodeFirst + (SummaryService.IntToBase32(hashCodeFinal)).Substring(0, 8 - hashCodeFirst.Length);
                        }
                        else
                        {
                            studentTemp.HashCode = hashCodeFirst;
                        }
                        hashCodeFinal = hashCodeFinal + 3;
                    }
                    if (student.IdentifierCode == null)
                    {
                        //if (student.StudentTypeId == 1)
                        //{
                        //    student.IdentifierCode = "HV" + (student.Id + 1000).ToString();
                        //}
                        //else
                        {
                            student.IdentifierCode = "td" + (student.Id + 1000).ToString();
                        }
                    }
                    if (student.Password == null)
                    {
                        var pas = CreatePassword.CreateRandomPassword(8);
                        student.Password = pas;
                    }
                    if (student != null)
                    {
                        _unitOfWork.StudentRepository.Update(student);
                    }
                }

                string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var random = new Random();

                foreach (var room_schedu in listRoom_Schedule)
                {
                    var pathSchedule = Path.Combine(path, room_schedu.TestScheduleId.ToString());
                    if (!Directory.Exists(pathSchedule))
                    {
                        Directory.CreateDirectory(pathSchedule);
                    }
                    pathSchedule = Path.Combine(pathSchedule, SummaryService.IntToBase32(room_schedu.ExaminationRoomId + room_schedu.TestScheduleId + 12));
                    if (!Directory.Exists(pathSchedule))
                    {
                        Directory.CreateDirectory(pathSchedule);
                    }

                }

                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetStudentInfoByTokenIdExam")]
        public IActionResult GetStudentInfoByTokenIdExam()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;
            if (claim.Count() == 0) { return BadRequest("Token invalid"); }
            // Gets name from claims. Generally it's an email address.
            var idStudentClaim = claim
                .Where(x => x.Type == "StudentId")
                .FirstOrDefault();
            var t = _unitOfWork.StudentRepository.GetByID(int.Parse(idStudentClaim.Value));
            return Ok(t);
        }
        [HttpPut("ChangeTestSchedule")]
        public IActionResult ChangeTestSchedule(FromChangeScheduleForStudentModel ChangeScheduleForStudent)
        {
            var room_schedule = from examinationRoom_testSchedule in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                                where examinationRoom_testSchedule.TestScheduleId == ChangeScheduleForStudent.IdTestSchedule && examinationRoom_testSchedule.ExaminationRoomId == ChangeScheduleForStudent.IdRoom
                                select examinationRoom_testSchedule.Id;
            if (!room_schedule.Any()) return BadRequest("ExaminationRoom_TestSchedule not match");
            var student = _unitOfWork.StudentRepository.GetByID(ChangeScheduleForStudent.IdStudent);
            if (student == null) return BadRequest("StudentId not match");
            student.ExaminationRoom_TestScheduleId = room_schedule.First();
            _unitOfWork.StudentRepository.Update(student);
            _unitOfWork.SaveChange();
            return Ok("Update successful");
        }

        //get id Room_Schedule test by token student
        [HttpGet("GetIdScheduleByToken")]
        public IActionResult GetIdScheduleByToken()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                // Gets list of claims.
                IEnumerable<Claim> claim = identity.Claims;
                if (claim.Count() == 0) { return BadRequest("Token invalid"); }
                // Gets name from claims. Generally it's an email address.
                var idStudentClaim = claim
                    .Where(x => x.Type == "StudentId")
                    .FirstOrDefault();
                var idStude = int.Parse(idStudentClaim.Value);
                var idSchedule = (from student in _unitOfWork.StudentRepository.GetAll()
                                  join schedule_room in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                                  on student.ExaminationRoom_TestScheduleId equals schedule_room.Id
                                  where student.Id == idStude
                                  select schedule_room.TestScheduleId);
                if (!idSchedule.Any()) { return BadRequest("Id schedule test invalid"); }

                return Ok(idSchedule.FirstOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest("file not found");
            }
        }
        [HttpGet("IsUploadFileExcel")]
        // kiểm tra thí sinh đã upfile chưa
        public async Task<IActionResult> IsUploadFileExcel(int scheduleId)
        {
            try
            {
                string path = "";
                Console.WriteLine("================================");
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                // Gets list of claims.
                IEnumerable<Claim> claim = identity.Claims;
                if (claim.Count() == 0) { return BadRequest("Token invalid"); }

                var idstudentclaim = claim
                    .Where(x => x.Type == "StudentId")
                    .FirstOrDefault();
                if (idstudentclaim == null)
                {
                    return BadRequest("Id student error");
                }

                var studentIsLocked = _unitOfWork.StudentRepository.GetByID(int.Parse(idstudentclaim.Value));
                if (studentIsLocked.Locked) return BadRequest("Student locked");

                //var idstudent = Int32.Parse(idstudentclaim.Value);
                var submitfile = from student in _unitOfWork.StudentRepository.GetAll()
                                 join filesubmit in _unitOfWork.FileSubmittedRepository.GetAll()
                                 on student.FileSubmittedId equals filesubmit.Id
                                 where student.Id.ToString() == idstudentclaim.Value
                                 select filesubmit;
                if (!submitfile.Any()) { return BadRequest("Submit file Excel error"); }
                var subfile = submitfile.FirstOrDefault();
                if (!subfile.FileExcel) return BadRequest("File is not true in subfile.FileExcel");
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                path = Path.Combine(path, scheduleId.ToString());
                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                var idroom = from student in _unitOfWork.StudentRepository.GetAll()
                             join exam_schedu in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                             on student.ExaminationRoom_TestScheduleId equals exam_schedu.Id
                             where student.Id.ToString() == idstudentclaim.Value
                             select exam_schedu;
                if (!idroom.Any()) { return BadRequest("Get id room failure"); }
                path = Path.Combine(path, SummaryService.IntToBase32(idroom.First().ExaminationRoomId + idroom.First().TestScheduleId + 12));//path to file of each room 

                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                var studentLogin = _unitOfWork.StudentRepository.GetByID(Int32.Parse(idstudentclaim.Value));
                if (studentLogin.HashCode == null) return BadRequest("HashCode null");
                path = Path.Combine(path, studentLogin.HashCode);
                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                string[] filePaths = Directory.GetFiles(path, "*.xlsx");
                if (filePaths.Length == 0) return BadRequest("Not found zip file");
                string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileNameReturn = filePaths[0].Split("\\").LastOrDefault();
                var stream = System.IO.File.ReadAllBytes(filePaths[0]);
                return File(stream, mimeType, fileNameReturn);
                //foreach (var f in filePaths)
                //{
                //    System.IO.File.Delete(f);
                //}
                //using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                //{
                //    await file.CopyToAsync(fileStream);
                //}
                //_unitOfWork.SaveChange();
                //return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("IsUploadFileZip")]
        public async Task<IActionResult> IsUploadFileZip(int scheduleId)
        {
            try
            {
                string path = "";
                Console.WriteLine("================================");
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                // Gets list of claims.
                IEnumerable<Claim> claim = identity.Claims;
                if (claim.Count() == 0) { return BadRequest("Token invalid"); }

                var idstudentclaim = claim
                    .Where(x => x.Type == "StudentId")
                    .FirstOrDefault();
                if (idstudentclaim == null)
                {
                    return BadRequest("Id student error");
                }

                var studentIsLocked = _unitOfWork.StudentRepository.GetByID(int.Parse(idstudentclaim.Value));
                if (studentIsLocked.Locked) return BadRequest("Student locked");

                //var idstudent = Int32.Parse(idstudentclaim.Value);
                var submitfile = from student in _unitOfWork.StudentRepository.GetAll()
                                 join filesubmit in _unitOfWork.FileSubmittedRepository.GetAll()
                                 on student.FileSubmittedId equals filesubmit.Id
                                 where student.Id.ToString() == idstudentclaim.Value
                                 select filesubmit;
                if (!submitfile.Any()) { return BadRequest("Submit file Excel error"); }
                var subfile = submitfile.FirstOrDefault();
                if (!subfile.FileExcel) return BadRequest("File is not true in subfile.FileExcel");
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                path = Path.Combine(path, scheduleId.ToString());
                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                var idroom = from student in _unitOfWork.StudentRepository.GetAll()
                             join exam_schedu in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                             on student.ExaminationRoom_TestScheduleId equals exam_schedu.Id
                             where student.Id.ToString() == idstudentclaim.Value
                             select exam_schedu;
                if (!idroom.Any()) { return BadRequest("Get id room failure"); }
                path = Path.Combine(path, SummaryService.IntToBase32(idroom.First().ExaminationRoomId + idroom.First().TestScheduleId + 12));//path to file of each room 

                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                var studentLogin = _unitOfWork.StudentRepository.GetByID(Int32.Parse(idstudentclaim.Value));
                if (studentLogin.HashCode == null) return BadRequest("HashCode null");
                path = Path.Combine(path, studentLogin.HashCode);
                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                string[] filePaths = Directory.GetFiles(path, "*.zip");
                if (filePaths.Length == 0) return BadRequest("Not found zip file");
                //string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileNameReturn = filePaths[0].Split("\\").LastOrDefault();
                var stream = System.IO.File.ReadAllBytes(filePaths[0]);
                return File(stream, "application/zip", fileNameReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("IsUploadFileWord")]
        public async Task<IActionResult> IsUploadFileWord(int scheduleId)
        {
            try
            {
                string path = "";
                Console.WriteLine("================================");
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                // Gets list of claims.
                IEnumerable<Claim> claim = identity.Claims;
                if (claim.Count() == 0) { return BadRequest("Token invalid"); }

                var idstudentclaim = claim
                    .Where(x => x.Type == "StudentId")
                    .FirstOrDefault();
                if (idstudentclaim == null)
                {
                    return BadRequest("Id student error");
                }

                var studentIsLocked = _unitOfWork.StudentRepository.GetByID(int.Parse(idstudentclaim.Value));
                if (studentIsLocked.Locked) return BadRequest("Student locked");

                //var idstudent = Int32.Parse(idstudentclaim.Value);
                var submitfile = from student in _unitOfWork.StudentRepository.GetAll()
                                 join filesubmit in _unitOfWork.FileSubmittedRepository.GetAll()
                                 on student.FileSubmittedId equals filesubmit.Id
                                 where student.Id.ToString() == idstudentclaim.Value
                                 select filesubmit;
                if (!submitfile.Any()) { return BadRequest("Submit file Excel error"); }
                var subfile = submitfile.FirstOrDefault();
                if (!subfile.FileExcel) return BadRequest("File is not true in subfile.FileExcel");
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                path = Path.Combine(path, scheduleId.ToString());
                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                var idroom = from student in _unitOfWork.StudentRepository.GetAll()
                             join exam_schedu in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                             on student.ExaminationRoom_TestScheduleId equals exam_schedu.Id
                             where student.Id.ToString() == idstudentclaim.Value
                             select exam_schedu;
                if (!idroom.Any()) { return BadRequest("Get id room failure"); }
                path = Path.Combine(path, SummaryService.IntToBase32(idroom.First().ExaminationRoomId + idroom.First().TestScheduleId + 12));//path to file of each room 

                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                var studentLogin = _unitOfWork.StudentRepository.GetByID(Int32.Parse(idstudentclaim.Value));
                if (studentLogin.HashCode == null) return BadRequest("HashCode null");
                path = Path.Combine(path, studentLogin.HashCode);
                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                string[] filePaths = Directory.GetFiles(path, "*.docx");
                if (filePaths.Length == 0) return BadRequest("Not found zip file");
                //string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileNameReturn = filePaths[0].Split("\\").LastOrDefault();
                var stream = System.IO.File.ReadAllBytes(filePaths[0]);
                return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileNameReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("IsUploadFilePowerPoint")]
        public async Task<IActionResult> IsUploadFilePowerPoint(int scheduleId)
        {
            try
            {
                string path = "";
                Console.WriteLine("================================");
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                // Gets list of claims.
                IEnumerable<Claim> claim = identity.Claims;
                if (claim.Count() == 0) { return BadRequest("Token invalid"); }

                var idstudentclaim = claim
                    .Where(x => x.Type == "StudentId")
                    .FirstOrDefault();
                if (idstudentclaim == null)
                {
                    return BadRequest("Id student error");
                }

                var studentIsLocked = _unitOfWork.StudentRepository.GetByID(int.Parse(idstudentclaim.Value));
                if (studentIsLocked.Locked) return BadRequest("Student locked");

                //var idstudent = Int32.Parse(idstudentclaim.Value);
                var submitfile = from student in _unitOfWork.StudentRepository.GetAll()
                                 join filesubmit in _unitOfWork.FileSubmittedRepository.GetAll()
                                 on student.FileSubmittedId equals filesubmit.Id
                                 where student.Id.ToString() == idstudentclaim.Value
                                 select filesubmit;
                if (!submitfile.Any()) { return BadRequest("Submit file Excel error"); }
                var subfile = submitfile.FirstOrDefault();
                if (!subfile.FileExcel) return BadRequest("File is not true in subfile.FileExcel");
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                path = Path.Combine(path, scheduleId.ToString());
                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                var idroom = from student in _unitOfWork.StudentRepository.GetAll()
                             join exam_schedu in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                             on student.ExaminationRoom_TestScheduleId equals exam_schedu.Id
                             where student.Id.ToString() == idstudentclaim.Value
                             select exam_schedu;
                if (!idroom.Any()) { return BadRequest("Get id room failure"); }
                path = Path.Combine(path, SummaryService.IntToBase32(idroom.First().ExaminationRoomId + idroom.First().TestScheduleId + 12));//path to file of each room 

                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                var studentLogin = _unitOfWork.StudentRepository.GetByID(Int32.Parse(idstudentclaim.Value));
                if (studentLogin.HashCode == null) return BadRequest("HashCode null");
                path = Path.Combine(path, studentLogin.HashCode);
                if (!Directory.Exists(path))
                {
                    return BadRequest("Not found file");
                }
                string[] filePaths = Directory.GetFiles(path, "*.pptx");
                if (filePaths.Length == 0) return BadRequest("Not found zip file");
                //string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileNameReturn = filePaths[0].Split("\\").LastOrDefault();
                var stream = System.IO.File.ReadAllBytes(filePaths[0]);
                return File(stream, "application/vnd.openxmlformats-officedocument.presentationml.presentation", fileNameReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
