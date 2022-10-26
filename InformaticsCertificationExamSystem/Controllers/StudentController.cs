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
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudent()
        {
            var AllStudent = _unitOfWork.StudentRepository.GetAll().ToList();
            return Ok(AllStudent);
        }

        [HttpGet("GetAllByIdExamination")]
        public async Task<IActionResult> GetAllStudentByIdExamination(int id)
        {
            var AllStudents = _unitOfWork.StudentRepository.GetAllByIdExamination(id);
            return Ok(AllStudents.ToArray());
        }
        [HttpGet("GetAllByIdTestSchedule")]
        //public async Task<IActionResult> GetAllByIdTestSchedule(int id)
        //{
        //    var AllStudents = _unitOfWork.StudentRepository.GetAllByIdTestSchedule(id);
        //    return Ok(AllStudents.ToArray());
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {

            var Student = _unitOfWork.StudentRepository.GetByID(id);
            if (Student == null)
            {
                return NotFound();
            }
            return Ok(Student);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNew(StudentModel NewStudent)
        {
            try
            {
                //Console.WriteLine(NewExamination.GradingDeadline);
                _unitOfWork.StudentRepository.Insert(_mapper.Map<Student>(NewStudent));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentModel StudentUpdate)
        {
            try
            {
                _unitOfWork.StudentRepository.Update(_mapper.Map<Student>(StudentUpdate));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                _unitOfWork.StudentRepository.Delete(id);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }
        [HttpGet("GetAllByRoomAndTestSchedule")]
        public IActionResult GetAllByRoomAndTestSchedule(int ExamRom_TestScheid)
        {
            var Students = from student in _unitOfWork.DbContext.Students
                           where student.ExaminationRoom_TestScheduleId == ExamRom_TestScheid
                           select student;
            return Ok(Students);
        }
    }
}
