using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
        public Boolean LockTeacher(int id)
        {
            Teacher Teacher = this.GetByID(id);
            if (Teacher != null)
            {
                Teacher.Locked = true;
                this.Update(Teacher);
                return true;
            }
            return false;
        }

        public Boolean UnLockTeacher(int id)
        {
            Teacher Teacher = this.GetByID(id);
            if (Teacher != null)
            {
                Teacher.Locked = false;
                this.Update(Teacher);
                return true;
            }
            return false;
        }
    }
}
