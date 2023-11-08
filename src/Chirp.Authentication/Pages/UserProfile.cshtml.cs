using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;

namespace Chirp.Authentication.Pages;

public class UserProfileModel : PageModel
{
    private readonly IAuthorRepository _repository;
    //public IEnumerable<CheepDTO>? Cheeps { get; set; }
    public AuthorDTO Name { get; set; }
    public string Email { get; set; }

    public UserProfileModel(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ActionResult> OnGetAsync(string author)
    {

        Name = await _repository.FindAuthorByName(author);
        return Page();
    }
}