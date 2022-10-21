using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    [Table("Examination")]
    public class ExaminationModel
    {
        [Column("ExaminationID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("Name")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        //[Column("ExaminationCode")]
        //[MaxLength(20)]
        //[Required]
        //public string? Code { get; set; }

        [Column("StarTime")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StarTime { get; set; }

        [Column("EndTime")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime EndTime { get; set; }

        [Column("Location")]
        [MaxLength(500)]
        [Required]
        public string Location { get; set; }

        [Column("MinimumTheoreticalScore")]
        [Required]
        public float MinimumTheoreticalMark { get; set; }

        [Column("MinimumPracticeScore")]
        public float MinimumPracticeMark { get; set; }

        //[Column("ReviewTime")]
        //[DefaultValue(false)]
        //[Required]
        //public Boolean IsBlocked { get; set; }

        [Column("GradingDeadline")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime GradingDeadline { get; set; }

        
    }
}
