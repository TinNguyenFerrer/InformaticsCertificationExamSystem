using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Service.Persistence.Repositories;
namespace InformaticsCertificationExamSystem.Service.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public InformaticsCertificationExamSystem_DBContext DbContext { get; }
        public IExaminationRepository ExaminationRepository { get; set; }
        public IExamination_ExaminationRoomRepository Examination_ExaminationRoomRepository { get; set; }
        public IExaminationRoomRepository ExaminationRoomRepository { get; set; }
        public IFileSubmittedRepository FileSubmittedRepository { get; set; }
        public IFinalResultRepository FinalResultRepository { get; set; }
        public IInconsistentMarkRepository InconsistentMarkRepository { get; set; }
        public IPermissionRepository PermissionRepository { get; set; }
        public IStudentRepository StudentRepository { get; set; }
        public IStudentTypeRepository StudentTypeRepository { get; set; }
        public ITeacher_InconsistentMarkRepository Teacher_InconsistentMarkRepository { get; set; }
        public ITeacherRepository TeacherRepository { get; set; }
        public ITestSchedule_TheoryTestRepository TestSchedule_TheoryTestRepository { get; set; }
        public ITestScheduleRepository TestScheduleRepository { get; set; }
        public ITheoryTestRepository TheoryTestRepository { get; set; }
        public int SaveChange();
    }
}
