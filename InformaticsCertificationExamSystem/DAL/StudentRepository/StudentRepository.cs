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
        public Student? GetByEmail(string Email)
        {
            return Dbset.FirstOrDefault(e=>e.Email==Email);
        }
        //public IEnumerable<Student> GetAllByIdTestSchedule(int Id)
        //{
        //    var Students = from students in this.DbContext.Students
        //                   where students.TestSchedule.Id == Id
        //                   select students;
        //    return Students.ToList();
        //}
    }
}
