namespace Chirp.Infrastructure;

/// <summary>
/// ChirpDBContext represents a database session derived from IdentityDbContext.
/// It is used to query and save instances of Chirp! entities.
/// </summary>
/// <remarks>
/// Authors, Cheeps and Follows are equivalent to relations
/// </remarks>
public class ChirpDBContext : IdentityDbContext<Author>
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Follow> Follows { get; set; }


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


        modelBuilder.Entity<Follow>().HasKey(a => new { a.FollowerId, a.FollowingId });

        //Follows
        modelBuilder.Entity<Author>()
        .HasMany(e => e.Followers)
        .WithOne(e => e.Follower)
        .HasForeignKey(e => new { e.FollowerId })
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Author>()
        .HasMany(e => e.Followings)
        .WithOne(e => e.Following)
        .HasForeignKey(e => new { e.FollowingId })
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasPrincipalKey(e => e.Id);

    }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    //create the migration that construct the sqlite by using the UseSqlite, which means that it understands Sqlite


}