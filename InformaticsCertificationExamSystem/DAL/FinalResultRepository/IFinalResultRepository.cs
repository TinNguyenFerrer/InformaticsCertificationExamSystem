using InformaticsCertificationExamSystem.Data;
namespace InformaticsCertificationExamSystem.DAL
{
    public interface IFinalResultRepository:IRepository<FinalResult>
    {
        public FinalResult? GetByIdStudent(int Id);
    }
}
