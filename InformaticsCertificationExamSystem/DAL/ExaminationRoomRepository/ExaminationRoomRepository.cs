using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class ExaminationRoomRepository : Repository<ExaminationRoom>, IExaminationRoomRepository
    {
        public ExaminationRoomRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }

        public Boolean LockExaminationRoom(int id)
        {
            ExaminationRoom ExRoom = this.GetByID(id);
            if(ExRoom != null)
            {
                ExRoom.Locked = true;
                this.Update(ExRoom);
                return true;
            }
            return false;
        }

        public Boolean UnLockExaminationRoom(int id)
        {
            ExaminationRoom ExRoom = this.GetByID(id);
            if (ExRoom != null)
            {
                ExRoom.Locked = false;
                this.Update(ExRoom);
                return true;
            }
            return false;
        }
    }
}
