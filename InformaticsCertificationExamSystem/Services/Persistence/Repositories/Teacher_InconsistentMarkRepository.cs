using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class Teacher_InconsistentMarkRepository : Repository<Teacher_InconsistentMark>, ITeacher_InconsistentMarkRepository
    {
        public Teacher_InconsistentMarkRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
