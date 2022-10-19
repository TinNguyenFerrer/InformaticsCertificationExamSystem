using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    [Table("FileSubmitted")]
    [Index(nameof(Code), IsUnique = true)]
    public class FileSubmittedModel
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("FileSubmittedID")]
        public int Id { get; set; }
        [MaxLength(4)]
        [Required]
        public string Code { get; set; }

        [DefaultValue(false)]
        public Boolean FileWord { get; set; } = false;

        [DefaultValue(false)]
        public Boolean FileExcel { get; set; } = false;

        [DefaultValue(false)]
        public Boolean FilePowerPoint { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime LastSubmissionTime { get; set; }

        //[DefaultValue(false)]
        //public Boolean UploadToCloud { get; set; } = false;
        public int FileOfStudentId { get; set; }
        public int  StudentId { get; set; }
    }
}
