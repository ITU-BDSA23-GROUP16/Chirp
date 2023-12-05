using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Chirp.Core;
namespace Chirp.Web.Pages;

public class MyPageModel : PageModel
{


    protected readonly ILogger<TimelineModel> _logger;
    protected readonly IAuthorRepository _repository;



    public MyPageModel(ILogger<TimelineModel> logger, IAuthorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    public void OnGet()
    {
    }


    public async Task OnPostDeleteAsync(string author)
    {
        if (author == null)
        {
            Console.WriteLine("hej");
        }
        else
        {
            await _repository.DeleteAuthor(author);
        }
    }
}