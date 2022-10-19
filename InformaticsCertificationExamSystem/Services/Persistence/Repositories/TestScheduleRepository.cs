using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class TestScheduleRepository : Repository<TestSchedule>, ITestScheduleRepository
    {
        public TestScheduleRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
