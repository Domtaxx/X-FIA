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
        public virtual DbSet<Pilot> Pilots { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<Realteam> Realteams { get; set; } = null!;
        public virtual DbSet<Subteam> Subteams { get; set; } = null!;
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
                    .HasName("PK__COUNTRY__737584F71FE6E6F4");

                entity.ToTable("COUNTRY");

                entity.HasIndex(e => e.Name, "UQ__COUNTRY__737584F693150D41")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.ToTable("PILOT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.RealTeamsName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryNameNavigation)
                    .WithMany(p => p.Pilots)
                    .HasForeignKey(d => d.CountryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PILOT__CountryNa__345EC57D");

                entity.HasOne(d => d.RealTeamsNameNavigation)
                    .WithMany(p => p.Pilots)
                    .HasForeignKey(d => d.RealTeamsName)
                    .HasConstraintName("FK__PILOT__RealTeams__3552E9B6");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.TournamentKey })
                    .HasName("PK__RACE__D54A887EBE6E66AA");

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
                    .HasConstraintName("FK__RACE__Country__27F8EE98");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.TournamentKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RACE__Tournament__2704CA5F");
            });

            modelBuilder.Entity<Realteam>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__REALTEAM__737584F727832C05");

                entity.ToTable("REALTEAMS");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);
            });

            modelBuilder.Entity<Subteam>(entity =>
            {
                entity.ToTable("SUBTEAMS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RealTeamsName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.RealTeamsNameNavigation)
                    .WithMany(p => p.Subteams)
                    .HasForeignKey(d => d.RealTeamsName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SUBTEAMS__RealTe__3B0BC30C");

                entity.HasOne(d => d.UserEmailNavigation)
                    .WithMany(p => p.Subteams)
                    .HasForeignKey(d => d.UserEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SUBTEAMS__UserEm__3A179ED3");

                entity.HasMany(d => d.Pilots)
                    .WithMany(p => p.SubTeams)
                    .UsingEntity<Dictionary<string, object>>(
                        "HasPilot",
                        l => l.HasOne<Pilot>().WithMany().HasForeignKey("PilotId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__HAS_PILOT__Pilot__40C49C62"),
                        r => r.HasOne<Subteam>().WithMany().HasForeignKey("SubTeamsId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__HAS_PILOT__SubTe__3FD07829"),
                        j =>
                        {
                            j.HasKey("SubTeamsId", "PilotId").HasName("PK__HAS_PILO__0F71292F4D262DDE");

                            j.ToTable("HAS_PILOT");

                            j.IndexerProperty<int>("SubTeamsId").HasColumnName("SubTeamsID");

                            j.IndexerProperty<int>("PilotId").HasColumnName("PilotID");
                        });
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__TOURNAME__C41E028874A29F40");

                entity.ToTable("TOURNAMENT");

                entity.HasIndex(e => e.Key, "UQ__TOURNAME__C41E0289A7FE25F2")
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
                    .HasName("PK__USER__A9D10535124FD5E2");

                entity.ToTable("USER");

                entity.HasIndex(e => e.Email, "UQ__USER__A9D1053475402906")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
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

                entity.Property(e => e.TeamsLogo).IsUnicode(false);

                entity.Property(e => e.TeamsName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryNameNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CountryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USER__CountryNam__24285DB4");

                entity.HasMany(d => d.TournamentKeys)
                    .WithMany(p => p.UserEmails)
                    .UsingEntity<Dictionary<string, object>>(
                        "Publicleague",
                        l => l.HasOne<Tournament>().WithMany().HasForeignKey("TournamentKey").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__Tourn__2AD55B43"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserEmail").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__UserE__2BC97F7C"),
                        j =>
                        {
                            j.HasKey("UserEmail", "TournamentKey").HasName("PK__PUBLICLE__AE5C8170058F20E5");

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
