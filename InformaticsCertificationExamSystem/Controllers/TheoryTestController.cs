using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheoryTestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public TheoryTestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("getAllByIdExam")]
        public IActionResult getAllByIdExam(int IdTest)
        {
            var ListTheoryTest = (from theorytest in _unitOfWork.DbContext.TheoryTests
                                  where theorytest.Examination.Id == IdTest
                                  select theorytest).ToList();
            return Ok(ListTheoryTest);
        }
        [HttpPost("UploadFileTheoryTest")]
        public async Task<IActionResult> UploadFileTheoryTest(IFormFile file, int ExaminationId, string Name)
        {
            string path = "";
            Console.WriteLine("================================");
            Console.WriteLine(file.ContentType);
            Console.WriteLine("Id kì thi " + ExaminationId);
            Console.WriteLine("tên bài thi " + Name);
            try
            {
                TheoryTest TheoryTest = new TheoryTest();
                var Exam = _unitOfWork.ExaminationRepository.GetByID(ExaminationId);
                TheoryTest.Examination = Exam;
                TheoryTest.Name = Name;
                _unitOfWork.TheoryTestRepository.Insert(TheoryTest);
                _unitOfWork.SaveChange();

                if (file.Length > 0 && file.ContentType == "application/pdf")
                {

                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, TheoryTest.Id + ".pdf"), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest("File Empty");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
        [HttpGet("DownloadPdfFile")]
        public IActionResult DownloadPdfFile(int id)
        {
            try
            {
                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                string mimeType = "application/pdf";
                path = Path.Combine(path, id + ".pdf");
                var stream = System.IO.File.ReadAllBytes(path);
                return File(stream, mimeType, id + ".pdf");
            }
            catch (Exception e)
            {
                return BadRequest("file not found");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTheoryTest(int id)
        {
            try
            {
                _unitOfWork.TheoryTestRepository.Delete(id);
                _unitOfWork.SaveChange();
                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFilesTheory"));
                if (System.IO.File.Exists(Path.Combine(path, id + ".pdf")))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(Path.Combine(path, id + ".pdf"));
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
