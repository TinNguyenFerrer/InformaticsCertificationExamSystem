using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class TestSchedule_TheoryTestRepository : Repository<TestSchedule_TheoryTest>, ITestSchedule_TheoryTestRepository
    {
        public TestSchedule_TheoryTestRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
