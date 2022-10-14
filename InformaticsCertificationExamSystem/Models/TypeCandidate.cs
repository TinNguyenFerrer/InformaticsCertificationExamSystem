using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    [Table("TypeCandidate")]
    public class TypeCandidate
    {
        [Column("TypeCandidateID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

    }
}
