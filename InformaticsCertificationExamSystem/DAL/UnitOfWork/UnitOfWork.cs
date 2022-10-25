using InformaticsCertificationExamSystem.Data;


namespace InformaticsCertificationExamSystem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public InformaticsCertificationExamSystem_DBContext DbContext { get; set; }
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
        public UnitOfWork(InformaticsCertificationExamSystem_DBContext dbContext,
                          IExaminationRepository examinationRepository,
                          IExaminationRoomRepository examinationRoomRepository,
                          IFileSubmittedRepository fileSubmittedRepository,
                          IFinalResultRepository finalResultRepository,
                          IInconsistentMarkRepository inconsistentMarkRepository,
                          IPermissionRepository permissionRepository,
                          IStudentRepository studentRepository,
                          IStudentTypeRepository studentTypeRepository,
                          ISupervisorRepository teacherInconsistentMarkRepository,
                          ITeacherRepository teacherRepository,
                          ITestSchedule_TheoryTestRepository testSchedule_TheoryTestRepository,
                          ITestScheduleRepository testScheduleRepository,
                          ITheoryTestRepository theoryTestRepository,
                          ISupervisorRepository supervisorRepository,
                          IExaminationRoom_TestScheduleRepository examinationRoom_TestScheduleRepository)
        {
            DbContext = dbContext;
            ExaminationRepository = examinationRepository;
            //Examination_ExaminationRoomRepository = examination_ExaminationRoomRepository;
            ExaminationRoomRepository = examinationRoomRepository;
            FileSubmittedRepository = fileSubmittedRepository;
            FinalResultRepository = finalResultRepository;
            InconsistentMarkRepository = inconsistentMarkRepository;
            PermissionRepository = permissionRepository;
            StudentRepository = studentRepository;
            StudentTypeRepository = studentTypeRepository;
            Teacher_InconsistentMarkRepository = teacherInconsistentMarkRepository;
            TeacherRepository = teacherRepository;
            TestSchedule_TheoryTestRepository = testSchedule_TheoryTestRepository;
            TestScheduleRepository = testScheduleRepository;
            TheoryTestRepository = theoryTestRepository;
            SupervisorRepository = supervisorRepository;
            ExaminationRoom_TestScheduleRepository = examinationRoom_TestScheduleRepository;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public int SaveChange()
        {
            return DbContext.SaveChanges();
        }
    }

}
