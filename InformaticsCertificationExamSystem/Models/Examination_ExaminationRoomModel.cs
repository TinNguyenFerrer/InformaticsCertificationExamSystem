using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    public class Examination_ExaminationRoomModel
    {
        [Key]
        public int Id { get; set; }
        public int ExaminationID { get; set; }
        public int ExaminationRoomID { get; set; }
        public ExaminationRoomModel ExaminationRoom { get; set; }
        public ExaminationModel Examination { get; set; }
    }
}
