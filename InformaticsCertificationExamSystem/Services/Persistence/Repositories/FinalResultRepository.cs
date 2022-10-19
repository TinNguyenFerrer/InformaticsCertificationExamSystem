using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class FinalResultRepository : Repository<FinalResult>, IFinalResultRepository
    {
        public FinalResultRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
