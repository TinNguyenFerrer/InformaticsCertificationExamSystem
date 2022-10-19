using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence.Repositories
{
    public class FileSubmittedRepository: Repository<FileSubmitted>, IFileSubmittedRepository
    {
        public FileSubmittedRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
}
}
