using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public interface IExaminationRoomRepository:IRepository<ExaminationRoom>
    {
        public Boolean LockExaminationRoom(int id);
        public Boolean UnLockExaminationRoom(int id);

    }
}
