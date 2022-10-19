using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class InconsistentMarkRepository : Repository<InconsistentMark>, IInconsistentMarkRepository
    {
        public InconsistentMarkRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
