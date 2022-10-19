﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    [Table("TestSchedule")]
    [Index(nameof(Name), IsUnique = true)]
    public class TestSchedule
    {
        [Column("TestScheduleID")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("Name")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Column("StarTime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StarTime { get; set; }

        [Column("EndTime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime EndTime { get; set; }
        
        public virtual ICollection<Student>? Students { get; set; }
        public virtual Examination Examination { get; set; }
        public virtual ICollection<TestSchedule_TheoryTest>? TestSchedule_TheoryTests { get; set; }

    }
}