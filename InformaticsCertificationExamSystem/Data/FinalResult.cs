using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformaticsCertificationExamSystem.Data
{
    public class FinalResult
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("InconsistentMarkID")]
        public int Id { get; set; }
        public double Word { get; set; } = 0;
        public double Excel { get; set; } = 0;
        public double PowerPoint { get; set; } = 0;
        public double Window { get; set; } = 0;
        public double Theory { get; set; } = 0;
        public double Practice { get; set; } = 0;
        public double FinalMark { get; set; } = 0;
        //[ForeignKey("Student")]
        //public int? StudentId { get; set; }
        public virtual Student? Student { get; set; }
    }
}
