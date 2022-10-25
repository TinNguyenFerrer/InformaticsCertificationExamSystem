using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public class SupervisorRepository : Repository<Supervisor>, ISupervisorRepository
    {
        public SupervisorRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
