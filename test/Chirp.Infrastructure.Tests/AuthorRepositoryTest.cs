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

public class AuthorRepTest: IDisposable
{
    AuthorRepository? repository;
    ChirpDBContext context;
    SqliteConnection connection;

    AuthorDTO saynabDTO,hermanDTO;
    Author herman;
    public AuthorRepTest(){
        //Arrange
        connection = new SqliteConnection("Filename=:memory:");
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection); 
        context = new ChirpDBContext();
        
        saynabDTO = new AuthorDTO("Saynab", "saynab@jjj", new List<CheepDTO>());
        hermanDTO = new AuthorDTO("herman", "Herman@only.com", new List<CheepDTO>());
        herman = new Author { Name = "herman", Email = "Herman@only.com" };
    }

    //the infrastructurtest needs a reference from infrasturctor project
    [Fact]
    public async void CreatedExists()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        
        //ActDTO
        repository.CreateAuthor(saynabDTO);

        //Assert
        var created = await context.Authors.SingleOrDefaultAsync(c => c.Name == "Saynab");
        Assert.NotNull(created);
    }
    [Fact]
    public async void CreatedIsUnique()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);

        context.Authors.Add(herman);

        //Act
        repository.CreateAuthor(hermanDTO);

        //Assert
        await Assert.ThrowsAsync<ArgumentException>( () =>  repository.CreateAuthor(hermanDTO));
        var author = await context.Authors.Where(c => c.Name == "herman").ToListAsync();
        Assert.Single(author);
    }

    [Fact]
    public async void FindByName()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);

        context.Authors.Add(herman);

        //Act
        repository.CreateAuthor(hermanDTO);

        //Assert
        var author = repository.FindAuthorByName("herman");
        Assert.Equal(author, hermanDTO);
    }

    [Fact]
    public async void FindByEmail()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);

        context.Authors.Add(herman);
        repository.CreateAuthor(hermanDTO);

        //Act
        var author =  repository.FindAuthorByEmail("Herman@only.com");

        //Assert
        Assert.Equal(herman.Email, author.Email);
    }
    
    public void Dispose()
    {
        connection.Dispose();
        context.Dispose();
        SqliteConnection.ClearAllPools();
    }
}