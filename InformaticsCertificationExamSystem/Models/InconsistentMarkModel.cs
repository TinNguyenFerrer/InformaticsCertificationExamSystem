using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformaticsCertificationExamSystem.Models
{
    [Table("InconsistentMark")]
    public class InconsistentMarkModel
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("InconsistentMarkID")]
        public int Id { get; set; }
        public float Word { get; set; }
        public float Excel { get; set; }
        public float PowerPoint { get; set; }

        public int MarkOfStudentId { get; set; }
        public int StudentId { get; set; }

    }
}
