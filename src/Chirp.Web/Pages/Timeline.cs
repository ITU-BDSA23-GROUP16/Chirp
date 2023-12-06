using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;
using NuGet.Protocol.Core.Types;
namespace Chirp.Web.Pages;

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

    public string Author="";

    public TimelineModel(ILogger<TimelineModel> logger, ICheepRepository repository, IAuthorRepository authors)
    {
        _logger = logger;
        _repository = repository;
        _authors = authors;
    }

    public async Task<ActionResult> OnPostAsync(string message)
    {
        var newCheep = new CheepDTO(User.Identity!.Name!, message, DateTime.Now);
        await _repository.CreateCheep(newCheep);

        return RedirectToPage();

    }


    public async Task<ActionResult> OnPostUpdateAsync(string follow)
    {
        AuthorDTO following = await _authors.FindAuthorByName(follow);
        AuthorDTO follower = await _authors.FindAuthorByName(User.Identity!.Name!);

        if(following !=null && follower != null){


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


    public async Task<bool> IfFollowExists(string follow)
    {

        AuthorDTO following = await _authors.FindAuthorByName(follow);
        AuthorDTO follower = await _authors.FindAuthorByName(User.Identity!.Name!);
        return await _authors.FollowExists(follower, following);

    }

}
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
            if(Cheeps.Count() == 0) {
                Console.WriteLine("hej");
            } else {
            CheepCount = (await _repository.GetAllByAuthor(author)).Count();    
            Pages = (int)Math.Ceiling(CheepCount / cheepsPerPage * 1.0);
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
}
