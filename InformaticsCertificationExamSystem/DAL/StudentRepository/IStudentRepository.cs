using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public interface IStudentRepository : IRepository<Student>
    {
        public IEnumerable<Student> GetAllByIdExamination(int Id);
        public Student? GetByEmail(string Email);
        public Student? GetByHashCode(string HashCode);
        //public IEnumerable<Student> GetAllByIdTestSchedule(int Id);
    }
}
