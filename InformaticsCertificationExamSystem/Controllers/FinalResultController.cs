using AutoMapper;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;
using InformaticsCertificationExamSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class FinalResultController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly ICSVService _csvService;

        public FinalResultController(IUnitOfWork unitOfWork, IMapper mapper, ICSVService csvService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _csvService = csvService;
        }

        [HttpPut("import-theoretical-mark")]
        public IActionResult ImportTheoreticalMark(IFormFile file)
        {
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.UTF8, // File dùng encoding UTF-8
                    Delimiter = "," // Ký tự phân cách giữa các trường
                };
                var textReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
                var csv = new CsvReader(textReader, configuration);
                csv.Context.RegisterClassMap<CSV_MapResultTheoryModel>();
                var data = csv.GetRecords<CSV_TheoryResultModel>();
                Console.WriteLine("-----------------Ket qua map ly thuyet:----------------");
                List<CSV_TheoryResultModel> dataResult = new List<CSV_TheoryResultModel>();
                foreach (var t in data.ToList())
                {
                    if (t.Theory != "-" && (t.Email.Length != 0))
                    {
                        dataResult.Add(t);
                    }
                }
                dataResult.ToList().ForEach(i => Console.WriteLine($"Email:  {i.Email}\t ly thuyet{i.Theory}"));
                foreach (var t in dataResult)
                {
                    var student = _unitOfWork.StudentRepository.GetByEmail(t.Email);
                    var exam = _unitOfWork.ExaminationRepository.GetByID(student.ExaminationId);
                    if (student == null) continue;
                    FinalResult resultStudent = _unitOfWork.FinalResultRepository.GetByIdStudent(student.Id);
                    var examinationUpdate = _unitOfWork.ExaminationRepository.GetByID(student.ExaminationId);
                    if (examinationUpdate != null)
                    {
                        examinationUpdate.IsEnterScore = true;
                        _unitOfWork.ExaminationRepository.Update(examinationUpdate);
                    }
                    if (resultStudent == null) continue;
                    //Format float :","
                    var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.NumberDecimalSeparator = ",";

                    resultStudent.Theory = double.Parse(t.Theory, ci);
                    resultStudent.FinalMark = (resultStudent.Practice + resultStudent.Theory) / 2;
                   
                    if (resultStudent.FinalMark > 10) return BadRequest("Finalmark more than 10");
                    if (resultStudent.Theory > 10) return BadRequest("Theory more than 10");

                    if (resultStudent.Theory >= exam.MinimumTheoreticalMark && resultStudent.Practice >= exam.MinimumPracticeMark)
                    {
                        resultStudent.ResultStatus = true;
                    }
                    else
                    {
                        resultStudent.ResultStatus = false;
                    }
                    _unitOfWork.FinalResultRepository.Update(resultStudent);
                }
                //dataResult.ToList().ForEach(i => Console.WriteLine($"Email:  {i.Email}\t ly thuyet{i.Practice}"));
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut("import-practice-mark")]
        public IActionResult ImportPracticeMark(IFormFile file)
        {
            try
            {
                var workbook = new XLWorkbook(file.OpenReadStream());
                IXLWorksheet worksheet;
                if (!workbook.TryGetWorksheet("Scorecard", out worksheet))
                {
                    return NotFound("Worksheet not found");
                }
                var codeColumn = worksheet.Column("A").ColumnUsed();
                var wordColumn = worksheet.Column("B").ColumnUsed();
                var excelColumn = worksheet.Column("C").ColumnUsed();
                var powerPointColumn = worksheet.Column("D").ColumnUsed();
                var windowColumn = worksheet.Column("E").ColumnUsed();
                var resultColumn = worksheet.Column("F").ColumnUsed();
                var t = codeColumn.CellCount();
                for (int i = 2; i <= codeColumn.CellCount(); i++)
                {
                    var resultStudent = (from student in _unitOfWork.StudentRepository.GetAll()
                                         join finalResult in _unitOfWork.FinalResultRepository.GetAll()
                                         on student.FinalResultId equals finalResult.Id
                                         where student.HashCode == codeColumn.Cell(i).Value.ToString()
                                         select finalResult).FirstOrDefault();
                    if (resultStudent == null) continue;

                    var students = _unitOfWork.StudentRepository.GetByHashCode(codeColumn.Cell(i).Value.ToString());
                    if (students == null) continue;

                    var exam = _unitOfWork.ExaminationRepository.GetByID(students.ExaminationId);
                    if (exam != null)
                    {
                        exam.IsEnterScore = true;
                        _unitOfWork.ExaminationRepository.Update(exam);
                    }

                    double wordMark = 0;
                    double excelMark = 0;
                    double powerPointMark = 0;
                    double windowMark = 0;
                    double resultMark = 0;
                    //conver cell value to mark
                    if (wordColumn.Cell(i).Value.ToString().Length != 0)
                        wordMark = double.Parse(wordColumn.Cell(i).Value.ToString());

                    if (excelColumn.Cell(i).Value.ToString().Length != 0)
                        excelMark = double.Parse(excelColumn.Cell(i).Value.ToString());

                    if (powerPointColumn.Cell(i).Value.ToString().Length != 0)
                        powerPointMark = double.Parse(powerPointColumn.Cell(i).Value.ToString());

                    if (windowColumn.Cell(i).Value.ToString().Length != 0)
                        windowMark = double.Parse(windowColumn.Cell(i).Value.ToString());

                    if (resultColumn.Cell(i).Value.ToString().Length != 0)
                        resultMark = double.Parse(resultColumn.Cell(i).Value.ToString());

                    //double.TryParse(wordColumn.Cell(i).Value.ToString(), out wordMark);
                    //double.TryParse(excelColumn.Cell(i).Value.ToString(), out excelMark);
                    //double.TryParse(powerPointColumn.Cell(i).Value.ToString(), out powerPointMark);
                    //double.TryParse(windowColumn.Cell(i).Value.ToString(), out windowMark);
                    //double.TryParse(resultColumn.Cell(i).Value.ToString(), out resultMark);

                    resultStudent.Word = wordMark;
                    resultStudent.Excel = excelMark;
                    resultStudent.PowerPoint = powerPointMark;
                    resultStudent.Window = windowMark;
                    resultStudent.Practice = resultMark;
                    resultStudent.FinalMark = (resultStudent.Practice + resultStudent.Theory) / 2;

                    if (resultStudent.FinalMark > 10) return BadRequest("Finalmark more than 10");
                    if (resultStudent.Practice > 10) return BadRequest("Practice more than 10");

                    if (resultStudent.Theory >= exam.MinimumTheoreticalMark && resultStudent.Practice >= exam.MinimumPracticeMark)
                    {
                        resultStudent.ResultStatus = true;
                    }
                    else
                    {
                        resultStudent.ResultStatus = false;
                    }
                    _unitOfWork.FinalResultRepository.Update(resultStudent);
                    Console.WriteLine("--------======================");
                    Console.WriteLine($"word: {wordMark}");
                    Console.WriteLine($"excel: {excelMark}");
                    Console.WriteLine($"powerpoin: {powerPointMark}");
                    Console.WriteLine($"windoww: {windowMark}");
                    Console.WriteLine($"result: {resultMark}");
                    //Console.WriteLine("||");
                    //Console.WriteLine("|lenght:" + excelColumn.Cell(i).Value.ToString().Length + "|");
                    //Console.WriteLine("|" + excelColumn.Cell(i).Value + "|");
                }
                _unitOfWork.SaveChange();
                return Ok();
            }catch (Exception e){
                return BadRequest(e);
            }
        }
    }
}
