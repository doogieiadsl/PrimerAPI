using Microsoft.EntityFrameworkCore;
using PrimerAPI.Models;

namespace PrimerAPI.Data
{
    public class PrimerApiDbContext : DbContext 
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public PrimerApiDbContext(DbContextOptions options) : base(options)
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuracion Tabla User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(p=> p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(200);
            });
            // Configuracion tabal TaskItem
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
                entity.Property(p => p.IsCompleted).HasDefaultValue(false);

            // Relacion: Un user tienen muchas Tasks, Una Task requiere un user
            entity.HasOne(p => p.User).WithMany(u => u.Tasks).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
            });
            

        }
    }
}
