using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class Teacher_InconsistentMarkRepository : Repository<Teacher_InconsistentMark>, ITeacher_InconsistentMarkRepository
    {
        public Teacher_InconsistentMarkRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
