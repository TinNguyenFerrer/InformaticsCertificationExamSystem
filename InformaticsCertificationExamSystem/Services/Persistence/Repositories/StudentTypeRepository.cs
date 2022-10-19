using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class StudentTypeRepository : Repository<StudentType>, IStudentTypeRepository
    {
        public StudentTypeRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
