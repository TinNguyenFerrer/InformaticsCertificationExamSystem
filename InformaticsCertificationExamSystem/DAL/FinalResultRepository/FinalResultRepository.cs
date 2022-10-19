using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class FinalResultRepository : Repository<FinalResult>, IFinalResultRepository
    {
        public FinalResultRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
