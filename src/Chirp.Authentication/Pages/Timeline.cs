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
    protected string print { get; set; }


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

        if (author == null)
        {
            Cheeps = await _repository.GetCheeps(cheepsPerPage, PageInt);

        } else if (author == User.Identity!.Name!) {
            Cheeps = await _repository.GetByFollower(author);
        } else {

            Cheeps = await _repository.GetByAuthor(author);

        }
        
        if (Cheeps==null) {
        Cheeps = await _repository.GetCheeps(32,1);
        }

        return Page();
    }

    public async Task<ActionResult> OnPostAsync(string message)
    {
        var newCheep = new CheepDTO(User.Identity!.Name!, message, DateTime.Now);
        Console.WriteLine(DateTime.Now);
        Console.WriteLine(newCheep.TimeStamp);
        await _repository.CreateCheep(newCheep);

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