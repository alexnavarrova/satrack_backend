using Satrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Satrack.Domain;

namespace Satrack.Infraestructure.Persistence
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de TaskEntity
            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();
                entity.Property(e => e.IsCompleted).HasDefaultValue(false);
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Tasks)
                      .HasForeignKey(e => e.CategoryId);
            });

            // Configuración de CategoryEntity
            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasMany(e => e.Tasks)
                      .WithOne(t => t.Category)
                      .HasForeignKey(t => t.CategoryId);
            });
        }



        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

    }
}

