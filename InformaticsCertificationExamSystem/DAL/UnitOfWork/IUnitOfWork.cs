using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        public InformaticsCertificationExamSystem_DBContext DbContext { get; }
        public IExaminationRepository ExaminationRepository { get; set; }
        //public IExamination_ExaminationRoomRepository Examination_ExaminationRoomRepository { get; set; }
        public IExaminationRoomRepository ExaminationRoomRepository { get; set; }
        public IFileSubmittedRepository FileSubmittedRepository { get; set; }
        public IFinalResultRepository FinalResultRepository { get; set; }
        public IInconsistentMarkRepository InconsistentMarkRepository { get; set; }
        public IPermissionRepository PermissionRepository { get; set; }
        public IStudentRepository StudentRepository { get; set; }
        public IStudentTypeRepository StudentTypeRepository { get; set; }
        public ISupervisorRepository Teacher_InconsistentMarkRepository { get; set; }
        public ITeacherRepository TeacherRepository { get; set; }
        public ITestSchedule_TheoryTestRepository TestSchedule_TheoryTestRepository { get; set; }
        public ITestScheduleRepository TestScheduleRepository { get; set; }
        public ITheoryTestRepository TheoryTestRepository { get; set; }
        public ISupervisorRepository SupervisorRepository { get; set; }
        public IExaminationRoom_TestScheduleRepository ExaminationRoom_TestScheduleRepository { get; set; }
        public int SaveChange();
    }
}
