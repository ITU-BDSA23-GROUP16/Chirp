using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepRepository _repository;
    public IEnumerable<CheepDTO>? Cheeps { get; set; }

    public UserTimelineModel(ICheepRepository repository)
    {
        _repository = repository;
    }

    public async Task<ActionResult> OnGetAsync(string author)
    {
        Cheeps = await _repository.GetByAuthor(author);
        return Page();
    }
}