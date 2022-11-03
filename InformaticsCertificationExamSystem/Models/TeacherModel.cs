using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    [Table("Teacher")]
    [Index(nameof(IdentifierCode), IsUnique = true)]
    public class TeacherModel
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

        public string Password { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set;}

        [Required]
        [MaxLength(255)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        //public virtual PermissionModel? Permission { get; set; }


    }
}
