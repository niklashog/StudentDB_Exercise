using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentDB_Exercise.Data;

public partial class StudentContext : DbContext
{
    public StudentContext()
    {
    }

    public StudentContext(DbContextOptions<StudentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kur> Kurs { get; set; }

    public virtual DbSet<Student> Students { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kur>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Namn)
                .HasMaxLength(50)
                .HasColumnName("namn");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("datetime")
                .HasColumnName("birthdate");
            entity.Property(e => e.Efternamn)
                .HasMaxLength(50)
                .HasColumnName("efternamn");
            entity.Property(e => e.Fornamn)
                .HasMaxLength(50)
                .HasColumnName("fornamn");
            entity.Property(e => e.KursId).HasColumnName("kurs_id");

            entity.HasOne(d => d.Kurs).WithMany(p => p.Students)
                .HasForeignKey(d => d.KursId)
                .HasConstraintName("FK_Student_Kurs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
