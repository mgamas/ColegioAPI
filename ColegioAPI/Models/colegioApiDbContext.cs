using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class colegioApiDbContext : DbContext
{
    public colegioApiDbContext()
    {
    }

    public colegioApiDbContext(DbContextOptions<colegioApiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Grado> Grados { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PK__Alumno__6D77A7F1C7AF7C31");

            entity.ToTable("Alumno");

            entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.idgrado).HasName("PK__Grado__6DB797EE75AC74E1");

            entity.ToTable("Grado");

            entity.Property(e => e.idgrado).HasColumnName("id_grado");
            entity.Property(e => e.idprofesor).HasColumnName("id_profesor");
            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_grado");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Grados)
                .HasForeignKey(d => d.idprofesor)
                .HasConstraintName("fk_profesor");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.idmatricula).HasName("PK__Matricul__1D7CF00BA44D0D56");

            entity.ToTable("Matricula");

            entity.Property(e => e.idmatricula).HasColumnName("id_matricula");
            entity.Property(e => e.fechamatricula).HasColumnName("fecha_matricula");
            entity.Property(e => e.idalumno).HasColumnName("id_alumno");
            entity.Property(e => e.idgrado).HasColumnName("id_grado");
            entity.Property(e => e.seccion)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("seccion");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.idalumno)
                .HasConstraintName("fk_alumno");

            entity.HasOne(d => d.IdGradoNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.idgrado)
                .HasConstraintName("fk_grado");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PK__Profesor__159ED61778F77FF5");

            entity.ToTable("Profesor");

            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Genero)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
