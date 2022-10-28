using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SupervisorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public SupervisorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost("AutoCreateSupervisor")]
        public async Task<IActionResult> AutoCreateSupervisor(int IdTestSchedule)
        {
            //xóa giám thị cũ nếu đã chia giám thị rồi
            var supervisors = from Room_schedule in _unitOfWork.DbContext.ExaminationRoom_TestSchedule
                             where Room_schedule.TestSchedule.Id == IdTestSchedule
                             select Room_schedule.Supervisor;
            if (supervisors.Count() != 0)
            {
                foreach (var supervisor in supervisors)
                {
                    _unitOfWork.SupervisorRepository.Delete(supervisor.Id);
                }
            }

            var ExaminationRoom_TestSchedule = (from room_schedule in _unitOfWork.DbContext.ExaminationRoom_TestSchedule
                                   where room_schedule.TestSchedule.Id == IdTestSchedule
                                   select room_schedule).Distinct().ToList();
                          
            var AllTeacher = from teachers in _unitOfWork.TeacherRepository.GetAll()
                             where teachers.Locked == false
                             select teachers;
            if (ExaminationRoom_TestSchedule.Count() > AllTeacher.Count() * 2) return BadRequest("not enough teachers"); 

            Random rnd = new Random();
            int rd1;
            int rd2;
            var _Teacher = AllTeacher.ToArray();
            foreach (var room_schedule in ExaminationRoom_TestSchedule)
            {
                // tạo cặp số ngẫu nhiên
                do
                {
                    rd1 = rnd.Next(_Teacher.Count());
                    rd2 = rnd.Next(_Teacher.Count());
                } while (rd1 == rd2);
                Supervisor supervisor = new Supervisor();
                supervisor.ExaminationRoom_TestSchedule = room_schedule;
                Teacher t1 = _Teacher[rd1];
                Teacher t2 = _Teacher[rd2];
                // delete array _Teacher with rd1 and rd2
                _Teacher = _Teacher.Where(val => val != _Teacher[rd1] && val != _Teacher[rd2]).ToArray();

                List<Teacher> ListTeacher = new List<Teacher>();
                ListTeacher.Add(t1);
                ListTeacher.Add(t2);
                //supervisor.Teachers.(t1);
                supervisor.Teachers = ListTeacher;
                _unitOfWork.SupervisorRepository.Insert(supervisor);
            }
            _unitOfWork.SaveChange();
            return Ok();
        }
    }
}
