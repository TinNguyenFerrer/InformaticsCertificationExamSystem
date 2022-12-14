using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL { 
    public interface ITestScheduleRepository : IRepository<TestSchedule>
    {
        IEnumerable<TestSchedule> GetAllByIdExamination(int id);
    }
}
