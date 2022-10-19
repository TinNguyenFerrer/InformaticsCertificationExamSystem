using InformaticsCertificationExamSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using InformaticsCertificationExamSystem.Models;
using InformaticsCertificationExamSystem.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private readonly IUnitOfWork _UnitOfWork;
        public TeacherController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        // GET: api/<TeacherController>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Teacher NewTeacher)
        {
            try
            {
                
                _UnitOfWork.TeacherRepository.Insert(NewTeacher);
                _UnitOfWork.SaveChange();
            }catch(Exception e)
            {
                Console.WriteLine("=====err: "+e);
                return BadRequest(e.Message);
            }
            return Ok(NewTeacher);
        }

       
    }
}
