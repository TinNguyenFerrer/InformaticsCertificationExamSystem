using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using InformaticsCertificationExamSystem.Services;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TheoryTestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public TheoryTestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("getAllByIdSchedule")]
        public IActionResult getAllByIdSchedule(int IdSche)
        {
            var ListTheoryTest = (from theorytest in _unitOfWork.DbContext.TheoryTests
                                  where theorytest.TestSchedule.Id == IdSche
                                  select theorytest).ToList();
            return Ok(ListTheoryTest);
        }
        [HttpPost("CreateTheoryTest")]
        public async Task<IActionResult> CreateTheoryTest(IFormFile file, int ScheduleId, string Name, IFormFile fileExel)
        {
            string path = "";
            Console.WriteLine("================================");
            Console.WriteLine(file.ContentType);
            Console.WriteLine("tên bài thi " + Name);
            Console.WriteLine(fileExel.ContentType);
            var theorytests = _unitOfWork.TheoryTestRepository.GetAll();
            var theorytest = (from t in theorytests where t.TestScheduleId == ScheduleId select t);
            //if (theorytest.Any())
            //{
            //    _unitOfWork.TheoryTestRepository.Delete(theorytest.FirstOrDefault().Id);
            //    string pathdelete = "";
            //    pathdelete = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
            //    if (System.IO.File.Exists(Path.Combine(pathdelete, theorytest.FirstOrDefault().Id + ".pdf")))
            //    {
            //        // If file found, delete it    
            //        System.IO.File.Delete(Path.Combine(pathdelete, theorytest.FirstOrDefault().Id + ".pdf"));
            //    }
            //}
            try
            {
                TheoryTest TheoryTest = new TheoryTest();
                var Sche = _unitOfWork.TestScheduleRepository.GetByID(ScheduleId);
                TheoryTest.TestSchedule = Sche;
                TheoryTest.Name = Name;
                _unitOfWork.TheoryTestRepository.Insert(TheoryTest);
                //Must save chang first
                _unitOfWork.SaveChange();

                if (file.Length > 0 && file.ContentType == "application/pdf")
                {

                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, TheoryTest.Id + ".pdf"), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //return Ok();
                }
                else
                {
                    return BadRequest("File Empty");
                }
                if (fileExel.Length > 0 /*&& fileExel.ContentType == "application/pdf"*/)
                {

                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, TheoryTest.Id + ".xlsx"), FileMode.Create))
                    {
                        await fileExel.CopyToAsync(fileStream);
                    }
                    _unitOfWork.SaveChange();
                    return Ok();
                }
                else
                {
                    return BadRequest("File Empty");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
        [HttpGet("DownloadPdfFile")]
        public IActionResult DownloadPdfFile(int id)
        {
            try
            {
                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                string mimeType = "application/pdf";
                path = Path.Combine(path, id + ".pdf");
                var stream = System.IO.File.ReadAllBytes(path);
                return File(stream, mimeType, id + ".pdf");
            }
            catch (Exception e)
            {
                return BadRequest("file not found");
            }
        }
        [HttpGet("DownloadExcelFile")]
        public IActionResult DownloadExcelFile(int id)
        {
            try
            {
                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                path = Path.Combine(path, id + ".xlsx");
                var stream = System.IO.File.ReadAllBytes(path);
                return File(stream, mimeType, id + ".xlsx");
            }
            catch (Exception e)
            {
                return BadRequest("file not found");
            }
        }
        [HttpGet("DownloadPdfFileByToken")]
        public IActionResult DownloadPdfFileByToken()
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
                var idStudent = Int32.Parse(idStudentClaim.Value);
                if (idStudentClaim == null) { return BadRequest("Token Student null"); }
                var idSchedules = (from student in _unitOfWork.StudentRepository.GetAll()
                                   join schedule_room in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                                   on student.ExaminationRoom_TestScheduleId equals schedule_room.Id
                                   where student.Id == Int32.Parse(idStudentClaim.Value)
                                   select schedule_room.TestScheduleId);
                if (!idSchedules.Any()) { return BadRequest("Id schedule test invalid"); }



                var theorytests = from testschedule in _unitOfWork.TestScheduleRepository.GetAll()
                                  join theorytest in _unitOfWork.TheoryTestRepository.GetAll()
                                  on testschedule.Id equals theorytest.TestScheduleId
                                  where testschedule.Id == idSchedules.FirstOrDefault()
                                  select theorytest.Id;
                if (!theorytests.Any()) { return BadRequest("TestSchedule_Id invalid"); }

                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                string mimeType = "application/pdf";
                path = Path.Combine(path, theorytests.ToArray()[idStudent % theorytests.Count()] + ".pdf");
                var stream = System.IO.File.ReadAllBytes(path);
                return File(stream, mimeType, theorytests.First() + ".pdf");
            }
            catch (Exception e)
            {
                return BadRequest("file not found");
            }
        }

        [HttpGet("DownloadExcelByToken")]
        public IActionResult DownloadExcelByToken()
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
                var idStudent = Int32.Parse(idStudentClaim.Value);
                if (idStudentClaim == null) { return BadRequest("Token Student null"); }
                var idSchedule = (from student in _unitOfWork.StudentRepository.GetAll()
                                  join schedule_room in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                                  on student.ExaminationRoom_TestScheduleId equals schedule_room.Id
                                  where student.Id == idStudent
                                  select schedule_room.TestScheduleId);
                if (!idSchedule.Any()) { return BadRequest("Id schedule test invalid"); }


                var theorytests = from testschedule in _unitOfWork.TestScheduleRepository.GetAll()
                                  join theorytest in _unitOfWork.TheoryTestRepository.GetAll()
                                  on testschedule.Id equals theorytest.TestScheduleId
                                  where testschedule.Id == idSchedule.FirstOrDefault()
                                  select theorytest.Id;
                if (!theorytests.Any()) { return BadRequest("TestSchedule_Id invalid"); }

                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                path = Path.Combine(path, theorytests.ToArray()[idStudent % theorytests.Count()] + ".xlsx");
                string mimeType = "application/octet-stream";
                var stream = System.IO.File.ReadAllBytes(path);
                return File(stream, mimeType, theorytests.First() + ".xlsx");
            }
            catch (Exception e)
            {
                return BadRequest("file not found");
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteTheoryTest(int id)
        {
            try
            {
                _unitOfWork.TheoryTestRepository.Delete(id);
                _unitOfWork.SaveChange();
                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                if (System.IO.File.Exists(Path.Combine(path, id + ".pdf")))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(Path.Combine(path, id + ".pdf"));
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //get id theory test by token
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
                var idSchedule = (from student in _unitOfWork.StudentRepository.GetAll()
                                  join schedule_room in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                                  on student.ExaminationRoom_TestScheduleId equals schedule_room.Id
                                  select schedule_room.Id);
                if (!idSchedule.Any()) { return BadRequest("Id schedule test invalid"); }

                return Ok(idSchedule.FirstOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest("file not found");
            }
        }

        [HttpPost("UploadFileExcel")]
        public async Task<IActionResult> UploadFileExcel(IFormFile file, int scheduleId)
        {
            try
            {
                string path = "";
                Console.WriteLine("================================");
                Console.WriteLine(file.ContentType);
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
                //var idstudent = Int32.Parse(idstudentclaim.Value);
                var submitfile = from student in _unitOfWork.StudentRepository.GetAll()
                                 join filesubmit in _unitOfWork.FileSubmittedRepository.GetAll()
                                 on student.FileSubmittedId equals filesubmit.Id
                                 where student.Id.ToString() == idstudentclaim.Value
                                 select filesubmit;
                if (!submitfile.Any()) { return BadRequest("Submit file Excel error"); }
                var subfile = submitfile.FirstOrDefault();
                subfile.FileExcel = true;
                _unitOfWork.FileSubmittedRepository.Update(subfile);
                if (file.Length > 0)
                {

                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                    path = Path.Combine(path, scheduleId.ToString());
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
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
                        Directory.CreateDirectory(path);
                    }
                    var studentLogin = _unitOfWork.StudentRepository.GetByID(Int32.Parse(idstudentclaim.Value));
                    if (studentLogin.HashCode == null) return BadRequest("HashCode null");
                    path = Path.Combine(path, studentLogin.HashCode);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string[] filePaths = Directory.GetFiles(path, "*.xlsx");
                    foreach (var f in filePaths)
                    {
                        System.IO.File.Delete(f);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    _unitOfWork.SaveChange();
                    return Ok();
                }
                else
                {
                    return BadRequest("File Empty");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPost("UploadFileWord")]
        public async Task<IActionResult> UploadFileWord(IFormFile file, int scheduleId)
        {
            try
            {
                string path = "";
                Console.WriteLine("================================");
                Console.WriteLine(file.ContentType);
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
                //var idstudent = Int32.Parse(idstudentclaim.Value);
                var submitfile = from student in _unitOfWork.StudentRepository.GetAll()
                                 join filesubmit in _unitOfWork.FileSubmittedRepository.GetAll()
                                 on student.FileSubmittedId equals filesubmit.Id
                                 where student.Id.ToString() == idstudentclaim.Value
                                 select filesubmit;
                if (!submitfile.Any()) { return BadRequest("Submit file Excel error"); }
                var subfile = submitfile.FirstOrDefault();
                subfile.FileWord = true;
                _unitOfWork.FileSubmittedRepository.Update(subfile);
                if (file.Length > 0)
                {

                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                    path = Path.Combine(path, scheduleId.ToString());
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
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
                        Directory.CreateDirectory(path);
                    }
                    var studentLogin = _unitOfWork.StudentRepository.GetByID(Int32.Parse(idstudentclaim.Value));
                    if (studentLogin.HashCode == null) return BadRequest("HashCode null");
                    path = Path.Combine(path, studentLogin.HashCode);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string[] filePaths = Directory.GetFiles(path, "*.docx");
                    foreach (var f in filePaths)
                    {
                        System.IO.File.Delete(f);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    _unitOfWork.SaveChange();
                    return Ok();
                }
                else
                {
                    return BadRequest("File Empty");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPost("UploadFilePowerPoint")]
        public async Task<IActionResult> UploadFilePowerPoint(IFormFile file, int scheduleId)
        {
            try
            {
                string path = "";
                Console.WriteLine("================================");
                Console.WriteLine(file.ContentType);
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
                //var idstudent = Int32.Parse(idstudentclaim.Value);
                var submitfile = from student in _unitOfWork.StudentRepository.GetAll()
                                 join filesubmit in _unitOfWork.FileSubmittedRepository.GetAll()
                                 on student.FileSubmittedId equals filesubmit.Id
                                 where student.Id.ToString() == idstudentclaim.Value
                                 select filesubmit;
                if (!submitfile.Any()) { return BadRequest("Submit file Excel error"); }
                var subfile = submitfile.FirstOrDefault();
                subfile.FilePowerPoint = true;
                _unitOfWork.FileSubmittedRepository.Update(subfile);
                if (file.Length > 0)
                {

                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                    path = Path.Combine(path, scheduleId.ToString());
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
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
                        Directory.CreateDirectory(path);
                    }
                    var studentLogin = _unitOfWork.StudentRepository.GetByID(Int32.Parse(idstudentclaim.Value));
                    if (studentLogin.HashCode == null) return BadRequest("HashCode null");
                    path = Path.Combine(path, studentLogin.HashCode);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string[] filePaths = Directory.GetFiles(path, "*.pptx");
                    foreach (var f in filePaths)
                    {
                        System.IO.File.Delete(f);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    _unitOfWork.SaveChange();
                    return Ok();
                }
                else
                {
                    return BadRequest("File Empty");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
        [HttpPost("UploadFileWindow")]
        public async Task<IActionResult> UploadFileWindow(IFormFile file, int scheduleId)
        {
            try
            {
                string path = "";
                Console.WriteLine("================================");
                Console.WriteLine(file.ContentType);
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
                //var idstudent = Int32.Parse(idstudentclaim.Value);
                var submitfile = from student in _unitOfWork.StudentRepository.GetAll()
                                 join filesubmit in _unitOfWork.FileSubmittedRepository.GetAll()
                                 on student.FileSubmittedId equals filesubmit.Id
                                 where student.Id.ToString() == idstudentclaim.Value
                                 select filesubmit;
                if (!submitfile.Any()) { return BadRequest("Submit file Excel error"); }
                var subfile = submitfile.FirstOrDefault();
                subfile.FileWindow = true;
                _unitOfWork.FileSubmittedRepository.Update(subfile);
                if (file.Length > 0)
                {

                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                    path = Path.Combine(path, scheduleId.ToString());
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
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
                        Directory.CreateDirectory(path);
                    }
                    var studentLogin = _unitOfWork.StudentRepository.GetByID(Int32.Parse(idstudentclaim.Value));
                    if (studentLogin.HashCode == null) return BadRequest("HashCode null");
                    path = Path.Combine(path, studentLogin.HashCode);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string[] filePaths = Directory.GetFiles(path, "*.zip");
                    foreach (var f in filePaths)
                    {
                        System.IO.File.Delete(f);
                    }
                    string[] filePathsRAR = Directory.GetFiles(path, "*.rar");
                    foreach (var f in filePathsRAR)
                    {
                        System.IO.File.Delete(f);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    _unitOfWork.SaveChange();
                    return Ok();
                }
                else
                {
                    return BadRequest("File Empty");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

    }
}
