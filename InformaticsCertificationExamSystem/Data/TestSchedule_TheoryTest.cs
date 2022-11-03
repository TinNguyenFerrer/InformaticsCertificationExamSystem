using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    public class TestSchedule_TheoryTest
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public int TestScheduleId { get; set; }
        public TestSchedule TestSchedule { get; set; }
        //public int TheoryTestId { get; set; }
        //public TheoryTest TheoryTest { get; set; }
    }
}
