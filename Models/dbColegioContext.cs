using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Models
{
    public partial class dbColegioContext : DbContext
    {
        public dbColegioContext()
        {
        }

        public dbColegioContext(DbContextOptions<dbColegioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("Alumno");

                entity.Property(e => e.AlumnoId).HasColumnName("AlumnoID");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
