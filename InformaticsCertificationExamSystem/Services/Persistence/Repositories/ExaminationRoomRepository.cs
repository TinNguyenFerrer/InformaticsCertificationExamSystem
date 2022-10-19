using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;


namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class ExaminationRoomRepository : Repository<ExaminationRoom>, IExaminationRoomRepository
    {
        public ExaminationRoomRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
