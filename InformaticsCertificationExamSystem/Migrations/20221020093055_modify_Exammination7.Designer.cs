﻿// <auto-generated />
using System;
using InformaticsCertificationExamSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    [DbContext(typeof(InformaticsCertificationExamSystem_DBContext))]
    [Migration("20221020093055_modify_Exammination7")]
    partial class modify_Exammination7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Examination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ExaminationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndTime");

                    b.Property<DateTime>("GradingDeadline")
                        .HasColumnType("datetime2")
                        .HasColumnName("GradingDeadline");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Location");

                    b.Property<float>("MinimumPracticeMark")
                        .HasColumnType("real")
                        .HasColumnName("MinimumPracticeScore");

                    b.Property<float>("MinimumTheoreticalMark")
                        .HasColumnType("real")
                        .HasColumnName("MinimumTheoreticalScore");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<DateTime>("StarTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("StarTime");

                    b.HasKey("Id");

                    b.ToTable("Examination");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Examination_ExaminationRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExaminationID")
                        .HasColumnType("int");

                    b.Property<int>("ExaminationRoomID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExaminationID");

                    b.HasIndex("ExaminationRoomID");

                    b.ToTable("Examination_ExaminationRooms");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.ExaminationRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ExaminationRoomID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int")
                        .HasColumnName("Capacity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ExaminationRoom");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.FileSubmitted", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FileSubmittedID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<bool>("FileExcel")
                        .HasColumnType("bit");

                    b.Property<int>("FileOfStudentId")
                        .HasColumnType("int");

                    b.Property<bool>("FilePowerPoint")
                        .HasColumnType("bit");

                    b.Property<bool>("FileWord")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastSubmissionTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("FileOfStudentId")
                        .IsUnique();

                    b.ToTable("FileSubmitted");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.FinalResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("InconsistentMarkID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Excel")
                        .HasColumnType("real");

                    b.Property<float>("FinalMark")
                        .HasColumnType("real");

                    b.Property<float>("PowerPoint")
                        .HasColumnType("real");

                    b.Property<float>("Practice")
                        .HasColumnType("real");

                    b.Property<int>("ResultOfStudentId")
                        .HasColumnType("int");

                    b.Property<float>("Word")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ResultOfStudentId")
                        .IsUnique();

                    b.ToTable("FinalResults");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.InconsistentMark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("InconsistentMarkID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Excel")
                        .HasColumnType("real");

                    b.Property<int>("MarkOfStudentId")
                        .HasColumnType("int");

                    b.Property<float>("PowerPoint")
                        .HasColumnType("real");

                    b.Property<float>("Word")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MarkOfStudentId")
                        .IsUnique();

                    b.ToTable("InconsistentMark");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PermissionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<bool>("Marker")
                        .HasColumnType("bit");

                    b.Property<int>("PermissionOfTeacherID")
                        .HasColumnType("int");

                    b.Property<bool>("Supervision")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PermissionOfTeacherID")
                        .IsUnique();

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StudentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("IdentifierCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<int>("NumberOfCheats")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("StudentTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("TestScheduleId")
                        .HasColumnType("int");

                    b.Property<int?>("TheoryTestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdentifierCode")
                        .IsUnique();

                    b.HasIndex("StudentTypeId");

                    b.HasIndex("TestScheduleId");

                    b.HasIndex("TheoryTestId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.StudentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StudentTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("StudentType");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TeacherID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("FullName");

                    b.Property<string>("IdentifierCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("IdentifierCode")
                        .IsUnique();

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Teacher_InconsistentMark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InconsistentMarkID")
                        .HasColumnType("int");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InconsistentMarkID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Teacher_InconsistentMarks");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TestSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TestScheduleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndTime");

                    b.Property<int>("ExaminationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.Property<DateTime>("StarTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("StarTime");

                    b.HasKey("Id");

                    b.HasIndex("ExaminationId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TestSchedule");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TestSchedule_TheoryTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("TestScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("TheoryTestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TestScheduleId");

                    b.HasIndex("TheoryTestId");

                    b.ToTable("TestSchedule_TheoryTests");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TheoryTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TheoryTestID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ExamCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("TheoryTests");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Examination_ExaminationRoom", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.Examination", "Examination")
                        .WithMany("Examination_ExaminationRooms")
                        .HasForeignKey("ExaminationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InformaticsCertificationExamSystem.Data.ExaminationRoom", "ExaminationRoom")
                        .WithMany("Examination_ExaminationRooms")
                        .HasForeignKey("ExaminationRoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examination");

                    b.Navigation("ExaminationRoom");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.FileSubmitted", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.Student", "Student")
                        .WithOne("FileSubmitted")
                        .HasForeignKey("InformaticsCertificationExamSystem.Data.FileSubmitted", "FileOfStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.FinalResult", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.Student", "Student")
                        .WithOne("FinalResult")
                        .HasForeignKey("InformaticsCertificationExamSystem.Data.FinalResult", "ResultOfStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.InconsistentMark", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.Student", "Student")
                        .WithOne("InconsistentMark")
                        .HasForeignKey("InformaticsCertificationExamSystem.Data.InconsistentMark", "MarkOfStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Permission", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.Teacher", "Teacher")
                        .WithOne("Permission")
                        .HasForeignKey("InformaticsCertificationExamSystem.Data.Permission", "PermissionOfTeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Student", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.StudentType", "StudentType")
                        .WithMany("Students")
                        .HasForeignKey("StudentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InformaticsCertificationExamSystem.Data.TestSchedule", "TestSchedule")
                        .WithMany("Students")
                        .HasForeignKey("TestScheduleId");

                    b.HasOne("InformaticsCertificationExamSystem.Data.TheoryTest", "TheoryTest")
                        .WithMany("Students")
                        .HasForeignKey("TheoryTestId");

                    b.Navigation("StudentType");

                    b.Navigation("TestSchedule");

                    b.Navigation("TheoryTest");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Teacher_InconsistentMark", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.InconsistentMark", "InconsistentMark")
                        .WithMany("Teacher_InconsistentMarks")
                        .HasForeignKey("InconsistentMarkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InformaticsCertificationExamSystem.Data.Teacher", "Teacher")
                        .WithMany("Teacher_InconsistentMarks")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InconsistentMark");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TestSchedule", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.Examination", "Examination")
                        .WithMany("TestSchedules")
                        .HasForeignKey("ExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examination");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TestSchedule_TheoryTest", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.TestSchedule", "TestSchedule")
                        .WithMany("TestSchedule_TheoryTests")
                        .HasForeignKey("TestScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InformaticsCertificationExamSystem.Data.TheoryTest", "TheoryTest")
                        .WithMany("TestSchedule_TheoryTests")
                        .HasForeignKey("TheoryTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestSchedule");

                    b.Navigation("TheoryTest");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Examination", b =>
                {
                    b.Navigation("Examination_ExaminationRooms");

                    b.Navigation("TestSchedules");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.ExaminationRoom", b =>
                {
                    b.Navigation("Examination_ExaminationRooms");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.InconsistentMark", b =>
                {
                    b.Navigation("Teacher_InconsistentMarks");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Student", b =>
                {
                    b.Navigation("FileSubmitted");

                    b.Navigation("FinalResult");

                    b.Navigation("InconsistentMark");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.StudentType", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Teacher", b =>
                {
                    b.Navigation("Permission");

                    b.Navigation("Teacher_InconsistentMarks");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TestSchedule", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("TestSchedule_TheoryTests");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TheoryTest", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("TestSchedule_TheoryTests");
                });
#pragma warning restore 612, 618
        }
    }
}
