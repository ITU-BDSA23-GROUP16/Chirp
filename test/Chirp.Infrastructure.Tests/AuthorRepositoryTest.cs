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

    AuthorDTO saynabDTO, hermanDTO;
    Author herman;
    public AuthorRepTest()
    {
        //Arrange
        connection = new SqliteConnection("Filename=:memory:");
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection); 
        var option = builder.Options;
        context = new ChirpDBContext(option);
        connection.Open();
        
        saynabDTO = new AuthorDTO("Saynab", "saynab@jjj", new List<CheepDTO>());
        hermanDTO = new AuthorDTO("herman", "Herman@only.com", new List<CheepDTO>());
        herman = new Author { UserName = "herman", Email = "Herman@only.com" };
    }

    //the infrastructureTest needs a reference from infrastructure project
    [Fact]
    public async Task CreatedExists()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        
        //ActDTO
        await repository.CreateAuthor(saynabDTO);

        //Assert
        var created = await context.Authors.SingleOrDefaultAsync(c => c.UserName == "Saynab");
        Assert.NotNull(created);
    }
    // https://stackoverflow.com/questions/36856073/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-of-this-t
    [Fact]
    public async Task CreatedIsUnique()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);

        context.Authors.Add(herman);
        context.Entry(herman).State = EntityState.Detached;

        //Act
        await repository.CreateAuthor(hermanDTO);

        //Assert
        await Assert.ThrowsAsync<ArgumentException>( () =>  repository.CreateAuthor(hermanDTO));
        var author = await context.Authors.Where(c => c.UserName == "herman").ToListAsync();
        Assert.Single(author);
    }

    [Fact]
    public async Task FindByName()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);

        context.Authors.Add(herman);
        context.Entry(herman).State = EntityState.Detached;
        await repository.CreateAuthor(hermanDTO);

        //Act
        var author = await repository.FindAuthorByName("herman");

        //Assert
        Assert.Equal(author.Name, hermanDTO.Name);
    }

    [Fact]
    public async Task FindByEmail()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);

        context.Authors.Add(herman);
        context.Entry(herman).State = EntityState.Detached;
        await repository.CreateAuthor(hermanDTO);

        //Act
        var author = await repository.FindAuthorByEmail("Herman@only.com");

        //Assert
        Assert.Equal(herman.Email, author.Email);
    }
    
    [Fact]
    public async Task UsernameChanged()
    {
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        
        //ActDTO
        await repository.CreateAuthor(saynabDTO);

        //Assert
        var author = await context.Authors.SingleOrDefaultAsync(c => c.UserName == "Saynab");
        await repository.DeleteAuthor(author.UserName);

        Assert.Equal("HASBEENCHANGED", author.UserName);
    }

    public void Dispose()
    {
        connection.Dispose();
        context.Dispose();
        SqliteConnection.ClearAllPools();
    }
}