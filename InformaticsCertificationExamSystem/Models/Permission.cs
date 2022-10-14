using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    [Table("Permission")]
    public class Permission
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("PermissionID")]
        public int Id { get; set; }

        [DefaultValue(true)]
        public Boolean Supervision { get; set; } = true;

        [DefaultValue(false)]
        public Boolean Marker { get; set; } = false;

        [DefaultValue(false)]
        public Boolean Admin { get; set; } = false ;
    }
}
