using Microsoft.EntityFrameworkCore;
using global::PAS.Models;

namespace PAS.Data
{
    public class PASDbContext : DbContext
    {
        public DbSet<AnneeAcademique> AnneeAcademiques { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Cour> Cours { get; set; }
        public DbSet<Professeur> Professeurs { get; set; }
        public DbSet<Heure> Heures { get; set; }
        public DbSet<Jour> Jours { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }

        public PASDbContext(DbContextOptions<PASDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classe>()
                .HasOne(c => c.AnneeAcademique)
                .WithMany(a => a.Classes)
                .HasForeignKey(c => c.AnneeAcademiqueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cour>()
                .HasOne(c => c.Classe)
                .WithMany(c => c.Cours)
                .HasForeignKey(c => c.ClasseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cour>()
                .HasMany(c => c.Professeurs)
                .WithMany(p => p.Cours)
                .UsingEntity(j => j.ToTable("CourProfesseur"));

            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.Heures)
                .WithOne(h => h.Professeur)
                .HasForeignKey(h => h.ProfesseurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.Jours)
                .WithOne(j => j.Professeur)
                .HasForeignKey(j => j.ProfesseurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Etudiant>()
                .HasMany(e => e.Classes)
                .WithMany(c => c.Etudiants)
                .UsingEntity(j => j.ToTable("EtudiantClasse"));

            modelBuilder.Entity<Etudiant>()
                .HasMany(e => e.Cours)
                .WithMany(c => c.Etudiants)
                .UsingEntity(j => j.ToTable("EtudiantCours"));
        }
    }
}
