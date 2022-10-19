using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
