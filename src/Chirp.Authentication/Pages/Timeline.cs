using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;
namespace Chirp.Authentication.Pages;

public class TimelineModel : PageModel
{
    protected readonly ILogger<TimelineModel> _logger;
    protected readonly ICheepRepository _repository;
    protected readonly IAuthorRepository _authors;
    public IEnumerable<CheepDTO>? Cheeps { get; set; }
    protected int cheepsPerPage = 32;


    public TimelineModel(ILogger<TimelineModel> logger, ICheepRepository repository, IAuthorRepository authors)
    {
        _logger = logger;
        _repository = repository;
        _authors = authors;
    }

    public async Task<ActionResult> OnGetAsync(string author)
    {
        bool hasPage = int.TryParse(Request.Query["page"], out var page);
        var PageInt = Math.Max(hasPage ? page : 1, 1);
        
        string debug=null;
        if (author == null){
            debug="Vi er inde i PublicTimeline";
            Cheeps = await _repository.GetCheeps(cheepsPerPage, PageInt);
        } else if (author == User.Identity!.Name!) {
            debug="Vi er inde i GetByFollower";
            Cheeps = await _repository.GetByFollower(author);
        } else {
            debug="Vi er inde i GetByAuthor";
            Cheeps = await _repository.GetByAuthor(author);

        }
            if (Cheeps==null) {
            Console.WriteLine(debug);
            Console.WriteLine(author);
            Cheeps = await _repository.GetCheeps(32, 1);
            }
        
        return Page();
    }

public async Task<ActionResult> OnPostUpdateAsync(string follow)
    {
        AuthorDTO following= await _authors.FindAuthorByName(follow);
        AuthorDTO follower= await _authors.FindAuthorByName(User.Identity!.Name!);
        

        if(await _authors.FollowExists(follower, following)){
            
            await _authors.RemoveFollow(follower, following);

        } else{

            await _authors.CreateFollow(follower,following);
            
            ////@try {Model.Cheeps!.Any();} catch (ArgumentNullException e) {Console.WriteLine(e); Console.WriteLine(Model.Cheeps.ToString());}

        }
        return RedirectToPage();
        
    }

   
    public async Task<Boolean> IfFollowExists(string follow){

        AuthorDTO following= await _authors.FindAuthorByName(follow);
        AuthorDTO follower= await _authors.FindAuthorByName(User.Identity!.Name!);
        return await _authors.FollowExists(follower, following);
    }

}