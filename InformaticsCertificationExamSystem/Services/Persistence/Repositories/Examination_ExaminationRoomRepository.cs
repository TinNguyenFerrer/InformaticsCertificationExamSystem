using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class Examination_ExaminationRoomRepository : Repository<Examination_ExaminationRoom>, IExamination_ExaminationRoomRepository
    {
        public Examination_ExaminationRoomRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
