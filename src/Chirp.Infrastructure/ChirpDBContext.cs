using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;
public class ChirpDBContext : DbContext
{
    //Name of tables are Authors and Cheeps
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cheep> Cheeps { get; set; }

    public string DbPath { get; }

    public ChirpDBContext(DbContextOptions options): base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Fluent API does not support minimum length
        //modelBuilder.Entity<Cheep>().Property(c => c.Text).HasMaxLength(160);
        modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(100);
        modelBuilder.Entity<Author>().Property(a => a.Email).HasMaxLength(50);
        
    }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    //create the migration that construct the sqlite by using the UseSqlite, which means that it understands Sqlite


}