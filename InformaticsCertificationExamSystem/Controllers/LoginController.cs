using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using InformaticsCertificationExamSystem.Models;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public LoginController(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("CreateTokenTeacher")]

        public IActionResult CreateTokenTeacher(UserLoginModel user)
        {
            var Teacher = (from teachers in _unitOfWork.TeacherRepository.GetAll()
                               where teachers.IdentifierCode == user.username && teachers.Password == user.password
                           select teachers);
            if (!Teacher.Any())
            {
                return BadRequest("Login False");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub ,_configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("TeacherId", Teacher.First().Id.ToString()),
                new Claim(ClaimTypes.Role,"User")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(3),
                        signingCredentials: signIn);


            var TokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new
            {
                token = TokenResult,
                permission = "admin"
            });
        }

        [HttpPost("CreateTokenStudent")]

        public IActionResult CreateTokenStudent(UserLoginModel user)
        {
            var students = (from student in _unitOfWork.StudentRepository.GetAll()
                               where student.IdentifierCode == user.username && student.Password == user.password
                           select student);
            if (!students.Any())
            {
                return BadRequest("Login False");
            }
            if (students.FirstOrDefault().Locked)
            {
                return BadRequest("Student locked");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub ,_configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("StudentId", students.First().Id.ToString()),
                new Claim(ClaimTypes.Role,"User")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(3),
                        signingCredentials: signIn);


            var TokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new
            {
                token = TokenResult,
                permission = "admin"
            });
        }
    }
}
