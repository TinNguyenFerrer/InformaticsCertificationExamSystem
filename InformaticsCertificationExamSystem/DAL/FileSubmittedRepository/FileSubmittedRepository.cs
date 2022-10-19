using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class FileSubmittedRepository : Repository<FileSubmitted>, IFileSubmittedRepository
    {
        public FileSubmittedRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
