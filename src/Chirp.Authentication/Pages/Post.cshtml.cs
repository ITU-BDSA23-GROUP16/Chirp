using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;

namespace Chirp.Authentication.Pages;

public class PostModel : PageModel
{
    private readonly ILogger<PostModel> _logger;
    private readonly ICheepRepository _repository;
    public IEnumerable<CheepDTO>? Cheeps { get; set; }

    public PostModel(ILogger<PostModel> logger, ICheepRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public void OnGet()
    {
    }
    public void OnPost(string message)
    {
        var newCheep = new CheepDTO(User.Identity!.Name!, message, DateTime.Now);
        _repository.CreateCheep(newCheep);
    }
}

