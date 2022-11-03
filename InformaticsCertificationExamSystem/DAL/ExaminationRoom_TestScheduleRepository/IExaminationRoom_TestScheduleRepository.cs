using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public interface IExaminationRoom_TestScheduleRepository : IRepository<ExaminationRoom_TestSchedule>
    {
        public List<int> GetIdRoomByIdTestScheduleRepository(int idTestSchedule);
    }
}
