using AutoMapper;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using InformaticsCertificationExamSystem.DAL;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;
using InformaticsCertificationExamSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace InformaticsCertificationExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                    if (student == null) continue;
                    FinalResult resultStudent = _unitOfWork.FinalResultRepository.GetByIdStudent(student.Id);
                    if (resultStudent == null) continue;
                    //Format float :","
                    var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.NumberDecimalSeparator = ",";

                    resultStudent.Theory = double.Parse(t.Theory, ci);
                    resultStudent.FinalMark = resultStudent.Practice + resultStudent.Theory;
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
            for (int i = 2; i <= codeColumn.CellCount(); i++)
            {
                var resultStudent = (from student in _unitOfWork.StudentRepository.GetAll()
                                     join finalResult in _unitOfWork.FinalResultRepository.GetAll()
                                     on student.FinalResultId equals finalResult.Id
                                     where student.HashCode == codeColumn.Cell(i).Value.ToString()
                                     select finalResult).FirstOrDefault();
                if (resultStudent == null) continue;

                double wordMark = 0;
                double excelMark = 0;
                double powerPointMark = 0;
                double windowMark = 0;
                double resultMark = 0;
                //conver cell value to mark
                double.TryParse(wordColumn.Cell(i).Value.ToString(), out wordMark);
                double.TryParse(excelColumn.Cell(i).Value.ToString(), out excelMark);
                double.TryParse(powerPointColumn.Cell(i).Value.ToString(), out powerPointMark);
                double.TryParse(windowColumn.Cell(i).Value.ToString(), out windowMark);
                double.TryParse(resultColumn.Cell(i).Value.ToString(), out resultMark);

                resultStudent.Word = wordMark;
                resultStudent.Excel = excelMark;
                resultStudent.PowerPoint = powerPointMark;
                resultStudent.Window = windowMark;
                resultStudent.Practice = resultMark;
                resultStudent.FinalMark = resultStudent.Practice + resultStudent.Theory;
                _unitOfWork.FinalResultRepository.Update(resultStudent);
                //Console.WriteLine("--------======================");
                //Console.WriteLine(codeColumn.Cell(i));
                //Console.WriteLine("||");
                //Console.WriteLine("|" + excelColumn.Cell(i).Value.ToString().Length + "|");
                //Console.WriteLine("|"+excelColumn.Cell(i).Value+"|");
            }
            _unitOfWork.SaveChange();
            return Ok();
        }
    }
}
