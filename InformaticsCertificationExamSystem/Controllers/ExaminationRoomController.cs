using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class ExaminationRoomController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public ExaminationRoomController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRoom()
        {
            var AllRoom = _unitOfWork.ExaminationRoomRepository.GetAll().ToList();
            return Ok(AllRoom);
        }
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
        [HttpPost]
        public async Task<IActionResult> CreateNew(ExaminationRoomModel NewRoom)
        {
            try
            {
                //Console.WriteLine(NewExamination.GradingDeadline);
                _unitOfWork.ExaminationRoomRepository.Insert(_mapper.Map<ExaminationRoom>(NewRoom));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateExaminationRoom(ExaminationRoomModel Examinationroom)
        {
            try {
                _unitOfWork.ExaminationRoomRepository.Update(_mapper.Map<ExaminationRoom>(Examinationroom));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExaminationRoom(int id)
        {
            try
            {
                var t = (from room in _unitOfWork.ExaminationRoomRepository.GetAll()
                         join exam_Schedu in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                         on room.Id equals exam_Schedu.ExaminationRoomId
                         join schedule in _unitOfWork.TestScheduleRepository.GetAll()
                         on exam_Schedu.TestScheduleId equals schedule.Id
                         join exam in _unitOfWork.ExaminationRepository.GetAll()
                         on schedule.ExaminationId equals exam.Id
                         where room.Id == id
                         select new
                         {
                             exam.Name,
                             exam.Id,
                             exam.ExamCode
                         }).ToList();
                if (t.Count() != 0) return BadRequest(new
                {
                    code = 405,
                    Message = "Room used by Examination",
                    Examination = t
                });
                _unitOfWork.ExaminationRoomRepository.Delete(id);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }

        [HttpPut("LockExaminationRoom")]
        public IActionResult LockExaminationRoom(int id)
        {
            if (_unitOfWork.ExaminationRoomRepository.LockExaminationRoom(id))
            {
                _unitOfWork.SaveChange();
                return Ok();
            }
            return BadRequest("Not found Room  ");
        }

        [HttpPut("UnLockExaminationRoom")]
        public IActionResult UnLockExaminationRoom(int id)
        {
            if (_unitOfWork.ExaminationRoomRepository.UnLockExaminationRoom(id))
            {
                _unitOfWork.SaveChange();
                return Ok();
            }
            return BadRequest("Not found Room  ");
        }
        [HttpGet("GetAllByIdTestSchedule")]
        public IActionResult GetAllByIdTestSchedule(int idTestSchedule)
        {
            var Room = from room_schedule in _unitOfWork.DbContext.ExaminationRoom_TestSchedule
                       where room_schedule.TestSchedule.Id == idTestSchedule
                       select new { 
                           room_schedule.ExaminationRoom,
                           room_schedule.Supervisor.Teachers
                       };
            return Ok(Room);
                       
        }
    }
}
