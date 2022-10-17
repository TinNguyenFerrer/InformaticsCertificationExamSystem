using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    public class Examination_ExaminationRoom
    {
        [Key]
        public int Id { get; set; }
        public int ExaminationID { get; set; }
        public Examination Examination { get; set; }
        public int ExaminationRoomID { get; set; }
        public ExaminationRoom ExaminationRoom { get; set; }
    }
}
