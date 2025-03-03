using Microsoft.EntityFrameworkCore;

namespace BadCompany2.MasterServer.Data;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Personas)
            .WithOne(p => p.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Persona> Personas { get; set; }
}