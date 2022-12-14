using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformaticsCertificationExamSystem.Models
{
    public class FinalResultModel
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("InconsistentMarkID")]
        public int Id { get; set; }
        public float Word { get; set; }
        public float Excel { get; set; }
        public float PowerPoint { get; set; }
        public float Theory { get; set; }
        public float Practice { get; set; }
        public float FinalMark { get; set; }
        public int ResultOfStudentId { get; set; }
        public int StudentId { get; set; }
    }
}
