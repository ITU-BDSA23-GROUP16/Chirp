using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
namespace Chirp.Infrastructure.Tests;

/*
Tests:
    Create a new Author in the database
    Find Author by name
    Find Author by email
*/

public class AuthorRepTest{


    
    //the infrastructurtest needs a reference from infrasturctor project
    [Fact]
    public async void AddAuthor(){
        //Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        //run the test and then kill the test that what connection.open do connection.Open(); ChirpDBContext doesn't take builder.Options
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection); using var context = new ChirpDBContext();
        await context.Database.EnsureCreatedAsync();
        var repository = new AuthorRepository(context);
        //ChirpRepository

        //Act
        var author = new AuthorDTO("Saynab", "saynab@jjj", new List<CheepDTO>());
        await repository.Create(author);
        //Assert
        var created = await context.Authors.SingleOrDefaultAsync(c =>c.Name == "Saynab"); 
        Assert.NotNull(created);
    }
    [Fact]
    public async void CreateAuthor(){
        //Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        //run the test and then kill the test that what connection.open do connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection); using var context = new ChirpDBContext();
        await context.Database.EnsureCreatedAsync();
        context.Authors.Add(new Author{ Name = "herman", Email = "Herman@only.com"}); 
        var repository = new AuthorRepository(context);
        //ChirpRepository
        //Act
        var author = new AuthorDTO("herman", "Herman@only.com", new List<CheepDTO>());
        await repository.Create(author);
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(async() => await repository.Create(author)); 
        var herman = await context.Authors.Where(c => c.Name == "herman").ToListAsync(); 
        Assert.Single(herman);
    } 
   
}