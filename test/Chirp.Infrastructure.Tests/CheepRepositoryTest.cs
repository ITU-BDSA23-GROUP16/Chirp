using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
namespace Chirp.Infrastructure.Tests;

/*
Tests:
    Retrieve Cheeps from a page of the Public Timeline
    Retrieve Cheeps from a page of an Author's timeline
    Create a new cheep in the database

    Queries do not change database
*/

public class CheepRepTest : IDisposable
{
    CheepRepository? repository;
    ChirpDBContext context;
    SqliteConnection connection;

    CheepDTO stanleyDTO, hermanDTO, helloDTO;

    public CheepRepTest()
    {
        //Arrange
        connection = new SqliteConnection("Filename=:memory:");
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);
        context = new ChirpDBContext();

        stanleyDTO = new CheepDTO("Stanley", "Once upon a time", new DateTime(1698150571));
        hermanDTO = new CheepDTO("herman", "Herman@only.com", DateTime.Parse("2022-08-01 13:14:37"));
        helloDTO = new CheepDTO("Stanley", "Hello World", DateTime.Parse("2022-12-01 17:14:37"));
    }

    [Fact]
    public async void AddCheep()
    {
        //Arrange
        Arrange();

        //Act
        repository!.CreateCheep(helloDTO);
        //Assert
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Hello World");
        Assert.NotNull(created);
    }
    //Identical Cheeps are allowed to exist
    [Fact]
    public async void GetPublicCheeps()
    {
        //Arrange
        Arrange();

        //Act
        Cheep? created = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Once upon a time");
        Cheep? herman = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Herman@only.com");

        IEnumerable<CheepDTO> cheeps = repository!.GetCheeps();

        //Assert
        EnsureUnchanged(created!, herman!);
        // Make second variable that gets a cheep with the same text from the list
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
        //Compare the two
    }

    [Fact]
    public async void GetAuthorCheeps()
    {
        //Arrange
        Arrange();

        //Act
        var created0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Once upon a time");
        var herman0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Herman@only.com");

        IEnumerable<CheepDTO> cheeps = repository!.GetByAuthor("herman");

        //Assert
        EnsureUnchanged(created0!, herman0!);

        IEnumerable<Cheep> created = await context.Cheeps.Where(c => c.Author.Name == "herman").ToListAsync();
        //https://stackoverflow.com/questions/168901/count-the-items-from-a-ienumerablet-without-iterating
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
    public async void GetPages()
    {
        //Arrange
        Arrange();
        repository!.CreateCheep(helloDTO);

        //Act
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Once upon a time");
        var herman = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == "Herman@only.com");

        IEnumerable<CheepDTO> page1 = repository.GetCheeps(2);
        IEnumerable<CheepDTO> page2 = repository.GetCheeps(2, 2);

        //Assert
        EnsureUnchanged(created!, herman!);
        var first1 = page1.ElementAt(0);
        var first2 = page2.ElementAt(0);
        //if hermanDTO is from August, then it is the earliest and should be on page 2
        Assert.Equal(first1, stanleyDTO);
        Assert.Equal(first2, hermanDTO);
    }

    public void Dispose()
    {
        connection.Dispose();
        context.Dispose();
        SqliteConnection.ClearAllPools();
    }
    private async void EnsureUnchanged(Cheep created0, Cheep herman0)
    {
        //Assert
        var created1 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == created0.Message);
        var herman1 = await context.Cheeps.SingleOrDefaultAsync(c => c.Message == herman0.Message);

        //assert that no changes to the data have occurred during query
        Assert.Equal(created1, created0);
        Assert.Equal(herman1, herman0);
    }
    private async void Arrange()
    {
        await context.Database.EnsureCreatedAsync();
        repository = new CheepRepository(context);
        repository.CreateCheep(stanleyDTO);
        repository.CreateCheep(hermanDTO);
    }
}