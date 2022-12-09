using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;
using InformaticsCertificationExamSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Ionic.Zip;
using System.Text.Json;

//using System.IO.Compression;

//using ICSharpCode.SharpZipLib.Zip;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TestSheduleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public TestSheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //======================--------------------------------------=====================
        //[HttpPost("AutoCreateTestSchedule1")]
        //public async Task<IActionResult> AutoCreateTestSchedule1(int IdExam)
        //{
        //    foreach (var Sched in _unitOfWork.TestScheduleRepository.GetAll())
        //    {
        //        if (Sched.ExaminationId == IdExam)
        //        {
        //            _unitOfWork.TestScheduleRepository.Delete(Sched.Id);
        //        }
        //    }
        //    var ListStudent = (from students in _unitOfWork.StudentRepository.GetAll()
        //                       where students.ExaminationId == IdExam
        //                       select students).ToList();
        //    var ListExaminationRoom = (from room in _unitOfWork.ExaminationRoomRepository.GetAll()
        //                               where room.Locked == false
        //                               select room).ToList();
        //    // --------------------chia theo số lượng giáo viên--------------------------
        //    var AllTeacher = from teachers in _unitOfWork.TeacherRepository.GetAll()
        //                     where teachers.Locked == false
        //                     select teachers;
        //    ListExaminationRoom = ListExaminationRoom.GetRange(0, (int)AllTeacher.Count() / 2);
        //    //--------------------------------===================-------------------------------
        //    int SumCapacityRooms = 0;
        //    foreach (var room in ListExaminationRoom)
        //    {
        //        SumCapacityRooms = SumCapacityRooms + room.Capacity;
        //    }
        //    Examination examination = _unitOfWork.ExaminationRepository.GetByID(IdExam);//null
        //    DateTime dtEx = examination.StarTime;

        //    int amountofSchedule = (int)Math.Ceiling((double)ListStudent.Count() / (double)SumCapacityRooms);

        //    int[] clock = new int[2] {7, 8, 14 };
        //    List<TestSchedule> listTestSchedule = new List<TestSchedule>();
        //    for (int i = 0; i < amountofSchedule; i++)
        //    {
        //        TestSchedule testSchedule = new TestSchedule();
        //        testSchedule.Name = "Ca " + (i + 1);
        //        DateTime startime = dtEx.AddDays(i / 2).AddHours(clock[i % 2]);
        //        testSchedule.StarTime = startime;
        //        testSchedule.EndTime = startime.AddHours(3);
        //        List<ExaminationRoom_TestSchedule> Exa_Tests = new List<ExaminationRoom_TestSchedule>();
        //        foreach (var room in ListExaminationRoom)
        //        {
        //            ExaminationRoom_TestSchedule ex_test = new ExaminationRoom_TestSchedule();
        //            ex_test.ExaminationRoom = room;
        //            List<Student> LStudents = new List<Student>();
        //            for (int j = 0; j < ex_test.ExaminationRoom.Capacity; j++)
        //            {
        //                if (ListStudent.Count() == 0) break;
        //                LStudents.Add(ListStudent.FirstOrDefault());
        //                ListStudent.Remove(ListStudent.FirstOrDefault());
        //            }
        //            ex_test.Students = LStudents;
        //            Exa_Tests.Add(ex_test);
        //            if (ListStudent.Count() == 0) break;
        //        }
        //        testSchedule.ExaminationRoom_TestSchedules = Exa_Tests;
        //        testSchedule.ExaminationId = IdExam;
        //        //listTestSchedule.Add(testSchedule);


        //        _unitOfWork.TestScheduleRepository.Insert(testSchedule);
        //    }
        //    //foreach (TestSchedule testSchedule in listTestSchedule)
        //    //{

        //    //}
        //    _unitOfWork.SaveChange();
        //    //var dateStr = "2022-10-24 13:30";
        //    //DateTime dt;
        //    //DateTime.TryParse(dateStr, out dt);
        //    //dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
        //    //DateTimeOffset utcTime2 = dt;
        //    //Console.WriteLine("f");
        //    //Console.WriteLine(utcTime2.UtcDateTime);
        //    //Console.WriteLine(dt);

        //    return Ok();
        //}

        [HttpPost("AutoCreateTestSchedule")]
        public async Task<IActionResult> AutoCreateTestSchedule(int IdExam)
        {
            Boolean startCreateAuto = false;
            Boolean ScheduleEmpty = true;
            foreach (var Sched in _unitOfWork.TestScheduleRepository.GetAll())
            {
                if (Sched.ExaminationId == IdExam)
                {
                    ScheduleEmpty = false;
                    var idRooms = _unitOfWork.ExaminationRoom_TestScheduleRepository.GetIdRoomByIdTestScheduleRepository(Sched.Id);
                    foreach(var idrooom in idRooms)
                    {
                        var room = _unitOfWork.ExaminationRoomRepository.GetByID(idrooom);
                        if (room == null) continue;
                        if (room.Locked) startCreateAuto = true;
                    }
                    if (startCreateAuto)
                    {
                        _unitOfWork.TestScheduleRepository.Delete(Sched.Id);
                    }
                }
            }
            if (!startCreateAuto && !ScheduleEmpty) return Ok("Not thing to do");
            var ListStudent = SummaryService.Randomize((from students in _unitOfWork.StudentRepository.GetAll()
                                                        where students.ExaminationId == IdExam
                                                        select students)).ToList();

            var ListExaminationRoom = (from room in _unitOfWork.ExaminationRoomRepository.GetAll()
                                       where room.Locked == false
                                       select room).ToList();
            // --------------------chia theo số lượng giáo viên--------------------------
            var AllTeacher = from teachers in _unitOfWork.TeacherRepository.GetAll()
                             where teachers.Locked == false
                             select teachers;
            //ListExaminationRoom = ListExaminationRoom.GetRange(0, (int)AllTeacher.Count() / 2);
            //--------------------------------===================-------------------------------
            int SumCapacityRooms = 0;
            foreach (var room in ListExaminationRoom)
            {
                SumCapacityRooms = SumCapacityRooms + room.Capacity/2;
            }
            Examination examination = _unitOfWork.ExaminationRepository.GetByID(IdExam);//null
            DateTime dtEx = examination.StarTime;

            int amountofSchedule = (int)Math.Ceiling((double)ListStudent.Count() / (double)SumCapacityRooms);
            if (amountofSchedule > 4) return BadRequest("Too more student");
            //Ca 1: 7h->9h15
            TestSchedule testSchedule1 = new TestSchedule();
            testSchedule1.Name = "Ca 1";
            DateTime startime = dtEx.AddHours(7);
            testSchedule1.StarTime = startime;
            testSchedule1.EndTime = startime.AddHours(2).AddMinutes(15);

            //Ca 2: 9h20->11h35
            TestSchedule testSchedule2 = new TestSchedule();
            testSchedule2.Name = "Ca 2";
            startime = dtEx.AddHours(9).AddMinutes(20);
            testSchedule2.StarTime = startime;
            testSchedule2.EndTime = startime.AddHours(2).AddMinutes(15);

            //------(afternoon)-----
            //Ca 3: 13h30->15h45 
            TestSchedule testSchedule3 = new TestSchedule();
            testSchedule3.Name = "Ca 3";
            startime = dtEx.AddHours(13).AddMinutes(30);
            testSchedule3.StarTime = startime;
            testSchedule3.EndTime = startime.AddHours(2).AddMinutes(15);
            //Ca 4:15h50->18h5 
            TestSchedule testSchedule4 = new TestSchedule();
            testSchedule4.Name = "Ca 4";
            startime = dtEx.AddHours(15).AddMinutes(50);
            testSchedule4.StarTime = startime;
            testSchedule4.EndTime = startime.AddHours(2).AddMinutes(15);

            List<TestSchedule> listSchedulesInit = new List<TestSchedule>();
            listSchedulesInit.Add(testSchedule1);
            listSchedulesInit.Add(testSchedule2);
            listSchedulesInit.Add(testSchedule3);
            listSchedulesInit.Add(testSchedule4);

            foreach (var Schedule in listSchedulesInit)
            {
                if (ListStudent.Count() == 0) break;
                List<ExaminationRoom_TestSchedule> Exa_Tests = new List<ExaminationRoom_TestSchedule>();
                foreach (var room in ListExaminationRoom)
                {
                    ExaminationRoom_TestSchedule ex_test = new ExaminationRoom_TestSchedule();
                    ex_test.ExaminationRoom = room;
                    List<Student> LStudents = new List<Student>();
                    for (int j = 0; j < ex_test.ExaminationRoom.Capacity/2; j++)
                    {
                        if (ListStudent.Count() == 0) break;
                        LStudents.Add(ListStudent.FirstOrDefault());
                        ListStudent.Remove(ListStudent.FirstOrDefault());
                    }
                    ex_test.Students = LStudents;
                    Exa_Tests.Add(ex_test);
                    if (ListStudent.Count() == 0) break;
                }
                Schedule.ExaminationRoom_TestSchedules = Exa_Tests;
                Schedule.ExaminationId = IdExam;
                //listTestSchedule.Add(testSchedule);


                _unitOfWork.TestScheduleRepository.Insert(Schedule);
            }
            var exam = _unitOfWork.ExaminationRepository.GetByID(IdExam);
            if (exam != null)
            {
                exam.IsScheduled = true;
                _unitOfWork.ExaminationRepository.Update(exam);
            }
            _unitOfWork.SaveChange();

            return Ok();
        }

        [HttpGet("GetAllByIdExamination")]
        public async Task<IActionResult> GetAllByIdExamination(int IdExam)
        {
            List<object> result = new List<object>();
            var schedule = (from sche in _unitOfWork.TestScheduleRepository.GetAllByIdExamination(IdExam)
                            select sche).ToList();


            foreach (var Schedu in schedule)
            {
                var room_schedu = (from exam_testsche in _unitOfWork.DbContext.ExaminationRoom_TestSchedule
                                   join room in _unitOfWork.DbContext.ExaminationRooms
                                   on exam_testsche.ExaminationRoom.Id equals room.Id
                                   where exam_testsche.TestSchedule.Id == Schedu.Id
                                   select new { room = room.Name, exam_testscheid = exam_testsche.Id, exam_testsche.TestSchedule.Name }
                                  ).Distinct();
                foreach (var room in room_schedu)
                {
                    var sched = new { Schedu, room.room, room.exam_testscheid };
                    result.Add(sched);
                }

            }
            return Ok(result);
        }
        //get all schedule with each Examination
        [HttpGet("GetAllScheduleByIdExamination")]
        public async Task<IActionResult> GetAllScheduleByIdExamination(int IdExam)
        {
            var Schedules = (from sche in _unitOfWork.TestScheduleRepository.GetAll()
                             where sche.ExaminationId == IdExam
                             select sche);
            return Ok(Schedules);
        }

        //======================--------------------------------------=====================
        [HttpGet("GetScheduleByTokenTeacher")]
        public IActionResult GetScheduleByTokenTeacher()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;
            if (claim.Count() == 0) { return BadRequest("Token invalid"); }
            // Gets name from claims. Generally it's an email address.
            var idTeacherClaim = claim
                .Where(x => x.Type == "TeacherId")
                .FirstOrDefault();
            Console.WriteLine("============================ID======================---------------" + idTeacherClaim);
            //get all Superviser by id teacher
            var supervisers = (from teacher in _unitOfWork.DbContext.Teachers
                               where teacher.Id.ToString() == idTeacherClaim.Value
                               select teacher.Supervisors.ToList());
            var supervisersList = supervisers.SelectMany(i => i).Distinct().ToList();

            var room_schedules = from roomSchedule in _unitOfWork.DbContext.ExaminationRoom_TestSchedule.ToList()
                                 join sup_viser in supervisersList
                                 on roomSchedule.SupervisorID equals sup_viser.Id
                                 select roomSchedule;
            var result = from room_schedule in room_schedules
                         join room_scheduleDB in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                         on room_schedule.Id equals room_scheduleDB.Id
                         join room in _unitOfWork.ExaminationRoomRepository.GetAll()
                         on room_scheduleDB.ExaminationRoomId equals room.Id
                         join schedule in _unitOfWork.TestScheduleRepository.GetAll()
                         on room_scheduleDB.TestScheduleId equals schedule.Id
                         select new
                         {
                             roomName = room.Name,
                             roomId = room.Id,
                             testSchedule = schedule.Name,
                             startTime = schedule.StarTime,
                             endTime = schedule.EndTime,
                             testScheduleId = schedule.Id,
                             examinationId = schedule.ExaminationId,
                             room_scheduleId = room_scheduleDB.Id
                         };

            return Ok(result);
        }

        [HttpGet("GetScheduleByTokenIdExam")]
        public IActionResult GetScheduleByTokenIdExam(int idExam)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;
            if (claim.Count() == 0) { return BadRequest("Token invalid"); }
            // Gets name from claims. Generally it's an email address.
            var idTeacherClaim = claim
                .Where(x => x.Type == "TeacherId")
                .FirstOrDefault();
            Console.WriteLine("============================ID======================---------------" + idTeacherClaim);
            //get all Superviser by id teacher
            var supervisers = (from teacher in _unitOfWork.DbContext.Teachers
                               where teacher.Id.ToString() == idTeacherClaim.Value
                               select teacher.Supervisors.ToList());
            var supervisersList = supervisers.SelectMany(i => i).Distinct().ToList();

            var room_schedules = from roomSchedule in _unitOfWork.DbContext.ExaminationRoom_TestSchedule.ToList()
                                 join sup_viser in supervisersList
                                 on roomSchedule.SupervisorID equals sup_viser.Id
                                 select roomSchedule;
            var result = from room_schedule in room_schedules
                         join room_scheduleDB in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                         on room_schedule.Id equals room_scheduleDB.Id
                         join room in _unitOfWork.ExaminationRoomRepository.GetAll()
                         on room_scheduleDB.ExaminationRoomId equals room.Id
                         join schedule in _unitOfWork.TestScheduleRepository.GetAll()
                         on room_scheduleDB.TestScheduleId equals schedule.Id
                         where schedule.ExaminationId == idExam
                         select new
                         {
                             roomName = room.Name,
                             roomId = room.Id,
                             testSchedule = schedule.Name,
                             startTime = schedule.StarTime,
                             endTime = schedule.EndTime,
                             testScheduleId = schedule.Id,
                             examinationId = schedule.ExaminationId,
                             room_scheduleId = room_scheduleDB.Id
                         };

            return Ok(result);
        }

        [HttpPost("{id}/DownloadSubmitFile")]
        public async Task<IActionResult> DownloadSubmitFile(int id)
        {
            try
            {
                string archiveName = String.Format("archive.zip");
                //DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                var testSchedule = _unitOfWork.TestScheduleRepository.GetByID(id);
                if (testSchedule == null) return NotFound("Id test schedule not found");
                var path = Path.Combine(Environment.CurrentDirectory, "FileSubmit");
                path = Path.Combine(path, id.ToString());
                var zipFolder = Path.GetFullPath(path);

                MemoryStream outputStream = new MemoryStream();
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(zipFolder);
                    zip.Save(outputStream);
                }
                outputStream.Seek(0, SeekOrigin.Begin);
                return File(outputStream, "application/zip", $"{testSchedule.Name}.zip");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("dowload")]
        public IActionResult Download(int id)
        {

            //DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            var testSchedule = _unitOfWork.TestScheduleRepository.GetByID(id);
            if (testSchedule == null) return NotFound("Id test schedule not found");
            var path = Path.Combine(Environment.CurrentDirectory, "FileSubmit");
            string archive = Path.Combine(path,String.Format("archive.zip"));
            path = Path.Combine(path, id.ToString());
            var zipFolder = Path.GetFullPath(path);

            // clear any existing archive
            if (System.IO.File.Exists(archive))
            {
                System.IO.File.Delete(archive);
            }
            // empty the temp folder


            // create a new archive
            System.IO.Compression.ZipFile.CreateFromDirectory(zipFolder, archive);
            var stream = System.IO.File.ReadAllBytes(archive);
            return File(stream, "application/zip", $"{testSchedule.Name}.zip");

            //return File(archive, "application/zip", "archive.zip");
        }

    }
}




/*[HttpPost("AutoCreateTestSchedule")]
        public async Task<IActionResult> AutoCreateTestSchedule(int IdExam)
        {
            foreach (var Sched in _unitOfWork.TestScheduleRepository.GetAll())
            {
                if (Sched.ExaminationId == IdExam)
                {
                    _unitOfWork.TestScheduleRepository.Delete(Sched.Id);
                }
            }
            var ListStudent = (from students in _unitOfWork.StudentRepository.GetAll()
                               where students.ExaminationId == IdExam
                               select students).ToList();
            var ListExaminationRoom = (from room in _unitOfWork.ExaminationRoomRepository.GetAll()
                                       select room).ToList();
            int SumCapacityRooms = 0;
            foreach (var room in ListExaminationRoom)
            {
                SumCapacityRooms = SumCapacityRooms + room.Capacity;
            }
            Examination examination = _unitOfWork.ExaminationRepository.GetByID(IdExam);//null
            DateTime dtEx = examination.StarTime;

            int amountofSchedule = (int)Math.Ceiling((double)ListStudent.Count() / (double)SumCapacityRooms);

            int[] clock = new int[2] { 8, 14 };
            List<TestSchedule> listTestSchedule = new List<TestSchedule>();
            for (int i = 0; i < amountofSchedule; i++)
            {
                foreach (var room in ListExaminationRoom)
                {
                    TestSchedule testSchedule = new TestSchedule();
                    testSchedule.Name = "Ca " + (i + 1) + " " + room.Name;
                    DateTime startime = dtEx.AddDays(i / 2).AddHours(clock[i % 2]);
                    testSchedule.StarTime = startime;
                    testSchedule.EndTime = startime.AddHours(3);
                    testSchedule.ExaminationRoom = room;
                    testSchedule.ExaminationId = IdExam;
                    //listTestSchedule.Add(testSchedule);

                    List<Student> LStudents = new List<Student>();
                    for (int i = 0; i < testSchedule.ExaminationRoom.Capacity; i++)
                    {
                        if (ListStudent.Count() == 0) break;
                        LStudents.Add(ListStudent.FirstOrDefault());
                        ListStudent.Remove(ListStudent.FirstOrDefault());
                    }
                    testSchedule.Students = LStudents;
                    _unitOfWork.TestScheduleRepository.Insert(testSchedule);
                }
            }
            //foreach (TestSchedule testSchedule in listTestSchedule)
            //{

            //}
            _unitOfWork.SaveChange();
            //var dateStr = "2022-10-24 13:30";
            //DateTime dt;
            //DateTime.TryParse(dateStr, out dt);
            //dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
            //DateTimeOffset utcTime2 = dt;
            //Console.WriteLine("f");
            //Console.WriteLine(utcTime2.UtcDateTime);
            //Console.WriteLine(dt);

            return Ok();
        }

*/
