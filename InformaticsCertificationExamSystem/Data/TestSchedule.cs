using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Table("TestSchedule")]
    //[Index(nameof(Name), IsUnique = true)]
    public class TestSchedule
    {
        [Column("TestScheduleID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("Name")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Column("StarTime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StarTime { get; set; }

        [Column("EndTime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime EndTime { get; set; }
        [JsonIgnore]
        public virtual ICollection<Student>? Students { get; set; }
        [ForeignKey("Examination")]
        public int? ExaminationId { get; set; }
        //----------------------------------------------------------
        [DefaultValue(false)]
        public Boolean IsAssignedSupervisor { get; set; } = false;
        [JsonIgnore]
        //[ForeignKey("ExaminationId")]
        public virtual Examination? Examination { get; set; }
        //public Supervisor? Supervisor { get; set; }
        public ICollection<ExaminationRoom_TestSchedule>? ExaminationRoom_TestSchedules { get; set; }
        public ICollection<TheoryTest>? TheoryTest { get; set; }

    }
}
