using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformaticsCertificationExamSystem.Models
{
    [Table("InconsistentMark")]
    public class InconsistentMark
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("InconsistentMarkID")]
        public int Id { get; set; }
        public float Word { get; set; }
        public float Excel { get; set; }
        public float PowerPoint { get; set; }

        public int MarkOfStudentId { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Teacher_InconsistentMark> Teacher_InconsistentMarks { get; set; }

    }
}
