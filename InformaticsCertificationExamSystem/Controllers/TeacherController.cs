using InformaticsCertificationExamSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using InformaticsCertificationExamSystem.Models;
using InformaticsCertificationExamSystem.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeacherController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public TeacherController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: api/<TeacherController>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(TeacherModel NewTeacher)
        {
            try
            {
                _unitOfWork.TeacherRepository.Insert(_mapper.Map<Teacher>(NewTeacher));
                _unitOfWork.SaveChange();
            }
            catch (Exception e)
            {
                Console.WriteLine("=====err: " + e);
                return BadRequest(e.Message);
            }
            return Ok();
        }

        //-------============admin===========--------------

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_unitOfWork.TeacherRepository.GetAll().ToList());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                _unitOfWork.TeacherRepository.Delete(id);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            try
            {
                var TeacherInf = _unitOfWork.TeacherRepository.GetByID(id);
                return Ok(_mapper.Map<TeacherModel>(TeacherInf));
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(TeacherModel NewTeacher)
        {
            try
            {
                _unitOfWork.TeacherRepository.Update(_mapper.Map<Teacher>(NewTeacher));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
        }
    }
}
