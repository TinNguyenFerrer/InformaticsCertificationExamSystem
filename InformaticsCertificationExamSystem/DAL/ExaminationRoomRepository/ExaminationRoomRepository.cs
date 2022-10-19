using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class ExaminationRoomRepository : Repository<ExaminationRoom>, IExaminationRoomRepository
    {
        public ExaminationRoomRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
