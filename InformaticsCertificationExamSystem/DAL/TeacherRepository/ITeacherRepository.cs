using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL { 
    public interface ITeacherRepository : IRepository<Teacher>
    {
        public Boolean LockTeacher(int id);
        public Boolean UnLockTeacher(int id);
    }
}
