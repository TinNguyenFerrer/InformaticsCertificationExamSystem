using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Index(nameof(Name), IsUnique = true)]
    [Table("ExaminationRoom")]
    public class ExaminationRoom
    {
        [Column("ExaminationRoomID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage="Please enter name")]
        [StringLength(250)]
        public string Name { get; set; }

        [Column("Location")]
        [Required(ErrorMessage = "Please enter Location")]
        [StringLength(250)]
        public string Location { get; set; }

        [Column("Capacity")]
        [Required]
        public int Capacity { get; set; }
        [DefaultValue(false)]
        public Boolean Locked { get; set; } = false;
        [JsonIgnore]
        public virtual ICollection<ExaminationRoom_TestSchedule>? ExaminationRoom_TestSchedules{ get; set; }



    }
}
