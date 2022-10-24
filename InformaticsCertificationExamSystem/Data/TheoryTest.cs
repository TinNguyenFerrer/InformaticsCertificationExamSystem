using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    public class TheoryTest
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("TheoryTestID")]
        public int Id { get; set; }
        [MaxLength(4)]
        [Required]
        public string ExamCode { get; set; }
        [MaxLength(255)]
        public string Path { get; set; }

        public virtual ICollection<Student>? Students { get; set; }
        public virtual Examination Examination { get; set; }
        //public virtual ICollection<TestSchedule_TheoryTest>? TestSchedule_TheoryTests { get; set; }
    }
}
