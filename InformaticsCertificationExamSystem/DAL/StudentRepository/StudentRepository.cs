using InformaticsCertificationExamSystem.Data;


namespace InformaticsCertificationExamSystem.DAL
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }

        public IEnumerable<Student> GetAllByIdExamination(int Id)
        {
            var Students = from students in this.GetAll()
                           where students.ExaminationId == Id
                           select students;
            return Students.ToList();
        }
    }
}
