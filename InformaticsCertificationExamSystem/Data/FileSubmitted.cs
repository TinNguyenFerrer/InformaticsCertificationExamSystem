using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Table("FileSubmitted")]
    [Index(nameof(Code), IsUnique = true)]
    public class FileSubmitted
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        [Column("FileSubmittedID")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string? Code { get; set; }

        [DefaultValue(false)]
        public Boolean FileWord { get; set; } = false;

        [DefaultValue(false)]
        public Boolean FileExcel { get; set; } = false;

        [DefaultValue(false)]
        public Boolean FilePowerPoint { get; set; } = false;
        [DefaultValue(false)]
        public Boolean FileWindow { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        //[Required]
        public DateTime? LastSubmissionTime { get; set; }

        //[DefaultValue(false)]
        //public Boolean UploadToCloud { get; set; } = false;
        //[ForeignKey("Student")]
        //[JsonIgnore]
        //public int StudentId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }
    }
}
