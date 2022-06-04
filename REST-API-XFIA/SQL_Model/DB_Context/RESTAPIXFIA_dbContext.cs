using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.SQL_Model.DB_Context
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
        public virtual DbSet<Privateleague> Privateleagues { get; set; } = null!;
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
                    .HasName("PK__COUNTRY__737584F7DBAA639E");

                entity.ToTable("COUNTRY");

                entity.HasIndex(e => e.Name, "UQ__COUNTRY__737584F6132F3A4B")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);
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
                    .HasConstraintName("FK__PILOT__CountryNa__5E3FF0B0");

                entity.HasOne(d => d.RealTeamsNameNavigation)
                    .WithMany(p => p.Pilots)
                    .HasForeignKey(d => d.RealTeamsName)
                    .HasConstraintName("FK__PILOT__RealTeams__5F3414E9");
            });

            modelBuilder.Entity<Privateleague>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__PRIVATEL__737584F7E3827124");

                entity.ToTable("PRIVATELEAGUE");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MaxUser).HasColumnName("maxUser");

                entity.Property(e => e.OwnerEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TournamentKey)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.OwnerEmailNavigation)
                    .WithMany(p => p.Privateleagues)
                    .HasForeignKey(d => d.OwnerEmail)
                    .HasConstraintName("FK__PRIVATELE__Owner__55AAAAAF");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.Privateleagues)
                    .HasForeignKey(d => d.TournamentKey)
                    .HasConstraintName("FK__PRIVATELE__Tourn__54B68676");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.TournamentKey })
                    .HasName("PK__RACE__D54A887E67D66AFD");

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
                    .HasConstraintName("FK__RACE__Country__4E0988E7");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.TournamentKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RACE__Tournament__4D1564AE");
            });

            modelBuilder.Entity<Realteam>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__REALTEAM__737584F779E8604C");

                entity.ToTable("REALTEAMS");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Logo).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);
            });

            modelBuilder.Entity<Subteam>(entity =>
            {
                entity.ToTable("SUBTEAMS");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

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
                    .HasConstraintName("FK__SUBTEAMS__RealTe__5B638405");

                entity.HasOne(d => d.UserEmailNavigation)
                    .WithMany(p => p.Subteams)
                    .HasForeignKey(d => d.UserEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SUBTEAMS__UserEm__5A6F5FCC");

                entity.HasMany(d => d.Pilots)
                    .WithMany(p => p.SubTeams)
                    .UsingEntity<Dictionary<string, object>>(
                        "HasPilot",
                        l => l.HasOne<Pilot>().WithMany().HasForeignKey("PilotId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__HAS_PILOT__Pilot__6304A5CD"),
                        r => r.HasOne<Subteam>().WithMany().HasForeignKey("SubTeamsId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__HAS_PILOT__SubTe__62108194"),
                        j =>
                        {
                            j.HasKey("SubTeamsId", "PilotId").HasName("PK__HAS_PILO__0F71292FE72A4086");

                            j.ToTable("HAS_PILOT");

                            j.IndexerProperty<int>("SubTeamsId").HasColumnName("SubTeamsID");

                            j.IndexerProperty<int>("PilotId").HasColumnName("PilotID");
                        });
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__TOURNAME__C41E028822043EC1");

                entity.ToTable("TOURNAMENT");

                entity.HasIndex(e => e.Key, "UQ__TOURNAME__C41E0289467D2419")
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
                    .HasName("PK__USER__A9D1053565120E95");

                entity.ToTable("USER");

                entity.HasIndex(e => e.Email, "UQ__USER__A9D105344437C316")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PrivateLeagueName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TeamsLogo).IsUnicode(false);

                entity.Property(e => e.TeamsName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryNameNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CountryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USER__CountryNam__4A38F803");

                entity.HasOne(d => d.PrivateLeagueNameNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PrivateLeagueName)
                    .HasConstraintName("PrivateLeague_User_key");

                entity.HasMany(d => d.TournamentKeys)
                    .WithMany(p => p.UserEmails)
                    .UsingEntity<Dictionary<string, object>>(
                        "Publicleague",
                        l => l.HasOne<Tournament>().WithMany().HasForeignKey("TournamentKey").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__Tourn__50E5F592"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserEmail").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__UserE__51DA19CB"),
                        j =>
                        {
                            j.HasKey("UserEmail", "TournamentKey").HasName("PK__PUBLICLE__AE5C8170936B3875");

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
