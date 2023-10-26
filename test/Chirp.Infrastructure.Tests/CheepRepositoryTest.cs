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
        var Cheep = new CheepDTO("Stanley", "Once upon a time", new DateTime(1698150571));
        await repository.Create(Cheep);
        //Assert
        var created = await context.Cheeps.SingleOrDefaultAsync(c => c.Text == "Once upon a time");
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
        var hermie = new Author { Name = "herman", Email = "Herman@only.com" };
        context.Cheeps.Add(new Cheep { Author = hermie, Text = "Herman@only.com" }); var repository = new CheepRepository(context);
        //ChirpRepository
        //Act
        var cheep = new CheepDTO("herman", "Herman@only.com", DateTime.Parse("2022-08-01 13:14:37"));
        await repository.Create(cheep);
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await repository.Create(cheep));
        var herman = await context.Cheeps.Where(c => c.Text == "Herman@only.com").ToListAsync();
        Assert.Single(herman);
    }
}