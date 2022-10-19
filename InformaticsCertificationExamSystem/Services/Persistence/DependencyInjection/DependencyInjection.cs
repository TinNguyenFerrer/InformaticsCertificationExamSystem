using InformaticsCertificationExamSystem.Service.Core;
using InformaticsCertificationExamSystem.Service.Core.Repositories;
using InformaticsCertificationExamSystem.Service.Persistence.Repositories;
using InformaticsCertificationExamSystem.Service.Persistence;

namespace InformaticsCertificationExamSystem.Service.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExaminationRepository, ExaminationRepository>();
            services.AddScoped<IExamination_ExaminationRoomRepository, Examination_ExaminationRoomRepository>();
            services.AddScoped <IExaminationRoomRepository, ExaminationRoomRepository>();
            services.AddScoped< IFileSubmittedRepository, FileSubmittedRepository > ();
            services.AddScoped< IFinalResultRepository, FinalResultRepository > ();
            services.AddScoped< IInconsistentMarkRepository, InconsistentMarkRepository > ();
            services.AddScoped< IPermissionRepository, PermissionRepository > ();
            services.AddScoped< IStudentRepository, StudentRepository > ();
            services.AddScoped< IStudentTypeRepository, StudentTypeRepository > ();
            services.AddScoped< ITeacher_InconsistentMarkRepository, Teacher_InconsistentMarkRepository > ();
            services.AddScoped< ITeacherRepository, TeacherRepository > ();
            services.AddScoped< ITestSchedule_TheoryTestRepository, TestSchedule_TheoryTestRepository > ();
            services.AddScoped< ITestScheduleRepository, TestScheduleRepository > ();
            services.AddScoped< ITheoryTestRepository, TheoryTestRepository > ();
            return services;
        }
    }
}
