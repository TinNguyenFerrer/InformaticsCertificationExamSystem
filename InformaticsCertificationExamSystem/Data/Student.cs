using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Table("Student")]
    [Index(nameof(IdentifierCode), IsUnique = true)]
    public class Student
    {
        [Column("StudentID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("BirthPlace")]
        public string BirthPlace { get; set; }

        [Required]
        [Column("BirthDay")]
        public DateTime BirthDay { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(20)]
        public string? IdentifierCode { get; set; }

        [MaxLength(255)]
        public string? Password { get; set; }

        [DefaultValue(0)]
        public int NumberOfCheats { get; set; }
        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        [JsonIgnore]
        public Examination Examination { get; set; }
        public FinalResult? FinalResult { get; set; }
        public InconsistentMark? InconsistentMark { get; set; }
        public TheoryTest? TheoryTest { get; set; }
        public StudentType? StudentType { get; set; }
        public FileSubmitted? FileSubmitted { get; set; }

        public int? ExaminationRoom_TestScheduleId { get; set; }
        [JsonIgnore]
        public ExaminationRoom_TestSchedule? ExaminationRoom_TestSchedule { get; set; }
    }
}
