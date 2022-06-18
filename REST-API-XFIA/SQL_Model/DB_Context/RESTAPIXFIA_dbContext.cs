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
        public virtual DbSet<HasPilot> HasPilots { get; set; } = null!;
        public virtual DbSet<Pilot> Pilots { get; set; } = null!;
        public virtual DbSet<PilotRace> PilotRaces { get; set; } = null!;
        public virtual DbSet<Privateleague> Privateleagues { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<RealTeamRace> RealTeamRaces { get; set; } = null!;
        public virtual DbSet<Realteam> Realteams { get; set; } = null!;
        public virtual DbSet<Subteam> Subteams { get; set; } = null!;
        public virtual DbSet<SubteamPoint> SubteamPoints { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=XFIA;Persist Security Info=False;User ID=Project_Espe;Password=Server2022;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__COUNTRY__737584F746A5B2A8");

                entity.ToTable("COUNTRY");

                entity.HasIndex(e => e.Name, "UQ__COUNTRY__737584F6D3B9213D")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);
            });

            modelBuilder.Entity<HasPilot>(entity =>
            {
                entity.HasKey(e => new { e.SubTeamsId, e.PilotId })
                    .HasName("PK__HAS_PILO__0F71292F291194F1");

                entity.ToTable("HAS_PILOT");

                entity.Property(e => e.SubTeamsId).HasColumnName("SubTeamsID");

                entity.Property(e => e.PilotId)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PilotID");

                entity.Property(e => e.DummyData).HasColumnName("dummyData");

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.HasPilots)
                    .HasForeignKey(d => d.PilotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HAS_PILOT__Pilot__7908F585");

                entity.HasOne(d => d.SubTeams)
                    .WithMany(p => p.HasPilots)
                    .HasForeignKey(d => d.SubTeamsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HAS_PILOT__SubTe__7814D14C");
            });

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.ToTable("PILOT");

                entity.Property(e => e.Id)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("ID");

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
                    .HasConstraintName("FK__PILOT__CountryNa__74444068");

                entity.HasOne(d => d.RealTeamsNameNavigation)
                    .WithMany(p => p.Pilots)
                    .HasForeignKey(d => d.RealTeamsName)
                    .HasConstraintName("FK__PILOT__RealTeams__753864A1");
            });

            modelBuilder.Entity<PilotRace>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.TournamentKey, e.PilotId })
                    .HasName("PK__PilotRac__B9F98D2F2281CB74");

                entity.ToTable("PilotRace");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TournamentKey)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PilotId)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PilotID");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.PilotRaces)
                    .HasForeignKey(d => d.PilotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PilotRace__Pilot__7BE56230");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.PilotRaces)
                    .HasForeignKey(d => new { d.Name, d.TournamentKey })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PilotRace__7CD98669");
            });

            modelBuilder.Entity<Privateleague>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__PRIVATEL__737584F7614EBD59");

                entity.ToTable("PRIVATELEAGUE");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MaxUser).HasColumnName("maxUser");

                entity.Property(e => e.OwnerEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PrivateLeagueKey)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.TournamentKey)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.OwnerEmailNavigation)
                    .WithMany(p => p.Privateleagues)
                    .HasForeignKey(d => d.OwnerEmail)
                    .HasConstraintName("FK__PRIVATELE__Owner__68D28DBC");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.Privateleagues)
                    .HasForeignKey(d => d.TournamentKey)
                    .HasConstraintName("FK__PRIVATELE__Tourn__67DE6983");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.TournamentKey })
                    .HasName("PK__RACE__D54A887EED9F61E1");

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
                    .HasConstraintName("FK__RACE__Country__61316BF4");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.TournamentKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RACE__Tournament__603D47BB");
            });

            modelBuilder.Entity<RealTeamRace>(entity =>
            {
                entity.HasKey(e => new { e.RealTeamName, e.TournamentKey, e.Name })
                    .HasName("PK__RealTeam__329B512FA09F531B");

                entity.ToTable("RealTeamRace");

                entity.Property(e => e.RealTeamName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TournamentKey)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Points).HasColumnName("points");

                entity.HasOne(d => d.RealTeamNameNavigation)
                    .WithMany(p => p.RealTeamRaces)
                    .HasForeignKey(d => d.RealTeamName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RealTeamR__RealT__7FB5F314");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.RealTeamRaces)
                    .HasForeignKey(d => new { d.Name, d.TournamentKey })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RealTeamRace__00AA174D");
            });

            modelBuilder.Entity<Realteam>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__REALTEAM__737584F7C78E5B8E");

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

                entity.Property(e => e.CreationDate).HasColumnType("date");

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
                    .HasConstraintName("FK__SUBTEAMS__RealTe__7167D3BD");

                entity.HasOne(d => d.UserEmailNavigation)
                    .WithMany(p => p.Subteams)
                    .HasForeignKey(d => d.UserEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SUBTEAMS__UserEm__7073AF84");
            });

            modelBuilder.Entity<SubteamPoint>(entity =>
            {
                entity.HasKey(e => new { e.TournamentKey, e.SubTeamId })
                    .HasName("PK__SubteamP__B014B616BC22A2B4");

                entity.Property(e => e.TournamentKey)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Points).HasColumnName("points");

                entity.HasOne(d => d.SubTeam)
                    .WithMany(p => p.SubteamPoints)
                    .HasForeignKey(d => d.SubTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubteamKey");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.SubteamPoints)
                    .HasForeignKey(d => d.TournamentKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubteamPo__Tourn__6D9742D9");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__TOURNAME__C41E0288DE48C259");

                entity.ToTable("TOURNAMENT");

                entity.HasIndex(e => e.Key, "UQ__TOURNAME__C41E02898337E2C8")
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
                    .HasName("PK__USER__A9D10535100C8BBD");

                entity.ToTable("USER");

                entity.HasIndex(e => e.Email, "UQ__USER__A9D10534A84C36BD")
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
                    .HasConstraintName("FK__USER__CountryNam__5D60DB10");

                entity.HasOne(d => d.PrivateLeagueNameNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PrivateLeagueName)
                    .HasConstraintName("PrivateLeague_User_key");

                entity.HasMany(d => d.TournamentKeys)
                    .WithMany(p => p.UserEmails)
                    .UsingEntity<Dictionary<string, object>>(
                        "Publicleague",
                        l => l.HasOne<Tournament>().WithMany().HasForeignKey("TournamentKey").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__Tourn__640DD89F"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserEmail").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__UserE__6501FCD8"),
                        j =>
                        {
                            j.HasKey("UserEmail", "TournamentKey").HasName("PK__PUBLICLE__AE5C81704BD70CA3");

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
