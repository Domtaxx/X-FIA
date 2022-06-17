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
                    .HasName("PK__COUNTRY__737584F710E0CCDE");

                entity.ToTable("COUNTRY");

                entity.HasIndex(e => e.Name, "UQ__COUNTRY__737584F6E719061E")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);
            });

            modelBuilder.Entity<HasPilot>(entity =>
            {
                entity.HasKey(e => new { e.SubTeamsId, e.PilotId })
                    .HasName("PK__HAS_PILO__0F71292FA0988A48");

                entity.ToTable("HAS_PILOT");

                entity.Property(e => e.SubTeamsId).HasColumnName("SubTeamsID");

                entity.Property(e => e.PilotId).HasColumnName("PilotID");

                entity.Property(e => e.DummyData).HasColumnName("dummyData");

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.HasPilots)
                    .HasForeignKey(d => d.PilotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HAS_PILOT__Pilot__6D181FEC");

                entity.HasOne(d => d.SubTeams)
                    .WithMany(p => p.HasPilots)
                    .HasForeignKey(d => d.SubTeamsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HAS_PILOT__SubTe__6C23FBB3");
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
                    .HasConstraintName("FK__PILOT__CountryNa__68536ACF");

                entity.HasOne(d => d.RealTeamsNameNavigation)
                    .WithMany(p => p.Pilots)
                    .HasForeignKey(d => d.RealTeamsName)
                    .HasConstraintName("FK__PILOT__RealTeams__69478F08");
            });

            modelBuilder.Entity<PilotRace>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.TournamentKey, e.PilotId })
                    .HasName("PK__PilotRac__B9F98D2F41D5A2E8");

                entity.ToTable("PilotRace");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TournamentKey)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PilotId).HasColumnName("PilotID");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.PilotRaces)
                    .HasForeignKey(d => d.PilotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PilotRace__Pilot__6FF48C97");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.PilotRaces)
                    .HasForeignKey(d => new { d.Name, d.TournamentKey })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PilotRace__70E8B0D0");
            });

            modelBuilder.Entity<Privateleague>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__PRIVATEL__737584F7260B2E43");

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
                    .HasConstraintName("FK__PRIVATELE__Owner__5CE1B823");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.Privateleagues)
                    .HasForeignKey(d => d.TournamentKey)
                    .HasConstraintName("FK__PRIVATELE__Tourn__5BED93EA");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.TournamentKey })
                    .HasName("PK__RACE__D54A887ED78F66E0");

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
                    .HasConstraintName("FK__RACE__Country__5540965B");

                entity.HasOne(d => d.TournamentKeyNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.TournamentKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RACE__Tournament__544C7222");
            });

            modelBuilder.Entity<RealTeamRace>(entity =>
            {
                entity.HasKey(e => new { e.RealTeamName, e.TournamentKey, e.Name })
                    .HasName("PK__RealTeam__329B512F0C8C7F49");

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
                    .HasConstraintName("FK__RealTeamR__RealT__73C51D7B");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.RealTeamRaces)
                    .HasForeignKey(d => new { d.Name, d.TournamentKey })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RealTeamRace__74B941B4");
            });

            modelBuilder.Entity<Realteam>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__REALTEAM__737584F7E5C0129F");

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
                    .HasConstraintName("FK__SUBTEAMS__RealTe__6576FE24");

                entity.HasOne(d => d.UserEmailNavigation)
                    .WithMany(p => p.Subteams)
                    .HasForeignKey(d => d.UserEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SUBTEAMS__UserEm__6482D9EB");
            });

            modelBuilder.Entity<SubteamPoint>(entity =>
            {
                entity.HasKey(e => new { e.TournamentKey, e.SubTeamId })
                    .HasName("PK__SubteamP__B014B616174D8358");

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
                    .HasConstraintName("FK__SubteamPo__Tourn__61A66D40");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__TOURNAME__C41E0288981E75F1");

                entity.ToTable("TOURNAMENT");

                entity.HasIndex(e => e.Key, "UQ__TOURNAME__C41E0289EB9C6740")
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
                    .HasName("PK__USER__A9D10535B8646937");

                entity.ToTable("USER");

                entity.HasIndex(e => e.Email, "UQ__USER__A9D10534E1833FCB")
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
                    .HasConstraintName("FK__USER__CountryNam__51700577");

                entity.HasOne(d => d.PrivateLeagueNameNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PrivateLeagueName)
                    .HasConstraintName("PrivateLeague_User_key");

                entity.HasMany(d => d.TournamentKeys)
                    .WithMany(p => p.UserEmails)
                    .UsingEntity<Dictionary<string, object>>(
                        "Publicleague",
                        l => l.HasOne<Tournament>().WithMany().HasForeignKey("TournamentKey").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__Tourn__581D0306"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserEmail").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PUBLICLEA__UserE__5911273F"),
                        j =>
                        {
                            j.HasKey("UserEmail", "TournamentKey").HasName("PK__PUBLICLE__AE5C81701CD0072B");

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
