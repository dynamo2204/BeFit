using BeFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BeFit.Models.ExerciseType> ExerciseType { get; set; } = default!;
        public DbSet<BeFit.Models.TrainingSession> TrainingSession { get; set; } = default!;
        public DbSet<BeFit.Models.CompletedExercise> CompletedExercise { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminRoleId = Guid.NewGuid().ToString();
            var adultRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = adultRoleId, Name = "Adult", NormalizedName = "ADULT" }
            );

            modelBuilder.Entity<ExerciseType>().HasData(
                new ExerciseType { Id = 1, Name = "Wyciskanie sztangi leżąc" },
                new ExerciseType { Id = 2, Name = "Przysiad ze sztangą" },
                new ExerciseType { Id = 3, Name = "Martwy ciąg" },
                new ExerciseType { Id = 4, Name = "Wyciskanie sztangi nad głowę" },
                new ExerciseType { Id = 5, Name = "Podciąganie na drążku" },
                new ExerciseType { Id = 6, Name = "Wiosłowanie sztangą" },
                new ExerciseType { Id = 7, Name = "Pompki" },
                new ExerciseType { Id = 8, Name = "Dipsy" },
                new ExerciseType { Id = 9, Name = "Uginanie ramion ze sztangą" },
                new ExerciseType { Id = 10, Name = "Wyciskanie francuskie" },
                new ExerciseType { Id = 11, Name = "Wznosy boczne" },
                new ExerciseType { Id = 12, Name = "Wspięcia na palce" }
            );

            modelBuilder.Entity<TrainingSession>()
                .HasOne(ts => ts.CreatedBy)
                .WithMany()
                .HasForeignKey(ts => ts.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompletedExercise>()
                .HasOne(ce => ce.CreatedBy)
                .WithMany()
                .HasForeignKey(ce => ce.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompletedExercise>()
                .HasOne(ce => ce.TrainingSession)
                .WithMany(ts => ts.CompletedExercises)
                .HasForeignKey(ce => ce.TrainingSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompletedExercise>()
                .HasOne(ce => ce.ExerciseType)
                .WithMany(et => et.CompletedExercises)
                .HasForeignKey(ce => ce.ExerciseTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}