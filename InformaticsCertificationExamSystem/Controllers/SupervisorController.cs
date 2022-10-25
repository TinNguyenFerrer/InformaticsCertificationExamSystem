using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> AutoCreateSupervisor(int IdExam)
        {
            //var AllTestSchedule = _unitOfWork.TestScheduleRepository.GetAllByIdExamination(IdExam);
            //var AllTeacher = _unitOfWork.TeacherRepository.GetAll();
            //Random rnd = new Random();
            //int rd1;
            //int rd2;
            //if (AllTeacher.Count() < 2) return BadRequest("less than 2 teacher");
            //do
            //{
            //    rd1 = rnd.Next(AllTeacher.Count());
            //    rd2 = rnd.Next(AllTeacher.Count());
            // } while (rd1 == rd2);
            //foreach (var testschedule in AllTestSchedule)
            //{
            //    var _Teacher = AllTeacher.ToArray();
            //    Supervisor supervisor = new Supervisor();
            //    supervisor.IdTestSchedule = testschedule.Id;
            //    supervisor.TestSchedule = testschedule;
            //    Teacher t1 = _Teacher[rd1];
            //    Teacher t2 = _Teacher[rd2];
            //    List<Teacher> ListT = new List<Teacher>();
            //    ListT.Add(t1);
            //    ListT.Add(t2);
            //    //supervisor.Teachers.(t1);
            //    supervisor.Teachers = ListT;
            //    _unitOfWork.SupervisorRepository.Insert(supervisor);
            //}
            //_unitOfWork.SaveChange();
            return Ok();
        }
    }
}
