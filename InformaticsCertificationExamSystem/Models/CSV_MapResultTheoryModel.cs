using CsvHelper.Configuration;

namespace InformaticsCertificationExamSystem.Models
{
    public class CSV_MapResultTheoryModel : ClassMap<CSV_TheoryResultModel>
    {
        CSV_MapResultTheoryModel()
        {
            Map(p => p.Email).Name("Địa chỉ thư điện tử");
            Map(p => p.Theory).Name("Điểm/10,00");
        }
    }
}
