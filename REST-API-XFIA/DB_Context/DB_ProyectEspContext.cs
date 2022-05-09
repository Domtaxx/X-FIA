using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.DB_Context
{
    public partial class DB_ProyectEspContext : DbContext
    {
        public DB_ProyectEspContext()
        {
        }

        public DB_ProyectEspContext(DbContextOptions<DB_ProyectEspContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Campeonato> Campeonatos { get; set; } = null!;
        public virtual DbSet<Carrera> Carreras { get; set; } = null!;
        public virtual DbSet<Paise> Paises { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:db-project-espe-server.database.windows.net,1433;Initial Catalog=DB_ProyectEsp;Persist Security Info=False;User ID=Project_Espe;Password=Server2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campeonato>(entity =>
            {
                entity.HasKey(e => e.NombreCm)
                    .HasName("PK__CAMPEONA__E69225DB057BC79E");

                entity.ToTable("CAMPEONATO");

                entity.HasIndex(e => e.Llave, "UQ__CAMPEONA__8E70B292050B8562")
                    .IsUnique();

                entity.HasIndex(e => e.NombreCm, "UQ__CAMPEONA__E69225DA127E679A")
                    .IsUnique();

                entity.Property(e => e.NombreCm)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionDeReglas)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Descripcion_de_reglas");

                entity.Property(e => e.FechaDeFin)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_fin");

                entity.Property(e => e.FechaDeInicio)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_inicio");

                entity.Property(e => e.HoraDeFin).HasColumnName("Hora_de_fin");

                entity.Property(e => e.HoraDeInicio).HasColumnName("Hora_de_inicio");

                entity.Property(e => e.Llave)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => e.NombreCr)
                    .HasName("PK__CARRERA__E69225D0083FCDC8");

                entity.ToTable("CARRERA");

                entity.HasIndex(e => e.NombreCr, "UQ__CARRERA__E69225D1622E0CAF")
                    .IsUnique();

                entity.Property(e => e.NombreCr)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Campeonato)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaDeFin)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_fin");

                entity.Property(e => e.FechaDeInicio)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_inicio");

                entity.Property(e => e.HoraDeFin).HasColumnName("Hora_de_fin");

                entity.Property(e => e.HoraDeInicio).HasColumnName("Hora_de_inicio");

                entity.Property(e => e.NombreDePista)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_de_pista");

                entity.Property(e => e.Pais).HasColumnType("text");

                entity.HasOne(d => d.CampeonatoNavigation)
                    .WithMany(p => p.Carreras)
                    .HasForeignKey(d => d.Campeonato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARRERA__Campeon__7E37BEF6");
            });

            modelBuilder.Entity<Paise>(entity =>
            {
                entity.HasKey(e => e.NombreP)
                    .HasName("PK__PAISES__8C9681EDEDA66823");

                entity.ToTable("PAISES");

                entity.HasIndex(e => e.NombreP, "UQ__PAISES__8C9681EC45FB0D6C")
                    .IsUnique();

                entity.Property(e => e.NombreP)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
