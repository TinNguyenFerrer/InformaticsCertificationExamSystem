using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class ExaminationRepository: Repository<Examination>, IExaminationRepository
    {
        public ExaminationRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
