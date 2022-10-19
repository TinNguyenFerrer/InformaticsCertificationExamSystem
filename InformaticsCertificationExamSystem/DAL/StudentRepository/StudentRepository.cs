using InformaticsCertificationExamSystem.Data;


namespace InformaticsCertificationExamSystem.DAL
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
