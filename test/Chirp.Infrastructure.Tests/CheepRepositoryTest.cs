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

public class CheepRepTest
{
    //the infrastructurtest needs a reference from infrasturctor project
    [Fact]
    public async void AddCheep()
    {
        //Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        //run the test and then kill the test that what connection.open do connection.Open(); ChirpDBContext doesn't take builder.Options
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);
        using var context = new ChirpDBContext();
        await context.Database.EnsureCreatedAsync();
        //ChirpRepository
        var repository = new CheepRepository(context);
        var Cheep = new CheepDTO("Stanley", "Once upon a time", new DateTime(1698150571));

        //Act
        await repository.Create(Cheep);
        //Assert
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Once upon a time");
        Assert.NotNull(created);
    }
    //Identical Cheeps are allowed to exist
    [Fact]
    public async void GetPublicCheeps() //Does not currently test that page sizes work correctly
    {
        //Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        //run the test and then kill the test that what connection.open do connection.Open(); ChirpDBContext doesn't take builder.Options
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);
        using var context = new ChirpDBContext();
        await context.Database.EnsureCreatedAsync();
        //ChirpRepository
        var repository = new CheepRepository(context);
        var cheep = new CheepDTO("Stanley", "Once upon a time", new DateTime(1698150571));
        await repository.Create(cheep);
        cheep = new CheepDTO("herman", "Herman@only.com", DateTime.Parse("2022-08-01 13:14:37"));
        await repository.Create(cheep);
        cheep = new CheepDTO("Stanley", "Hello World", DateTime.Parse("2022-12-01 17:14:37"));
        await repository.Create(cheep);

        //Act
        
        var created0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Once upon a time");
        var herman0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Herman@only.com");

        IEnumerable<CheepDTO> cheeps = repository.GetCheeps();

        //Assert
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Once upon a time");
        var herman = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Herman@only.com");

        //assert that no changes to the data have occurred during query
            Assert.Equal(created,created0);
            Assert.Equal(herman,herman0);
        // Make second variable that gets a cheep with the same text from the list
        var cheep0=cheeps.ElementAt(0);
        var cheep1=cheeps.ElementAt(1);
        if (cheep0.Message.Equals("Once upon a time")) {
            Assert.Equal(created,cheep0);
            Assert.Equal(herman,cheep1);
        } else {
            Assert.Equal(created,cheep1);
            Assert.Equal(herman,cheep0);
        }
        //Compare the two
    }

    [Fact]
    public async void GetAuthorCheeps()
    {
        //Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        //run the test and then kill the test that what connection.open do connection.Open(); ChirpDBContext doesn't take builder.Options
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);
        using var context = new ChirpDBContext();
        await context.Database.EnsureCreatedAsync();
        //ChirpRepository
        var repository = new CheepRepository(context);
        var cheep = new CheepDTO("herman", "Once upon a time", new DateTime(1698150571));
        await repository.Create(cheep);
        cheep = new CheepDTO("herman", "Herman@only.com", DateTime.Parse("2022-08-01 13:14:37"));
        await repository.Create(cheep);
        cheep = new CheepDTO("Stanley", "Hello World", DateTime.Parse("2022-12-01 17:14:37"));
        await repository.Create(cheep);

        //Act
        
        var created0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Once upon a time");
        var herman0 = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Herman@only.com");

        IEnumerable<CheepDTO> cheeps = repository.GetByAuthor("herman");

        //Assert
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Once upon a time");
        var herman = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Herman@only.com");
        //assert that no changes to the data have occurred during query
            Assert.Equal(created,created0);
            Assert.Equal(herman,herman0);

        IEnumerable<Cheep> created = await context.Cheeps.AllAsync(c => c.Author == "herman");
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
        Assert.Equal(resultdto,result);
    }
}