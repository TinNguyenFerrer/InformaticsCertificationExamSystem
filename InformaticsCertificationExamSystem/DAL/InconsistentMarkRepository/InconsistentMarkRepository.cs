using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class InconsistentMarkRepository : Repository<InconsistentMark>, IInconsistentMarkRepository
    {
        public InconsistentMarkRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
