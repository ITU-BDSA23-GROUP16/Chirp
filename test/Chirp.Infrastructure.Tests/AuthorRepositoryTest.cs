namespace Chirp.Infrastructure.Tests;
/// <summary>
/// Integration test between AuthorRepository and ChirpDBContext
/// These are tightly coupled
/// In most tests, the authors are inserted using the context, and methods of the repository are then tested.
/// </summary>

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

        //Act
        await repository.CreateAuthor(saynabDTO);
        var created = await context.Authors.SingleOrDefaultAsync(c => c.UserName == "Saynab");

        //Assert
        Assert.NotNull(created);
    }

    [Fact]
    public async Task CreatedIsUnique()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);

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
        context.SaveChanges();


        //Act
        var author = await repository.FindAuthorByName("herman");

        //Assert
        Assert.Equal(hermanDTO.Name, author.Name);
    }

    [Fact]
    public async Task FindByEmail()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        await repository.CreateAuthor(hermanDTO);

        //Act
        var author = await repository.FindAuthorByEmail("Herman@only.com");

        //Assert
        Assert.Equal(hermanDTO.Email, author.Email);
    }




    [Fact]
    public async Task FollowerExist()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        await repository.CreateAuthor(saynabDTO);
        await repository.CreateAuthor(hermanDTO);

        //Act
        await repository.CreateFollow(saynabDTO, hermanDTO);
        var created = await context.Follows.SingleOrDefaultAsync(c => c.Follower.UserName == "Saynab");

        //Assert

        Assert.NotNull(created);
    }


    [Fact]
    public async Task FindFollower()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);

        await repository.CreateAuthor(saynabDTO);
        await repository.CreateAuthor(hermanDTO);
        await repository.CreateFollow(saynabDTO, hermanDTO);

        //Act
        IEnumerable<AuthorDTO> followed = await repository.GetFollowed("herman");
        AuthorDTO actual = followed.ElementAt(0);

        //Assert
        Assert.Equal(saynab.UserName, actual.Name);
    }

    [Fact]
    public async Task FindFollowings()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        await repository.CreateAuthor(saynabDTO);
        await repository.CreateAuthor(hermanDTO);
        await repository.CreateFollow(saynabDTO, hermanDTO);

        //Act
        IEnumerable<AuthorDTO> followed = await repository.GetFollowing("Saynab");
        AuthorDTO actual = followed.ElementAt(0);

        //Assert
        Assert.Equal(herman.UserName, actual.Name);
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

        //Act
        await repository.RemoveFollow(saynabDTO, hermanDTO);
        var created = await context.Follows.SingleOrDefaultAsync(c => c.Follower.UserName == "Saynab" && c.Following.UserName == "herman");

        //Assert
        Assert.Null(created);
    }

    [Fact]
    public async Task RemoveUser()
    {
        //Arrange
        await context.Database.EnsureCreatedAsync();
        repository = new AuthorRepository(context);
        await repository.CreateAuthor(saynabDTO);


        //Act
        await repository.DeleteAuthor(saynabDTO.Name);
        var created = await context.Authors.SingleOrDefaultAsync(c => c.UserName == "Saynab");

        //Assert
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

        //Act
        var created = await repository.FollowExists(saynabDTO, hermanDTO);

        //Assert
        Assert.True(created);
    }


    public void Dispose()
    {
        connection.Dispose();
        context.Dispose();
        SqliteConnection.ClearAllPools();
    }
}