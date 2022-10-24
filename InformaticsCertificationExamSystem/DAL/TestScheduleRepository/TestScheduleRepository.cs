using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class TestScheduleRepository : Repository<TestSchedule>, ITestScheduleRepository
    {
        public TestScheduleRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }

        IEnumerable<TestSchedule> ITestScheduleRepository.GetAllByIdExamination(int id)
        {
            var TestSchedules = from testschedules in this.DbContext.TestSchedules
                                //join examination in this.DbContext.Examinations 

                                //where testschedules.Examination.Id == id
                                select testschedules;
                return TestSchedules.ToList();
        }
    }
}
