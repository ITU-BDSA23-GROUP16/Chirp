using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Authentication.Pages;

public class PostModel : PageModel
{
    public String Text { get; set; }
    private readonly ILogger<PostModel> _logger;

    public PostModel(ILogger<PostModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
    public void OnPost(string message)
    {
        Console.WriteLine($"{message}");
    }
}

