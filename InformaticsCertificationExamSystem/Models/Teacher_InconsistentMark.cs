using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Models
{
    public class Teacher_InconsistentMark
    {
        [Key]
        public int Id { get; set; }
        public int TeacherID { get; set; }
        public TeacherModel Teacher { get; set; }
        public int InconsistentMarkID { get; set; }
        public InconsistentMarkModel InconsistentMark { get; set; }
    }
}

