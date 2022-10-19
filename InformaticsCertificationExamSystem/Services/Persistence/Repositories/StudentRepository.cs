using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class StudentRepository: Repository<Student>, IStudentRepository
    {
        public StudentRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) {  }
    }
}
