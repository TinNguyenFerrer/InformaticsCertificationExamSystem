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
using Syncfusion.XlsIO;
using System.Globalization;
using System.Text;

namespace InformaticsCertificationExamSystem.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly ICSVService _csvService;
        public ExaminationController(IUnitOfWork unitOfWork, IMapper mapper, ICSVService csvService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _csvService = csvService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllExamination()
        {
            var AllExamination = _unitOfWork.ExaminationRepository.GetAll().ToList();
            return Ok(AllExamination.ToArray());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamination(int id)
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
                var t = _mapper.Map<Examination>(NewExamination);
                t.ExamCode = SummaryService.IntToBase32(_unitOfWork.DbContext.Examinations.Max(x => x.Id)) + _unitOfWork.DbContext.Examinations.Max(x => x.Id);
                _unitOfWork.ExaminationRepository.Insert(t);
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
            try
            {
                _unitOfWork.ExaminationRepository.Update(_mapper.Map<Examination>(examination));
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
                _unitOfWork.ExaminationRepository.Delete(id);
                Console.WriteLine("Xóa bài thi: " + id);
                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("can not delete!");
            }
        }

        [HttpPut("{examId}/CreateScorecard")]
        public async Task<IActionResult> CreateScorecard(int examId)
        {
            try
            {
                var examUpdate = _unitOfWork.ExaminationRepository.GetByID(examId);
                if (examUpdate == null) return NotFound(" Exam id not found");
                examUpdate.IsCreateScorecard = true;
                _unitOfWork.ExaminationRepository.Update(examUpdate);
                var room_schedules = from testschedule in _unitOfWork.TestScheduleRepository.GetAll()
                                     join room_schedule in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                                     on testschedule.Id equals room_schedule.TestScheduleId
                                     where testschedule.ExaminationId == examId
                                     select room_schedule;
                if (!room_schedules.Any()) return BadRequest("examId does not have room ");
                foreach (var room_schedule in room_schedules)
                {
                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "FileSubmit"));

                    path = Path.Combine(path, room_schedule.TestScheduleId.ToString());
                    if (!Directory.Exists(Path.GetFullPath(path)))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = Path.Combine(path, SummaryService.IntToBase32(room_schedule.ExaminationRoomId + room_schedule.TestScheduleId + 12));
                    if (!Directory.Exists(Path.GetFullPath(path)))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var stude = (from student in _unitOfWork.StudentRepository.GetAll()
                                 where student.ExaminationRoom_TestScheduleId == room_schedule.Id
                                 select student).OrderBy(item => item.HashCode);
                    //create file excel
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Scorecard");

                        worksheet.Cell("A1").Value = "Code";
                        worksheet.Cell("B1").Value = "Word";
                        worksheet.Cell("C1").Value = "Excel";
                        worksheet.Cell("D1").Value = "PowerPoint";
                        worksheet.Cell("E1").Value = "Window";
                        worksheet.Cell("F1").Value = "Result";
                        //Background color
                        worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(0, 172, 78);
                        worksheet.Cell("B1").Style.Fill.BackgroundColor = XLColor.FromArgb(0, 172, 78);
                        worksheet.Cell("C1").Style.Fill.BackgroundColor = XLColor.FromArgb(0, 172, 78);
                        worksheet.Cell("D1").Style.Fill.BackgroundColor = XLColor.FromArgb(0, 172, 78);
                        worksheet.Cell("E1").Style.Fill.BackgroundColor = XLColor.FromArgb(0, 172, 78);
                        worksheet.Cell("F1").Style.Fill.BackgroundColor = XLColor.FromArgb(0, 172, 78);
                        //Border
                        worksheet.Cell("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell("A1").Style.Border.OutsideBorderColor = XLColor.Black;

                        worksheet.Cell("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell("B1").Style.Border.OutsideBorderColor = XLColor.Black;

                        worksheet.Cell("C1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell("C1").Style.Border.OutsideBorderColor = XLColor.Black;

                        worksheet.Cell("D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell("D1").Style.Border.OutsideBorderColor = XLColor.Black;

                        worksheet.Cell("E1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell("E1").Style.Border.OutsideBorderColor = XLColor.Black;

                        worksheet.Cell("F1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell("F1").Style.Border.OutsideBorderColor = XLColor.Black;
                        int k = 2;
                        foreach (var student in stude)
                        {
                            var pathStudentSubmitFile = Path.Combine(path, student.HashCode.ToString());
                            if (!Directory.Exists(Path.GetFullPath(pathStudentSubmitFile)))
                            {
                                Directory.CreateDirectory(pathStudentSubmitFile);
                            }

                            worksheet.Cell("A" + k).Value = student.HashCode;
                            worksheet.Cell("F" + k).FormulaA1 = $"=SUM(B{k},C{k},D{k},E{k})";
                            worksheet.Cell("F" + k).Style.Font.Bold = true;
                            worksheet.Cell("F" + k).Style.Font.FontColor = XLColor.Red;
                            //border
                            worksheet.Range($"A{k}:F{k}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Range($"A{k}:F{k}").Style.Border.InsideBorderColor = XLColor.Black;
                            worksheet.Range($"A{k}:F{k}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Range($"A{k}:F{k}").Style.Border.OutsideBorderColor = XLColor.Black;
                            k++;
                        }
                        workbook.SaveAs(Path.Combine(path, "ScoreCard.xlsx"));
                        //MemoryStream stream = new MemoryStream();

                        //workbook.SaveAs(stream);

                        //Set the position as '0'.
                        //stream.Position = 0;
                        //save file
                        //FileStream file = new FileStream(Path.Combine(path, "ScoreCard.xlsx"), FileMode.Create, FileAccess.Write);
                        //stream.WriteTo(file);
                        //file.Close();
                        //stream.Close();
                        //using (var fileStream = new FileStream(Path.Combine(path, "ScoreCard.xlsx"), FileMode.Create))
                        //{
                        //    await stream.WriteTo(fileStream);
                        //}
                    }
                }

                _unitOfWork.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}/TestSchedules")]
        public IActionResult GetAllTestSchedules(int id)
        {
            try
            {
                var testSchedules = _mapper.Map<IEnumerable<TestScheduleModel>>(_unitOfWork.TestScheduleRepository.GetAllByIdExamination(id));
                return Ok(testSchedules);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("{id}/student-accounts-for-export")]
        public IActionResult GetStudentsForExport(int id)
        {
            try
            {
                var allStudents = from students in _unitOfWork.StudentRepository.GetAllByIdExamination(id)
                                  join examRoom_testSchedule in _unitOfWork.ExaminationRoom_TestScheduleRepository.GetAll()
                                  on students.ExaminationRoom_TestScheduleId equals examRoom_testSchedule.Id
                                  join testSchedule in _unitOfWork.TestScheduleRepository.GetAll()
                                  on examRoom_testSchedule.TestScheduleId equals testSchedule.Id
                                  join examination in _unitOfWork.ExaminationRepository.GetAll()
                                  on id equals examination.Id
                                  select new
                                  {
                                      userName = students.IdentifierCode.ToLower(),
                                      password = students.Password,
                                      name = students.Name,
                                      email = students.Email,
                                      testScheduleName = testSchedule.Name.Replace(" ", ""),
                                      examCode = examination.ExamCode
                                  };
                return Ok(allStudents.ToArray());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("{id}/get-marks")]
        public IActionResult GetAllMarkByIdExam(int id)
        {
            try
            {
                var result = from students in _unitOfWork.StudentRepository.GetAllByIdExamination(id)
                             join finalresult in _unitOfWork.FinalResultRepository.GetAll()
                             on students.FinalResultId equals finalresult.Id
                             select new
                             {
                                 students.Id,
                                 students.Name,
                                 students.IdentifierCode,
                                 finalresult.Practice,
                                 finalresult.Theory,
                                 finalresult.Excel,
                                 finalresult.PowerPoint,
                                 finalresult.Word,
                                 finalresult.FinalMark,
                                 finalresult.ResultStatus
                             };
                return Ok(result.ToArray());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        //[HttpPut("import-theoretical-mark")]
        //public IActionResult ImportTheoreticalMark(IFormFile file)
        //{
        //    try
        //    {
        //        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        //        {
        //            Encoding = Encoding.UTF8, // File dùng encoding UTF-8
        //            Delimiter = "," // Ký tự phân cách giữa các trường
        //        };
        //        var textReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
        //        var csv = new CsvReader(textReader, configuration);
        //        csv.Context.RegisterClassMap<CSV_MapResultTheoryModel>();
        //        var data = csv.GetRecords<CSV_TheoryResultModel>();
        //        Console.WriteLine("-----------------Ket qua map ly thuyet:----------------");
        //        List<CSV_TheoryResultModel> dataResult = new List<CSV_TheoryResultModel>();
        //        foreach (var t in data.ToList())
        //        {
        //            if (t.Theory != "-"&&(t.Email.Length!=0))
        //            {
        //                dataResult.Add(t);
        //            }
        //        }
        //        dataResult.ToList().ForEach(i => Console.WriteLine($"Email:  {i.Email}\t ly thuyet{i.Theory}"));
        //        foreach (var t in dataResult)
        //        {
        //            var student = _unitOfWork.StudentRepository.GetByEmail(t.Email);
        //            if (student == null) continue;
        //            FinalResult resultStudent = _unitOfWork.FinalResultRepository.GetByIdStudent(student.Id);
        //            if (resultStudent == null) continue;
        //            //Format float :","
        //            var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        //            ci.NumberFormat.NumberDecimalSeparator = ",";

        //            resultStudent.Theory = double.Parse(t.Theory,ci);
        //            resultStudent.FinalMark = resultStudent.Practice + resultStudent.Theory;
        //            _unitOfWork.FinalResultRepository.Update(resultStudent);
        //        }
        //        //dataResult.ToList().ForEach(i => Console.WriteLine($"Email:  {i.Email}\t ly thuyet{i.Practice}"));
        //        _unitOfWork.SaveChange();
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}
        //[HttpPut("import-practice-mark")]
        //public IActionResult ImportPracticeMark(IFormFile file)
        //{
        //    var workbook = new XLWorkbook(file.OpenReadStream());
        //    IXLWorksheet worksheet;
        //    if (!workbook.TryGetWorksheet("Scorecard", out worksheet))
        //    {
        //        return NotFound("Worksheet not found");
        //    }
        //    var codeColumn = worksheet.Column("A").ColumnUsed();
        //    var wordColumn = worksheet.Column("B").ColumnUsed();
        //    var excelColumn = worksheet.Column("C").ColumnUsed();
        //    var powerPointColumn = worksheet.Column("D").ColumnUsed();
        //    var windowColumn = worksheet.Column("E").ColumnUsed();
        //    var resultColumn = worksheet.Column("F").ColumnUsed();
        //    for (int i = 2; i <= codeColumn.CellCount(); i++)
        //    {
        //        var resultStudent = (from student in _unitOfWork.StudentRepository.GetAll()
        //                             join finalResult in _unitOfWork.FinalResultRepository.GetAll()
        //                             on student.FinalResultId equals finalResult.Id
        //                             where student.HashCode == codeColumn.Cell(i).Value.ToString()
        //                             select finalResult).FirstOrDefault();
        //        if (resultStudent == null) continue;

        //        double wordMark = 0;
        //        double excelMark = 0;
        //        double powerPointMark = 0;
        //        double windowMark = 0;
        //        double resultMark = 0;
        //        //conver cell value to mark
        //        double.TryParse(wordColumn.Cell(i).Value.ToString(),out wordMark);
        //        double.TryParse(excelColumn.Cell(i).Value.ToString(), out excelMark);
        //        double.TryParse(powerPointColumn.Cell(i).Value.ToString(), out powerPointMark);
        //        double.TryParse(windowColumn.Cell(i).Value.ToString(), out windowMark);
        //        double.TryParse(resultColumn.Cell(i).Value.ToString(), out resultMark);

        //        resultStudent.Word = wordMark;
        //        resultStudent.Excel = excelMark ;
        //        resultStudent.PowerPoint = powerPointMark ;
        //        resultStudent.Window = windowMark ;
        //        resultStudent.Practice = resultMark ;
        //        resultStudent.FinalMark = resultStudent.Practice + resultStudent.Theory;
        //        _unitOfWork.FinalResultRepository.Update(resultStudent);
        //        //Console.WriteLine("--------======================");
        //        //Console.WriteLine(codeColumn.Cell(i));
        //        //Console.WriteLine("||");
        //        //Console.WriteLine("|" + excelColumn.Cell(i).Value.ToString().Length + "|");
        //        //Console.WriteLine("|"+excelColumn.Cell(i).Value+"|");
        //    }
        //    _unitOfWork.SaveChange();
        //    return Ok();
        //}
    }
}
