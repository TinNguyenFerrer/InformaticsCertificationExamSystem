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
        [HttpGet("GetAllByIdExamination")]
        public async Task<IActionResult> GetAllRoomByIdExamination(int id)
        {
            var AllRoom = _unitOfWork.TestScheduleRepository.GetAllByIdExamination(id).ToArray();
            return Ok(AllRoom);
        }
        //======================--------------------------------------=====================
        [HttpPost("AutoCreateTestSchedule")]
        public async Task<IActionResult> AutoCreateTestSchedule(int IdExam)
        {
            


            return Ok();
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
        [HttpPost]
        public async Task<IActionResult> CreateNew(TestScheduleModel NewTestSchedule)
        {
            try
            {
                //Console.WriteLine(NewExamination.GradingDeadline);
                _unitOfWork.TestScheduleRepository.Insert(_mapper.Map<TestSchedule>(NewTestSchedule));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateExaminationRoom(TestScheduleModel TestSchedule)
        {
            try
            {
                _unitOfWork.TestScheduleRepository.Update(_mapper.Map<TestSchedule>(TestSchedule));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamination(int id)
        {
            try
            {
                _unitOfWork.TestScheduleRepository.Delete(id);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }
    }
}
