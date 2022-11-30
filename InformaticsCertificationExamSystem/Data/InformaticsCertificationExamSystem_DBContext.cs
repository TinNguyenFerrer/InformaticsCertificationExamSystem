using InformaticsCertificationExamSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace InformaticsCertificationExamSystem.Data
{
    public class InformaticsCertificationExamSystem_DBContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to one
            //modelBuilder.Entity<Student>()
            //    .HasOne<FileSubmitted>(a => a.FileSubmitted)
            //    .WithOne(b => b.Student)
            //    .HasForeignKey<FileSubmitted>(c => c.StudentId);

            //modelBuilder.Entity<Student>()
            //   .HasOne<FinalResult>(a => a.FinalResult)
            //   .WithOne(b => b.Student)
            //   .HasForeignKey<FinalResult>(c => c.ResultOfStudentId);

            modelBuilder.Entity<Student>()
                .HasOne<InconsistentMark>(a => a.InconsistentMark)
                .WithOne(b => b.Student)
                .HasForeignKey<InconsistentMark>(c => c.MarkOfStudentId);

            //modelBuilder.Entity<Teacher>()
            //    .HasOne<Permission>(a => a.Permission)
            //    .WithOne(b => b.Teacher)
            //    .HasForeignKey<Permission>(c => c.PermissionOfTeacherID);

            modelBuilder.Entity<ExaminationRoom_TestSchedule>()
                .HasOne<Supervisor>(a => a.Supervisor)
                .WithOne(b => b.ExaminationRoom_TestSchedule)
                .HasForeignKey<ExaminationRoom_TestSchedule>(c => c.SupervisorID)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ExaminationRoom_TestSchedule>()
                .HasMany<Student>(a => a.Students)
                .WithOne(b => b.ExaminationRoom_TestSchedule)
                .HasForeignKey(c => c.ExaminationRoom_TestScheduleId)
                .OnDelete(DeleteBehavior.SetNull);
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
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Teacher_InconsistentMark> Teacher_InconsistentMarks { get; set; }
        public DbSet<ExaminationRoom_TestSchedule> ExaminationRoom_TestSchedule { get; set; }
        //public DbSet<Examination_ExaminationRoom> Examination_ExaminationRooms { get; set; }
        //public DbSet<TestSchedule_TheoryTest> TestSchedule_TheoryTests { get; set; }

        public override int SaveChanges()
        {
            var maxId = this.Students.Max(p => (int?)p.Id) ?? 0;
            var identifierCode = maxId + 1;
            var hashcode = maxId + 1; ;
            var rdom = new Random();
            int hashCodeFinal = rdom.Next(10000, 19999);
            string hashCodeFirst;
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is Student entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                if (entityReference.IdentifierCode == null || entityReference.IdentifierCode == "")
                                {
                                    entityReference.IdentifierCode = "TD_" + (identifierCode + 2000).ToString();
                                    identifierCode++;
                                }
                                if (entityReference.HashCode == null || entityReference.HashCode == "")
                                {
                                    hashCodeFirst = SummaryService.IntToBase32(hashcode * 3 + 11);
                                    if (hashCodeFirst.Length <= 8)
                                    {
                                        entityReference.HashCode = hashCodeFirst + (SummaryService.IntToBase32(hashCodeFinal)).Substring(0, 8 - hashCodeFirst.Length);
                                    }
                                    else
                                    {
                                        entityReference.HashCode = hashCodeFirst;
                                    }
                                    hashCodeFinal = hashCodeFinal + 3;
                                    hashcode++;
                                }
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }


        public InformaticsCertificationExamSystem_DBContext()
        {

        }

        public InformaticsCertificationExamSystem_DBContext(DbContextOptions<InformaticsCertificationExamSystem_DBContext> options)
            : base(options)
        {
        }


    }
}
