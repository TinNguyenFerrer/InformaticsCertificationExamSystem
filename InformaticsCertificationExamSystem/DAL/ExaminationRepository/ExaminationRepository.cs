using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class ExaminationRepository: Repository<Examination>, IExaminationRepository
    {
        public ExaminationRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
