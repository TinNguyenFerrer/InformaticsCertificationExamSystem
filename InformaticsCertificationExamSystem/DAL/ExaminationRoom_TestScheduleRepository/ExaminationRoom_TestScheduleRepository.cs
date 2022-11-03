using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class ExaminationRoom_TestScheduleRepository : Repository<ExaminationRoom_TestSchedule>, IExaminationRoom_TestScheduleRepository
    {
        public ExaminationRoom_TestScheduleRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }

        public List<int> GetIdRoomByIdTestScheduleRepository(int idTestSchedule)
        {
            var result = from examinationroom_testschedule in this.GetAll()
                         where examinationroom_testschedule.TestScheduleId == idTestSchedule
                         select examinationroom_testschedule.ExaminationRoomId;
            return result.ToList();
        }
    }
}
