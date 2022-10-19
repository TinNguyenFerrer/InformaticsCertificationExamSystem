using InformaticsCertificationExamSystem.Data;


namespace InformaticsCertificationExamSystem.DAL
{
    public class Examination_ExaminationRoomRepository : Repository<Examination_ExaminationRoom>, IExamination_ExaminationRoomRepository
    {
        public Examination_ExaminationRoomRepository(InformaticsCertificationExamSystem_DBContext dbContext) : base(dbContext) { }
    }
}
