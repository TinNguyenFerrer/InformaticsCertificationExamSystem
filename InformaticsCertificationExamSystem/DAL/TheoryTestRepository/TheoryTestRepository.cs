using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class TheoryTestRepository : Repository<TheoryTest>, ITheoryTestRepository
    {
        public TheoryTestRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
