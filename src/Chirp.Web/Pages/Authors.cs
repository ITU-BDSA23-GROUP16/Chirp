using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Chirp.Web.Pages;


/// <summary>
/// AuthorsModel class inherits from PageModel
/// Contains properties for storing a collection of authors, authors to display per page, page number and total number of authors and pages
/// It also checks whether another user is followed
/// </summary>

public class AuthorsModel : PageModel
{
    protected readonly ILogger<AuthorsModel> _logger;
    protected readonly IAuthorRepository _repository;

    public IEnumerable<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    protected int authorsPerPage = 32;
    public bool hasPage;
    public int PageInt = 1;
    public int AuthorCount;

    public int Pages;

    public AuthorsModel(ILogger<AuthorsModel> logger, IAuthorRepository repository)
    {
        _logger = logger;
        _repository = repository;

    }

    public async Task<bool> IfFollowExists(string follow)
    {

        AuthorDTO following = await _repository.FindAuthorByName(follow);
        AuthorDTO follower = await _repository.FindAuthorByName(User.Identity!.Name!);
        return await _repository.FollowExists(follower, following);

    }

}

/// <summary>
/// FollowerAuthorsModel inherits from AuthorsModel
/// It retrieves the authors that the user is following
/// </summary>

public class FollowingAuthorsModel : AuthorsModel
{
    public FollowingAuthorsModel(ILogger<AuthorsModel> logger, IAuthorRepository repository)
    : base(logger, repository)
    {
    }
    public async Task<ActionResult> OnGetAsync(string author)
    {
        hasPage = int.TryParse(Request.Query["page"], out var page);
        PageInt = Math.Max(hasPage ? page : 1, 1) - 1;

            Authors = await _repository.GetFollowing(author, authorsPerPage, PageInt);
            AuthorCount = (await _repository.GetAllFollowing(author)).Count();
            Pages = (int)Math.Ceiling(AuthorCount / authorsPerPage * 1.0);
        

        return Page();
    }
}

/// <summary>
/// AllAuthorsModel inherits from AuthorsModel
/// It retrieves all of the users in the application
/// </summary>


public class AllAuthorsModel : AuthorsModel
{
    public AllAuthorsModel(ILogger<AuthorsModel> logger, IAuthorRepository repository)
    : base(logger, repository)
    {
    }
    public async Task<ActionResult> OnGetAsync(string author)
    {
        hasPage = int.TryParse(Request.Query["page"], out var page);
        PageInt = Math.Max(hasPage ? page : 1, 1) - 1;

            Authors = await _repository.GetAuthors(authorsPerPage, PageInt);
            AuthorCount = (await _repository.GetAllAuthors()).Count();
            Pages = (int)Math.Ceiling(AuthorCount / authorsPerPage * 1.0);
        

        return Page();
    }
}