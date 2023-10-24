using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
namespace Chirp.Infrastructure.Tests;

public class AuthorRepTest{
//the infrastructurtest needs a reference from infrasturctor project
    [Fact]
    public async void Adds_author_to_databse(){
    //Arrange
    using var connection = new SqliteConnection("Filename=:memory:");
    //run the test and then kill the test that what connection.open do connection.Open(); ChirpDBContext doesn't take builder.Options
    var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection); using var context = new ChirpDBContext();
    await context.Database.EnsureCreatedAsync();
    var repository = new AuthorRepository(context);
    //ChirpRepository

    //Act
    var author = new AuthorDTO() {Name = "Saynab", Email = "saynab@jjj"};
    await repository.Create(author);
    //Assert
    var created = await context.Authors.SingleOrDefaultAsync(c =>c.Name == "Saynab"); 
    Assert.NotNull(created);
    }
    [Fact]
    public async void Create(){
    //Arrange
    using var connection = new SqliteConnection("Filename=:memory:");
    //run the test and then kill the test that what connection.open do connection.Open();
    var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection); using var context = new ChirpDBContext();
    await context.Database.EnsureCreatedAsync();
    context.Authors.Add(new Author{ Name = "herman", Email = "Herman@only.com"}); var repository = new AuthorRepository(context);
    //ChirpRepository
    //Act
    var author = new AuthorDTO{ Name = "herman", Email = "Herman@only.com" };
    await repository.Create(author);
    //Assert
    await Assert.ThrowsAsync<ArgumentException>(async() => await repository.Create(author)); 
    var herman = await context.Authors.Where(c => c.Name == "Saynab").ToListAsync(); 
    Assert.Single(herman);
    } 
}