using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    [Table("StudentType")]
    public class StudentType
    {
        [Column("StudentTypeID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Student>? Students { get; set; }
    }
}
