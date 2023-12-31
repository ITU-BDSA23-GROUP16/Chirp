using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;
using NuGet.Protocol.Core.Types;
namespace Chirp.Web.Pages;

/// <summary>
/// TimelineModel inherits from PageModel. It defines the methods for handling posting of cheeps and following users.
/// </summary>

public class TimelineModel : PageModel
{
    protected readonly ILogger<TimelineModel> _logger;
    protected readonly ICheepRepository _repository;
    protected readonly IAuthorRepository _authors;

    public IEnumerable<CheepDTO> Cheeps { get; set; } = new List<CheepDTO>();
    protected int cheepsPerPage = 32;
    public bool hasPage;
    public int PageInt = 1;
    public int CheepCount;

    public int Pages;

    public string Author = "";

    public TimelineModel(ILogger<TimelineModel> logger, ICheepRepository repository, IAuthorRepository authors)
    {
        _logger = logger;
        _repository = repository;
        _authors = authors;
    }

    public async Task<ActionResult> OnPostAsync(string message)
    {
        if (message != null && message.Length > 5 && message.Length < 160)
        {
            var newCheep = new CheepDTO(User.Identity!.Name!, message, DateTime.Now);
            await _repository.CreateCheep(newCheep);
            ViewData["confirmation"] = $"";
        }
        else
        {
            ViewData["confirmation"] = $"Cheeps must be between 5 and 160 characters";
        }
        return RedirectToPage();
    }


    public async Task<ActionResult> OnPostUpdateAsync(string follow)
    {
        AuthorDTO following = await _authors.FindAuthorByName(follow);
        AuthorDTO follower = await _authors.FindAuthorByName(User.Identity!.Name!);

        if (following != null && follower != null)
        {


            if (await _authors.FollowExists(follower, following))
            {

                await _authors.RemoveFollow(follower, following);

            }
            else
            {

                await _authors.CreateFollow(follower, following);


            }

        }

        return RedirectToPage();

    }


    public async Task<bool> IfAuthorExists(string author)
    {

        AuthorDTO aut = await _authors.FindAuthorByName(author);
        if (aut==null) return false; else return true;
    }
    public async Task<bool> IfFollowExists(string follow)
    {

        AuthorDTO following = await _authors.FindAuthorByName(follow);
        AuthorDTO follower = await _authors.FindAuthorByName(User.Identity!.Name!);
        return await _authors.FollowExists(follower, following);

    }
}

/// <summary>
/// PublicTimeline inherits from TimelineModel. It defines the method for retrieving relevant cheeps through a GET request.
/// </summary>
public class PublicTimeline : TimelineModel
{
    public PublicTimeline(ILogger<TimelineModel> logger, ICheepRepository repository, IAuthorRepository authors)
    : base(logger, repository, authors)
    {
    }
    public async Task<ActionResult> OnGetAsync(string author)
    {
        hasPage = int.TryParse(Request.Query["page"], out var page);
        PageInt = Math.Max(hasPage ? page : 1, 1) - 1;
        Author = author;

        if (author == null)
        {
            Cheeps = await _repository.GetCheeps(cheepsPerPage, PageInt);
            CheepCount = (await _repository.GetAllCheeps()).Count();
            Pages = (int)Math.Ceiling(CheepCount / cheepsPerPage * 1.0);
        }
        else
        {
            Cheeps = await _repository.GetByAuthor(author, cheepsPerPage, PageInt);
            if (Cheeps.Count() != 0)
            {
                CheepCount = (await _repository.GetAllByAuthor(author)).Count();
                Pages = (int)Math.Ceiling(CheepCount / cheepsPerPage * 1.0);
            }
        }
        if (Cheeps == null)
        {
            Cheeps = await _repository.GetCheeps(32, 1);
            CheepCount = (await _repository.GetAllCheeps()).Count();
            Pages = (int)Math.Ceiling(CheepCount / cheepsPerPage * 1.0);
        }

        return Page();

    }
}

/// <summary>
/// FollowTimeline inherits from TimelineModel. It defines the method for retrieving relevant Cheeps through a GET request. 
/// Also handles a POST request redirecting to an endpoint displaying followed users.
/// </summary>
public class FollowTimeline : TimelineModel
{
    public FollowTimeline(ILogger<TimelineModel> logger, ICheepRepository repository, IAuthorRepository authors)
    : base(logger, repository, authors)
    {
    }
    public async Task<ActionResult> OnGetAsync(string author)
    {
        hasPage = int.TryParse(Request.Query["page"], out var page);
        PageInt = Math.Max(hasPage ? page : 1, 1) - 1;
        Author = author;


        Cheeps = await _repository.GetByFollower(User.Identity!.Name!, cheepsPerPage, PageInt);
        CheepCount = Cheeps.Count();
        Pages = (int)Math.Ceiling(CheepCount / cheepsPerPage * 1.0);

        return Page();
    }

    public IActionResult OnPostFollowing() {
        var toReturn = "/author/" + User!.Identity!.Name + "/following";
        return Redirect(toReturn);

    }

}
