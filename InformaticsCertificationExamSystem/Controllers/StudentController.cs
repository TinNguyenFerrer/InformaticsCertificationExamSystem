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
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudent()
        {
            var AllStudent = _unitOfWork.StudentRepository.GetAll().ToList();
            return Ok(AllStudent);
        }

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
                //Console.WriteLine(NewExamination.GradingDeadline);
                var t = _mapper.Map<Student>(NewStudent);
                Student studentadd = new Student()
                {
                    Name=t.Name,
                    BirthPlace=t.BirthPlace,
                    BirthDay=t.BirthDay,
                    Email=t.Email,
                    PhoneNumber=t.PhoneNumber,
                    IdentifierCode=t.IdentifierCode,
                    Password=t.Password,
                    NumberOfCheats=t.NumberOfCheats,
                    ExaminationId=t.ExaminationId,
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
        [HttpGet("GetAllByRoomAndTestSchedule")]
        public IActionResult GetAllByRoomAndTestSchedule(int ExamRom_TestScheid)
        {
            var Students = from student in _unitOfWork.DbContext.Students
                           where student.ExaminationRoom_TestScheduleId == ExamRom_TestScheid
                           select student;
            return Ok(Students);
        }
        //Get all student by id ExamRom_TestSche and info file submited
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
                k.FileSubmitted = _mapper.Map < FileSubmittedModel>(student.filesubmitted);
                result.Add(k);
            }
            return Ok(result);
        }
        //-----------------mở khóa cho sinh viên làm bài------------------
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

        [HttpGet("CreatePassWord")]
        public IActionResult CreatePassWord(int IdExam)
        {
            try
            {
                var students = _unitOfWork.StudentRepository.GetAllByIdExamination(IdExam);
                if (!students.Any())
                {
                    return BadRequest("Examination not have Student");
                }
                foreach (var student in students)
                {

                    var studentTemp = student;
                    if (student.IdentifierCode == null)
                    {
                        if (student.StudentTypeId == 1)
                        {
                            student.IdentifierCode = "HV" + (student.Id + 1000).ToString();
                        }
                        else
                        {
                            student.IdentifierCode = "TD" + (student.Id + 1000).ToString();
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


    }
}
