using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;
namespace Chirp.Authentication.Pages;

public class TimelineModel : PageModel
{
    protected readonly ILogger<TimelineModel> _logger;
    protected readonly ICheepRepository _repository;
    protected readonly IAuthorRepository _aut;
    public IEnumerable<CheepDTO>? Cheeps { get; set; }
    protected int cheepsPerPage = 32;
    protected string print { get; set; }

    public TimelineModel(ILogger<TimelineModel> logger, ICheepRepository repository, IAuthorRepository aut)
    {
        _logger = logger;
        _repository = repository;
        _aut = aut;
    }

    public async Task<ActionResult> OnGetAsync(string author)
    {
        bool hasPage = int.TryParse(Request.Query["page"], out var page);
        var PageInt = Math.Max(hasPage ? page : 1, 1);

        if (author == null)
        {
            Cheeps = await _repository.GetCheeps(cheepsPerPage, PageInt);
        }
        else
        {
            Cheeps = await _repository.GetByAuthor(author);
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

}