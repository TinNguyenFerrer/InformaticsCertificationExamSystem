using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Table("Permission")]
    public class Permission
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("PermissionID")]
        public int Id { get; set; }

        [DefaultValue(false)]
        public Boolean Supervision { get; set; } = false;

        [DefaultValue(false)]
        public Boolean Marker { get; set; } = false;

        [DefaultValue(false)]
        public Boolean Admin { get; set; } = false ;

        public int PermissionOfTeacherID { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
