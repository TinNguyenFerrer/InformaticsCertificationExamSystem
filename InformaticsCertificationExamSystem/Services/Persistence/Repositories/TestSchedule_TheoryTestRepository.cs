using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class TestSchedule_TheoryTestRepository : Repository<TestSchedule_TheoryTest>, ITestSchedule_TheoryTestRepository
    {
        public TestSchedule_TheoryTestRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
