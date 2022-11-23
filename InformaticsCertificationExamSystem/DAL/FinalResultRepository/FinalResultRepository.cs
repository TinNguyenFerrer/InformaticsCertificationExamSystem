using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class FinalResultRepository : Repository<FinalResult>, IFinalResultRepository
    {
        public FinalResultRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
        public FinalResult? GetByIdStudent(int Id)
        {
            var result = from students in this.DbContext.Students.AsEnumerable()
                         join finalResult in this.GetAll()
                         on students.FinalResultId equals finalResult.Id
                         where students.Id == Id
                         select finalResult;
            return result.FirstOrDefault();
        }
    }
}
