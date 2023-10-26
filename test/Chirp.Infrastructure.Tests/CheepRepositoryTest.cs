using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
namespace Chirp.Infrastructure.Tests;

public class CheepRepTest
{
    //the infrastructurtest needs a reference from infrasturctor project
    [Fact]
    public async void AddCheep()
    {
        //Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        //run the test and then kill the test that what connection.open do connection.Open(); ChirpDBContext doesn't take builder.Options
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection); using var context = new ChirpDBContext();
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        //ChirpRepository

        //Act
        var Cheep = new CheepDTO() { Author = "Stanley", Message = "Once upon a time", TimeStamp = 1698150571 };
        await repository.Create(Cheep);
        //Assert
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Author == "Stanley");
        Assert.NotNull(created);
    }
    [Fact]
    public async void CreateCheep()
    {
        //Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        //run the test and then kill the test that what connection.open do connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection); using var context = new ChirpDBContext();
        await context.Database.EnsureCreatedAsync();
        context.Cheeps.Add(new Cheep { Author = "herman", Text = "Herman@only.com" }); var repository = new CheepRepository(context);
        //ChirpRepository
        //Act
        var cheep = new CheepDTO { Author = "herman", Text = "Herman@only.com" };
        await repository.Create(Cheep);
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await repository.Create(Cheep));
        var herman = await context.Cheeps.Where(c => c.Name == "Saynab").ToListAsync();
        Assert.Single(herman);
    }
}