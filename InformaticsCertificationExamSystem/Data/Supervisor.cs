using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Table("Supervisor")]
    public class Supervisor
    {
        [Column("SupervisorID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
        //[ForeignKey("TestSchedule")]
        //public int IdTestSchedule { get; set; }
        public ExaminationRoom_TestSchedule? ExaminationRoom_TestSchedule { get; set; }
        //[ForeignKey("ExaminationRoom_TestSchedule")]
        //public int ExaminationRoom_TestSchedule_ID { get; set; }
    }
}
