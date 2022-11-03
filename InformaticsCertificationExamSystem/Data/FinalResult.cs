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
        public float Word { get; set; } = 0;
        public float Excel { get; set; } = 0;
        public float PowerPoint { get; set; } = 0;
        public float Practice { get; set; } = 0;
        public float FinalMark { get; set; } = 0;
        //public int ResultOfStudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
