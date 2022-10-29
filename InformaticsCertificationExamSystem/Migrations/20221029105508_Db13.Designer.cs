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
    [Migration("20221029105508_Db13")]
    partial class Db13
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

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Location");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.ExaminationRoom_TestSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExaminationRoomId")
                        .HasColumnType("int");

                    b.Property<int?>("SupervisorID")
                        .HasColumnType("int");

                    b.Property<int>("TestScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExaminationRoomId");

                    b.HasIndex("SupervisorID")
                        .IsUnique()
                        .HasFilter("[SupervisorID] IS NOT NULL");

                    b.HasIndex("TestScheduleId");

                    b.ToTable("ExaminationRoom_TestSchedule");
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

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2")
                        .HasColumnName("BirthDay");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("BirthPlace");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ExaminationId")
                        .HasColumnType("int");

                    b.Property<int?>("ExaminationRoom_TestScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("IdentifierCode")
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
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("StudentTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("TestScheduleId")
                        .HasColumnType("int");

                    b.Property<int?>("TheoryTestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExaminationId");

                    b.HasIndex("ExaminationRoom_TestScheduleId");

                    b.HasIndex("IdentifierCode")
                        .IsUnique()
                        .HasFilter("[IdentifierCode] IS NOT NULL");

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

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Supervisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SupervisorID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Supervisor");
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

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

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

                    b.Property<int?>("ExaminationId")
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

                    b.ToTable("TestSchedule");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TheoryTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TheoryTestID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("TestScheduleId")
                        .HasColumnType("int");

                    b.Property<bool>("blocked")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("TestScheduleId");

                    b.ToTable("TheoryTests");
                });

            modelBuilder.Entity("SupervisorTeacher", b =>
                {
                    b.Property<int>("SupervisorsId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.HasKey("SupervisorsId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("SupervisorTeacher");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.ExaminationRoom_TestSchedule", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.ExaminationRoom", "ExaminationRoom")
                        .WithMany("ExaminationRoom_TestSchedules")
                        .HasForeignKey("ExaminationRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InformaticsCertificationExamSystem.Data.Supervisor", "Supervisor")
                        .WithOne("ExaminationRoom_TestSchedule")
                        .HasForeignKey("InformaticsCertificationExamSystem.Data.ExaminationRoom_TestSchedule", "SupervisorID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("InformaticsCertificationExamSystem.Data.TestSchedule", "TestSchedule")
                        .WithMany("ExaminationRoom_TestSchedules")
                        .HasForeignKey("TestScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExaminationRoom");

                    b.Navigation("Supervisor");

                    b.Navigation("TestSchedule");
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
                    b.HasOne("InformaticsCertificationExamSystem.Data.Examination", "Examination")
                        .WithMany("Students")
                        .HasForeignKey("ExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InformaticsCertificationExamSystem.Data.ExaminationRoom_TestSchedule", "ExaminationRoom_TestSchedule")
                        .WithMany("Students")
                        .HasForeignKey("ExaminationRoom_TestScheduleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("InformaticsCertificationExamSystem.Data.StudentType", "StudentType")
                        .WithMany("Students")
                        .HasForeignKey("StudentTypeId");

                    b.HasOne("InformaticsCertificationExamSystem.Data.TestSchedule", "TestSchedule")
                        .WithMany("Students")
                        .HasForeignKey("TestScheduleId");

                    b.HasOne("InformaticsCertificationExamSystem.Data.TheoryTest", "TheoryTest")
                        .WithMany("Students")
                        .HasForeignKey("TheoryTestId");

                    b.Navigation("Examination");

                    b.Navigation("ExaminationRoom_TestSchedule");

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
                        .WithMany()
                        .HasForeignKey("ExaminationId");

                    b.Navigation("Examination");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TheoryTest", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.TestSchedule", "TestSchedule")
                        .WithMany()
                        .HasForeignKey("TestScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestSchedule");
                });

            modelBuilder.Entity("SupervisorTeacher", b =>
                {
                    b.HasOne("InformaticsCertificationExamSystem.Data.Supervisor", null)
                        .WithMany()
                        .HasForeignKey("SupervisorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InformaticsCertificationExamSystem.Data.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Examination", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.ExaminationRoom", b =>
                {
                    b.Navigation("ExaminationRoom_TestSchedules");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.ExaminationRoom_TestSchedule", b =>
                {
                    b.Navigation("Students");
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

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Supervisor", b =>
                {
                    b.Navigation("ExaminationRoom_TestSchedule");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.Teacher", b =>
                {
                    b.Navigation("Permission");

                    b.Navigation("Teacher_InconsistentMarks");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TestSchedule", b =>
                {
                    b.Navigation("ExaminationRoom_TestSchedules");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("InformaticsCertificationExamSystem.Data.TheoryTest", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
