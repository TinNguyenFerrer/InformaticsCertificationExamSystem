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
        public async Task<IActionResult> DeleteExamination(int id)
        {
            try
            {
                _unitOfWork.ExaminationRoomRepository.Delete(id);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }

        [HttpGet("LockExaminationRoom")]
        public IActionResult LockExaminationRoom(int id)
        {
            if (_unitOfWork.ExaminationRoomRepository.LockExaminationRoom(id))
            {
                _unitOfWork.SaveChange();
                return Ok();
            }
            return BadRequest("Not found Room  ");
        }

        [HttpGet("UnLockExaminationRoom")]
        public IActionResult UnLockExaminationRoom(int id)
        {
            if (_unitOfWork.ExaminationRoomRepository.UnLockExaminationRoom(id))
            {
                _unitOfWork.SaveChange();
                return Ok();
            }
            return BadRequest("Not found Room  ");
        }
    }
}
