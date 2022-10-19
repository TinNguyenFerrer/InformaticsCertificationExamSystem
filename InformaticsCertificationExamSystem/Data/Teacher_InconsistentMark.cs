using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    public class Teacher_InconsistentMark
    {
        [Key]
        public int Id { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public int InconsistentMarkID { get; set; }
        public InconsistentMark InconsistentMark { get; set; }
    }
}

