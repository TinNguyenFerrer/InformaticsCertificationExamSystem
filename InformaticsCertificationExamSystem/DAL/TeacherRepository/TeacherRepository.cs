using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
