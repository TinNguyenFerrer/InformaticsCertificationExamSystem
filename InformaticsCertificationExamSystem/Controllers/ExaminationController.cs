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
    public class ExaminationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public ExaminationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllExamination()
        {
            var AllExamination = _unitOfWork.ExaminationRepository.GetAll().ToList();
            return Ok(AllExamination);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllExamination(int id)
        {

            var Examination = _unitOfWork.ExaminationRepository.GetByID(id);
            if (Examination == null)
            {
                return NotFound();
            }
            return Ok(Examination);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNew(ExaminationModel NewExamination)
        {
            try
            {
                //Console.WriteLine(NewExamination.GradingDeadline);
                _unitOfWork.ExaminationRepository.Insert(_mapper.Map<Examination>(NewExamination));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateExamination(ExaminationModel examination)
        {
            try {
                _unitOfWork.ExaminationRepository.Update(_mapper.Map<Examination>(examination));
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
                _unitOfWork.ExaminationRepository.Delete(id);
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
