using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Table("Teacher")]
    [Index(nameof(IdentifierCode), IsUnique = true)]
    public class Teacher
    {
        [Column("TeacherID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("FullName")]
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        

        [MaxLength(20)]
        [Required]
        public string IdentifierCode { get; set; }

        [MaxLength(20)]
        [Required]
        public string Password { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set;}

        [Required]
        [MaxLength(255)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }
        [DefaultValue(false)]
        public Boolean Locked { get; set; } = false;
        [ForeignKey("Permission")]
        [DefaultValue(1)]
        public int? PermissionId { get; set; } = 1;
        public virtual Permission? Permission { get; set; }
        public ICollection<Supervisor>? Supervisors { get; set; }
       
        public virtual ICollection<Teacher_InconsistentMark>? Teacher_InconsistentMarks { get; set; }

    }
}
