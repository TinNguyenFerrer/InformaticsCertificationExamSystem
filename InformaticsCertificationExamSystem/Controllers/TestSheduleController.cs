﻿using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost("AutoCreateTestSchedule")]
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

                TestSchedule testSchedule = new TestSchedule();
                testSchedule.Name = "Ca " + (i + 1);
                DateTime startime = dtEx.AddDays(i / 2).AddHours(clock[i % 2]);
                testSchedule.StarTime = startime;
                testSchedule.EndTime = startime.AddHours(3);
                List<ExaminationRoom_TestSchedule> Exa_Tests = new List<ExaminationRoom_TestSchedule>();
                foreach (var room in ListExaminationRoom)
                {
                    ExaminationRoom_TestSchedule ex_test = new ExaminationRoom_TestSchedule();
                    ex_test.ExaminationRoom = room;
                    List<Student> LStudents = new List<Student>();
                    for (int j = 0; j < ex_test.ExaminationRoom.Capacity; j++)
                    {
                        if (ListStudent.Count() == 0) break;
                        LStudents.Add(ListStudent.FirstOrDefault());
                        ListStudent.Remove(ListStudent.FirstOrDefault());
                    }
                    ex_test.Students = LStudents;
                    Exa_Tests.Add(ex_test);
                }
                testSchedule.ExaminationRoom_TestSchedules = Exa_Tests;
                testSchedule.ExaminationId = IdExam;
                //listTestSchedule.Add(testSchedule);


                _unitOfWork.TestScheduleRepository.Insert(testSchedule);
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
                                   select new { room = room.Name, exam_testscheid = exam_testsche.Id}
                                  ).Distinct();
                foreach (var room in room_schedu)
                {
                    var sched = new { Schedu, room.room,room.exam_testscheid };
                    result.Add(sched);
                }

            }
            return Ok(result);
        }

        //======================--------------------------------------=====================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {

            var Room = _unitOfWork.ExaminationRoomRepository.GetByID(id);
            if (Room == null)
            {
                return NotFound();
            }
            return Ok(Room);
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