using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    public class InformaticsCertificationExamSystem_DBContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to one
            modelBuilder.Entity<Student>()
                .HasOne<FileSubmitted>(a => a.FileSubmitted)
                .WithOne(b => b.Student)
                .HasForeignKey<FileSubmitted>(c => c.FileOfStudentId);

            modelBuilder.Entity<Student>()
               .HasOne<FinalResult>(a => a.FinalResult)
               .WithOne(b => b.Student)
               .HasForeignKey<FinalResult>(c => c.ResultOfStudentId);

            modelBuilder.Entity<Student>()
                .HasOne<InconsistentMark>(a => a.InconsistentMark)
                .WithOne(b => b.Student)
                .HasForeignKey<InconsistentMark>(c => c.MarkOfStudentId);

            modelBuilder.Entity<Teacher>()
                .HasOne<Permission>(a => a.Permission)
                .WithOne(b => b.Teacher)
                .HasForeignKey<Permission>(c => c.PermissionOfTeacherID);

            //===========
            //modelBuilder.Entity<Examination>(b =>
            //{
            //    b.ToTable("Examination");
            //    b.Property(x => x.Code).ValueGeneratedOnAdd().UseIdentityColumn(1200, 1);
            //});


        }

        public DbSet<Examination> Examinations { get; set; }
        public DbSet<ExaminationRoom> ExaminationRooms { get; set; }
        public DbSet<FileSubmitted> FileSubmitteds { get; set; }
        public DbSet<FinalResult> FinalResults { get; set; }
        public DbSet<InconsistentMark> InconsistentMarks { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TestSchedule> TestSchedules { get; set; }
        public DbSet<TheoryTest> TheoryTests { get; set; }
        public DbSet<StudentType> StudentTypes { get; set; }
        public DbSet<Teacher_InconsistentMark> Teacher_InconsistentMarks { get; set; }
        public DbSet<Examination_ExaminationRoom> Examination_ExaminationRooms { get; set; }
        public DbSet<TestSchedule_TheoryTest> TestSchedule_TheoryTests { get; set; }

        public InformaticsCertificationExamSystem_DBContext()
        {

        }

        public InformaticsCertificationExamSystem_DBContext(DbContextOptions<InformaticsCertificationExamSystem_DBContext> options)
            : base(options)
        {
        }


    }
}
