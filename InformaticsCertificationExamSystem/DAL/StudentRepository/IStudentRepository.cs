using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public interface IStudentRepository : IRepository<Student>
    {
        public IEnumerable<Student> GetAllByIdExamination(int Id);
    }
}
