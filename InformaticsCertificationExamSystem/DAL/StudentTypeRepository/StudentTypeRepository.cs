using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class StudentTypeRepository : Repository<StudentType>, IStudentTypeRepository
    {
        public StudentTypeRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
