namespace Chirp.Infrastructure.Tests;
/// <summary>
/// Integration test between CheepRepository and ChirpDBContext
/// These are tightly coupled.
/// </summary> 
/// <remarks>
/// 
/// </remarks>

public class CheepRepTest : IDisposable
{
    CheepRepository? repository;
    ChirpDBContext context;
    SqliteConnection connection;

    CheepDTO stanleyDTO, hermanDTO, helloDTO;
    Author stanley, herman;

    public CheepRepTest()
    {
        //Arrange
        connection = new SqliteConnection("Filename=:memory:");
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);
        connection.Open();
        var option = builder.Options;
        context = new ChirpDBContext(option);

        stanleyDTO = new CheepDTO("Stanley", "Once upon a time", DateTimeOffset.FromUnixTimeSeconds(1698150571).UtcDateTime);
        hermanDTO = new CheepDTO("herman", "Herman@only.com", DateTime.Parse("2022-08-01 13:14:37"));
        helloDTO = new CheepDTO("Stanley", "Hello World", DateTime.Parse("2022-12-01 17:14:37"));
        stanley = new Author { UserName = "Stanley", Email = "swlc@jjj" };
        herman = new Author { UserName = "herman", Email = "Herman@only.com" };
    }

    [Fact]
    public async Task AddCheep()
    {
        //Arrange
        await Arrange();

        //Act
        await repository!.CreateCheep(helloDTO);

        //Assert
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Hello World");
        Assert.NotNull(created);
    }

    [Fact]
    public async Task GetPublicCheeps()
    {
        //Arrange
        await Arrange();

        //Act
        Cheep? created = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Once upon a time");
        Cheep? herman = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Herman@only.com");

        IEnumerable<CheepDTO> cheeps = await repository!.GetAllCheeps();

        //Assert
        await EnsureUnchanged(created!, herman!);
        
        CheepDTO cheep0 = cheeps.ElementAt(0);
        CheepDTO cheep1 = cheeps.ElementAt(1);
        if (cheep0.Message.Equals("Once upon a time"))
        {
            Assert.Equal(created!.Message, cheep0.Message);
            Assert.Equal(herman!.Message, cheep1.Message);
        }
        else
        {
            Assert.Equal(created!.Message, cheep1.Message);
            Assert.Equal(herman!.Message, cheep0.Message);
        }
        
    }

    [Fact]
    public async Task GetAuthorCheeps()
    {
        //Arrange
        await Arrange();

        //Act
        var created0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Once upon a time");
        var herman0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Herman@only.com");

        IEnumerable<CheepDTO> cheeps = await repository!.GetByAuthor("herman");

        //Assert
        await EnsureUnchanged(created0!, herman0!);

        IEnumerable<Cheep> created = await context.Cheeps.Where(c => c.Author.UserName == "herman").ToListAsync();
        //Following iteration has been found from this: https://stackoverflow.com/questions/168901/count-the-items-from-a-ienumerablet-without-iterating
        int result = 0;
        using (IEnumerator<Cheep> enumerator = created.GetEnumerator())
        {
            while (enumerator.MoveNext())
                result++;
        }
        int resultdto = 0;
        using (IEnumerator<CheepDTO> enumerator = cheeps.GetEnumerator())
        {
            while (enumerator.MoveNext())
                resultdto++;
        }
        Assert.Equal(resultdto, result);
    }

    [Fact]
    public async Task GetFollowerCheeps()
    {
        //Arrange
        await Arrange();

        //Act
        var created0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Once upon a time");
        var herman0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Herman@only.com");

        IEnumerable<CheepDTO> cheeps = await repository!.GetByFollower("herman");

        //Assert
        await EnsureUnchanged(created0!, herman0!);

        IEnumerable<Cheep> created = await context.Cheeps.Where(c => c.Author.UserName == "Stanley").ToListAsync();
        //Following iteration has been found from this: https://stackoverflow.com/questions/168901/count-the-items-from-a-ienumerablet-without-iterating
        int result = 0;
        using (IEnumerator<Cheep> enumerator = created.GetEnumerator())
        {
            while (enumerator.MoveNext())
                result++;
        }
        int resultdto = 0;
        using (IEnumerator<CheepDTO> enumerator = cheeps.GetEnumerator())
        {
            while (enumerator.MoveNext())
                resultdto++;
        }
        Assert.Equal(resultdto, result);
    }

    [Fact]
    public async Task GetPages()
    {
        //Arrange
        await Arrange();
        await repository!.CreateCheep(helloDTO);

        //Act
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Once upon a time");
        var herman = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Herman@only.com");

        IEnumerable<CheepDTO> page1 = await repository.GetCheeps(1);
        IEnumerable<CheepDTO> page2 = await repository.GetCheeps(1, 2);

        //Assert
        await EnsureUnchanged(created!, herman!);
        var first1 = page1.ElementAt(0);
        var first2 = page2.ElementAt(0);

        Assert.Equal(first1, stanleyDTO);
        Assert.Equal(first2, hermanDTO);
    }


    public void Dispose()
    {
        connection.Dispose();
        context.Dispose();
        SqliteConnection.ClearAllPools();
    }
    private async Task EnsureUnchanged(Cheep created0, Cheep herman0)
    {
        //Act
        var created1 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == created0.Message);
        var herman1 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == herman0.Message);

        //Assert
        Assert.Equal(created1, created0);
        Assert.Equal(herman1, herman0);
    }
    private async Task Arrange()
    {
        await context.Database.EnsureCreatedAsync();
        repository = new CheepRepository(context);
        await context.Authors.AddAsync(herman);
        await context.Authors.AddAsync(stanley);
        context.SaveChanges();
        
        await repository.CreateCheep(stanleyDTO);
        await repository.CreateCheep(hermanDTO);

        //Herman follows Stanley
        await context.Follows.AddAsync(new Follow
        {
            FollowerId = herman.Id,
            FollowingId = stanley.Id,
            Follower = herman,
            Following = stanley
        });
        context.SaveChanges();
    }
}