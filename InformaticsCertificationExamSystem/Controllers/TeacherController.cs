using InformaticsCertificationExamSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using InformaticsCertificationExamSystem.Models;
using InformaticsCertificationExamSystem.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")]
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
            var listTeachers = from teachers in  _unitOfWork.TeacherRepository.GetAll() 
                               where teachers.PermissionId != 2
                               select teachers;

            return Ok(listTeachers.ToList());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var teacher = _unitOfWork.TeacherRepository.GetByID(id);
                var sup = (from te in _unitOfWork.DbContext.Teachers
                          where te.Id == id
                          select te.Supervisors).ToList();
                if (teacher != null)
                {
                    if (sup[0].Count()!=0) return BadRequest("Teacher in schedule");
                }
                _unitOfWork.TeacherRepository.Delete(id);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }
        [Authorize(Roles = "Admin,Teacher")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin(TeacherModel NewTeacher)
        {
            try
            {
                var teacher = _mapper.Map<Teacher>(NewTeacher);
                teacher.PermissionId = 2;
                _unitOfWork.TeacherRepository.Update(teacher);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        [Authorize(Roles = "Admin,Teacher")]
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
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetTeacherInfoByTokenIdExam")]
        public IActionResult GetTeacherInfoByTokenIdExam()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;
            if (claim.Count() == 0) { return BadRequest("Token invalid"); }
            // Gets name from claims. Generally it's an email address.
            var idTeacherClaim = claim
                .Where(x => x.Type == "TeacherId")
                .FirstOrDefault();
            if (idTeacherClaim == null) { return BadRequest("Id teacher null"); }
            var result = _unitOfWork.TeacherRepository.GetByID(Int32.Parse(idTeacherClaim.Value));
            return Ok(_mapper.Map<TeacherModel>(result));
        }
        [HttpPut("{id}/Lock")]
        public IActionResult LockTeacher(int id)
        {
            if (_unitOfWork.TeacherRepository.LockTeacher(id))
            {
                _unitOfWork.SaveChange();
                return Ok();
            }
            return BadRequest("Not found Teacher  ");
        }

        [HttpPut("{id}/UnLock")]
        public IActionResult UnLockTeacher(int id)
        {
            if (_unitOfWork.TeacherRepository.UnLockTeacher(id))
            {
                _unitOfWork.SaveChange();
                return Ok();
            }
            return BadRequest("Not found Teacher  ");
        }
    }
}
