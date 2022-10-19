using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class TestScheduleRepository : Repository<TestSchedule>, ITestScheduleRepository
    {
        public TestScheduleRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
