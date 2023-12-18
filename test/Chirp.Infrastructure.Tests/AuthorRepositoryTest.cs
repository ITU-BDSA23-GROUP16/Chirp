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

public class AuthorRepTest : IDisposable
{
    AuthorRepository? repository;
    ChirpDBContext context;
    SqliteConnection connection;

    AuthorDTO saynabDTO, hermanDTO;
    Author herman, saynab;
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
        saynab = new Author { UserName = "Saynab", Email = "saynab@jjj" };
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
        await Assert.ThrowsAsync<ArgumentException>(() => repository.CreateAuthor(hermanDTO));
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
    public async Task FollowerExist()
    {

        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        await repository.CreateAuthor(saynabDTO);
        await repository.CreateAuthor(hermanDTO);

        //ActDTO
        await repository.CreateFollow(saynabDTO, hermanDTO);

        //Assert
        var created = await context.Follows.SingleOrDefaultAsync(c => c.Follower.UserName == "Saynab");

        Assert.NotNull(created);
    }


    [Fact]
    public async Task FindFollower()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);


        context.Authors.Add(saynab);
        context.Entry(saynab).State = EntityState.Detached;

        await repository.CreateAuthor(saynabDTO);
        await repository.CreateAuthor(hermanDTO);

        await repository.CreateFollow(saynabDTO, hermanDTO);

        //Act
        IEnumerable<AuthorDTO> followed = await repository.GetFollowed("herman");
        AuthorDTO expected = followed.ElementAt(0);

        //Assert
        Assert.Equal(saynab.UserName, expected.Name);
    }

    [Fact]
    public async Task FindFollowings()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);


        context.Authors.Add(saynab);
        context.Entry(saynab).State = EntityState.Detached;

        await repository.CreateAuthor(saynabDTO);
        await repository.CreateAuthor(hermanDTO);

        await repository.CreateFollow(saynabDTO, hermanDTO);

        //Act
        IEnumerable<AuthorDTO> followed = await repository.GetFollowing("Saynab");
        AuthorDTO expected = followed.ElementAt(0);

        //Assert
        Assert.Equal(herman.UserName, expected.Name);
    }

    [Fact]
    public async Task RemoveFollower()
    {

        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        await repository.CreateAuthor(saynabDTO);
        await repository.CreateAuthor(hermanDTO);
        await repository.CreateFollow(saynabDTO, hermanDTO);

        //ActDTO
        await repository.RemoveFollow(saynabDTO, hermanDTO);

        //Assert
        var created = await context.Follows.SingleOrDefaultAsync(c => c.Follower.UserName == "Saynab" && c.Following.UserName == "herman");


        Assert.Null(created);
    }

    [Fact]
    public async Task DoesFollowExist()
    {

        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        await repository.CreateAuthor(saynabDTO);
        await repository.CreateAuthor(hermanDTO);
        await repository.CreateFollow(saynabDTO, hermanDTO);

        //ActDTO
        var created = await repository.FollowExists(saynabDTO, hermanDTO);

        //Assert
        //Assert.NotNull(created);
        Assert.True(created);

    }


    public void Dispose()
    {
        connection.Dispose();
        context.Dispose();
        SqliteConnection.ClearAllPools();
    }
}