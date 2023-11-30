using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Fluent API does not support minimum length
        //modelBuilder.Entity<Cheep>().Property(c => c.Message).HasMaxLength(160);
        modelBuilder.Entity<Author>().Property(a => a.UserName).HasMaxLength(100);
        modelBuilder.Entity<Author>().Property(a => a.Email).HasMaxLength(50);
        modelBuilder.Entity<Author>()
        .HasMany(e => e.Cheeps)
        .WithOne(e => e.Author)
        .HasForeignKey(e => e.AuthorId)
        .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Author>().HasQueryFilter(a => !a.IsDeleted);

    }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    //create the migration that construct the sqlite by using the UseSqlite, which means that it understands Sqlite


}