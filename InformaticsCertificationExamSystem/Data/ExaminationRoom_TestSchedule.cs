using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Table("ExaminationRoom_TestSchedule")]
    public class ExaminationRoom_TestSchedule
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("ID")]
        public int Id { get; set; }
        //[ForeignKey("ExaminationRoom")]
        //public int ExaminationRoomId { get; set; }
        public ExaminationRoom ExaminationRoom { get; set; }
        //[ForeignKey("TestSchedule")]
        //public int TestSchedule { get; set; }
        public TestSchedule TestSchedule { get; set; }
        //[ForeignKey("Supervisor")]
        public int? SupervisorID { get; set; }
        public ICollection<Student> Students { get; set; }
        public Supervisor? Supervisor { get; set; }
    }
}
