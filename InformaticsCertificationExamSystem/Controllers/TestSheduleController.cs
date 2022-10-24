using AutoMapper;
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

            int amountofSchedule = (int)Math.Round((double)ListStudent.Count() / (double)SumCapacityRooms);

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
                    listTestSchedule.Add(testSchedule);
                }
            }
            foreach (TestSchedule testSchedule in listTestSchedule)
            {
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
            return Ok(_unitOfWork.TestScheduleRepository.GetAllByIdExamination(IdExam).ToArray());
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
