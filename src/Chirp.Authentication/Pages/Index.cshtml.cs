using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Chirp.Core;

namespace Chirp.Authentication.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ILogger<PostModel> _postlogger;
    private readonly ICheepRepository _repository;
    public IEnumerable<CheepDTO>? Cheeps { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ICheepRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<ActionResult> OnGetAsync()
    {
        bool hasPage = int.TryParse(Request.Query["page"], out var page);
        var PageInt = Math.Max(hasPage ? page : 1, 1);
        var cheepsPerPage = 32;
        Cheeps = await _repository.GetCheeps(cheepsPerPage, PageInt);
        return Page();

    }

    public async void OnPostAsync(string message)
    {
        var newCheep = new CheepDTO(User.Identity!.Name!, message, DateTime.Now);
        Console.WriteLine(DateTime.Now);
        await _repository.CreateCheep(newCheep);
    }



}