using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Table("ExaminationRoom")]
    public class ExaminationRoomModel
    {
        [Column("ExaminationRoomID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage="Please enter name")]
        [StringLength(250)]
        public string Name { get; set; }

        [Column("Capacity")]
        [Required]
        public int Capacity { get; set; }


    }
}
