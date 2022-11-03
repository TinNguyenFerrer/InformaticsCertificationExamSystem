using AutoMapper;
using InformaticsCertificationExamSystem.Data;
using InformaticsCertificationExamSystem.Models;

namespace InformaticsCertificationExamSystem.Services
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Teacher, TeacherModel>().ReverseMap();
            CreateMap<Examination, ExaminationModel>().ReverseMap();
            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<Student, StudentInfoSubmitFileModel>().ReverseMap();
            CreateMap<ExaminationRoom, ExaminationRoomModel>().ReverseMap();
            CreateMap<TestSchedule, TestScheduleModel>().ReverseMap();
            CreateMap<FileSubmitted, FileSubmittedModel>().ReverseMap();
        }
    }
}
