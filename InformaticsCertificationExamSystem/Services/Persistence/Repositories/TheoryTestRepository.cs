using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class TheoryTestRepository : Repository<TheoryTest>, ITheoryTestRepository
    {
        public TheoryTestRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
