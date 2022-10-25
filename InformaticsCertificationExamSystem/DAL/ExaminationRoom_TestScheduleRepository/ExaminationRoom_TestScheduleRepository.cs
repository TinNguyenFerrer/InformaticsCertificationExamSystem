using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class ExaminationRoom_TestScheduleRepository : Repository<ExaminationRoom_TestSchedule>, IExaminationRoom_TestScheduleRepository
    {
        public ExaminationRoom_TestScheduleRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
