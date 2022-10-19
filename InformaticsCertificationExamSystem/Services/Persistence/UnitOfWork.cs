using InformaticsCertificationExamSystem.Service.Core;
using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Data;

namespace InformaticsCertificationExamSystem.Service.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public InformaticsCertificationExamSystem_DBContext DbContext { get; set; }
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

        public UnitOfWork(InformaticsCertificationExamSystem_DBContext dbContext,
                          IExaminationRepository examinationRepository,
                          IExamination_ExaminationRoomRepository examination_ExaminationRoomRepository,
                          IExaminationRoomRepository examinationRoomRepository,
                          IFileSubmittedRepository fileSubmittedRepository,
                          IFinalResultRepository finalResultRepository,
                          IInconsistentMarkRepository inconsistentMarkRepository,
                          IPermissionRepository permissionRepository,
                          IStudentRepository studentRepository,
                          IStudentTypeRepository studentTypeRepository,
                          ITeacher_InconsistentMarkRepository teacherInconsistentMarkRepository,
                          ITeacherRepository teacherRepository,
                          ITestSchedule_TheoryTestRepository testSchedule_TheoryTestRepository,
                          ITestScheduleRepository testScheduleRepository,
                          ITheoryTestRepository theoryTestRepository)
        {
            DbContext = dbContext;
            ExaminationRepository = examinationRepository;
            Examination_ExaminationRoomRepository = examination_ExaminationRoomRepository;
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
