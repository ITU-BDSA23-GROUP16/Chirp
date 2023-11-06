using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Data.Entity;

/*namespace Chirp.Authentication.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
*/



namespace Chirp.Infrastructure;
public class ChirpDBContext : IdentityDbContext<Author>
{
    //Name of tables are Authors and Cheeps
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cheep> Cheeps { get; set; }

    public string DbPath { get; }

public ChirpDBContext(DbContextOptions<ChirpDBContext> options)
            : base(options)
    {

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "chirp.db");
        Console.WriteLine($"Database path: {DbPath}.");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Fluent API does not support minimum length
        //modelBuilder.Entity<Cheep>().Property(c => c.Text).HasMaxLength(160);
        modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(100);
        modelBuilder.Entity<Author>().Property(a => a.Email).HasMaxLength(50);

    }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    //create the migration that construct the sqlite by using the UseSqlite, which means that it understands Sqlite

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}