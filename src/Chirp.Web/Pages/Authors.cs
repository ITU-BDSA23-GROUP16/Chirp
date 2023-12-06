using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;
using NuGet.Protocol.Core.Types;
namespace Chirp.Web.Pages;

public class AuthorsModel : PageModel
{
    protected readonly ILogger<AuthorsModel> _logger;
    protected readonly IAuthorRepository _repository;

    public IEnumerable<AuthorDTO>? Authors { get; set; }
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

            Authors = await _repository.GetFollowing(author);
            AuthorCount = (await _repository.GetFollowing(author)).Count();
            Pages = (int)Math.Ceiling(AuthorCount / authorsPerPage * 1.0);
        

        return Page();
    }
}