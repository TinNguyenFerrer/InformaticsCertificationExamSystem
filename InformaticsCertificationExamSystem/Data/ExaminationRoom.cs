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
        [JsonIgnore]
        public virtual ICollection<TestSchedule>? TestSchedule { get; set; }



    }
}
