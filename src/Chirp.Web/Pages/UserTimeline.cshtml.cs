using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepRepository _repository;
    public List<CheepDTO>? Cheeps { get; set; }

    public UserTimelineModel(ICheepRepository repository)
    {
        _repository = repository;
    }

    public ActionResult OnGet(string author)
    {
        Cheeps = _repository.GetCheepsFromAuthor(author);
        return Page();
    }
}