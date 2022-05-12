using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.DB_Context
{
    public partial class RESTAPIXFIA_dbContext : DbContext
    {
        public RESTAPIXFIA_dbContext()
        {
        }

        public RESTAPIXFIA_dbContext(DbContextOptions<RESTAPIXFIA_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:rest-api-xfiadbserver.database.windows.net,1433;Initial Catalog=REST-API-XFIA_db;Persist Security Info=False;User ID=Project_Espe;Password=Server2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__COUNTRY__737584F72C0A4F83");

                entity.ToTable("COUNTRY");

                entity.HasIndex(e => e.Name, "UQ__COUNTRY__737584F61AA5C13D")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.TournamentKey })
                    .HasName("PK__RACE__D54A887E820D26A3");

                entity.ToTable("RACE");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TournamentKey)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FinalDate).HasColumnType("date");

                entity.Property(e => e.InitialDate).HasColumnType("date");

                entity.Property(e => e.TrackName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RACE__Country__787EE5A0");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.TournamentKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RACE__Tournament__778AC167");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__TOURNAME__C41E0288727D5F3E");

                entity.ToTable("TOURNAMENT");

                entity.HasIndex(e => e.Key, "UQ__TOURNAME__C41E0289ADC7F183")
                    .IsUnique();

                entity.Property(e => e.Key)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FinalDate).HasColumnType("date");

                entity.Property(e => e.InitialDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Rules)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__USER__A9D105355B62A7AB");

                entity.ToTable("USER");

                entity.HasIndex(e => e.Email, "UQ__USER__A9D10534B37BBD5F")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USER__Country__74AE54BC");

                entity.HasMany(d => d.TournamentKeys)
                    .WithMany(p => p.UserEmails)
                    .UsingEntity<Dictionary<string, object>>(
                        "Publicleague",
                        l => l.HasOne<Tournament>().WithMany().HasForeignKey("TournamentKey").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__Tourn__7B5B524B"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserEmail").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__UserE__7C4F7684"),
                        j =>
                        {
                            j.HasKey("UserEmail", "TournamentKey").HasName("PK__PUBLICLE__AE5C8170EA8A5835");

                            j.ToTable("PUBLICLEAGUE");

                            j.IndexerProperty<string>("UserEmail").HasMaxLength(30).IsUnicode(false);

                            j.IndexerProperty<string>("TournamentKey").HasMaxLength(6).IsUnicode(false);
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
