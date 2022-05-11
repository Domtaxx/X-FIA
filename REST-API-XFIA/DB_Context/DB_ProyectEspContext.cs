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
                entity.HasKey(e => e.Llave)
                    .HasName("PK__CAMPEONA__8E70B293E5DA5323");

                entity.ToTable("CAMPEONATO");

                entity.HasIndex(e => e.Llave, "UQ__CAMPEONA__8E70B292A63A0402")
                    .IsUnique();

                entity.Property(e => e.Llave)
                    .HasMaxLength(6)
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

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.CampeonatoKey })
                    .HasName("PK__CARRERA__52861B8150FB26D3");

                entity.ToTable("CARRERA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CampeonatoKey)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Campeonato_key");

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

                entity.Property(e => e.Pais)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CampeonatoKeyNavigation)
                    .WithMany(p => p.Carreras)
                    .HasForeignKey(d => d.CampeonatoKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARRERA__Campeon__690797E6");

                entity.HasOne(d => d.PaisNavigation)
                    .WithMany(p => p.Carreras)
                    .HasForeignKey(d => d.Pais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARRERA__Pais__69FBBC1F");
            });

            modelBuilder.Entity<Paise>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("PK__PAISES__75E3EFCE55DD334F");

                entity.ToTable("PAISES");

                entity.HasIndex(e => e.Nombre, "UQ__PAISES__75E3EFCFC61E931B")
                    .IsUnique();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
