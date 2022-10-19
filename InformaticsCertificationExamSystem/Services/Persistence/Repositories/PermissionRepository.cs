using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
