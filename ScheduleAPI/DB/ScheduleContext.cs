using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ScheduleAPI.Models;

namespace ScheduleAPI.DB;

public partial class ScheduleContext : DbContext
{
    public ScheduleContext()
    {
    }

    public ScheduleContext(DbContextOptions<ScheduleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VU2BGK0;Initial Catalog=Schedule;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.IdClass);

            entity.Property(e => e.IdClass)
                .ValueGeneratedNever()
                .HasColumnName("id_class");
            entity.Property(e => e.DayOfWeek)
                .HasComment("index of day of week")
                .HasColumnName("day_of_week");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.IdSubject).HasColumnName("id_subject");
            entity.Property(e => e.IdTeacher).HasColumnName("id_teacher");
            entity.Property(e => e.Time)
                .HasComment("Time from 0:00 in minutes")
                .HasColumnName("time");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classes_Groups");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classes_Subjects");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classes_Teachers");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.IdGrade);

            entity.Property(e => e.IdGrade)
                .ValueGeneratedNever()
                .HasColumnName("id_grade");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdClass).HasColumnName("id_class");
            entity.Property(e => e.IdStudent).HasColumnName("id_student");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.Grades)
                .HasForeignKey(d => d.IdClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Classes");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.Grades)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Students");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.IdGroup);

            entity.Property(e => e.IdGroup)
                .ValueGeneratedNever()
                .HasColumnName("id_group");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNotification);

            entity.Property(e => e.IdNotification)
                .ValueGeneratedNever()
                .HasColumnName("id_notification");
            entity.Property(e => e.Header)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("header");
            entity.Property(e => e.IdStudent).HasColumnName("id_student");
            entity.Property(e => e.IsChecked).HasColumnName("is_checked");
            entity.Property(e => e.Message)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Moment)
                .HasColumnType("datetime")
                .HasColumnName("moment");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdStudent)
                .HasConstraintName("FK_Notifications_Students");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.IdStudent);

            entity.Property(e => e.IdStudent)
                .ValueGeneratedNever()
                .HasColumnName("id_student");
            entity.Property(e => e.CurrentToken)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("current_token");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fullname");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.TokenGenDate)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("token_gen_date");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Groups");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.IdSubject);

            entity.Property(e => e.IdSubject)
                .ValueGeneratedNever()
                .HasColumnName("id_subject");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.IdTeacher);

            entity.Property(e => e.IdTeacher)
                .ValueGeneratedNever()
                .HasColumnName("id_teacher");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fullname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
