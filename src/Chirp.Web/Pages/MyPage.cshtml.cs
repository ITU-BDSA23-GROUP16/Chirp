using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Chirp.Core;
using Microsoft.AspNetCore.Identity;
namespace Chirp.Web.Pages;

public class MyPageModel : PageModel
{


    protected readonly ILogger<TimelineModel> _logger;
    protected readonly IAuthorRepository _repository;
    private readonly SignInManager<Author> _signInManager;

    public string UserName;
    public string Email;



    public MyPageModel(ILogger<TimelineModel> logger, IAuthorRepository repository, SignInManager<Author> signInManager)
    {
        _logger = logger;
        _repository = repository;
        _signInManager = signInManager;

    }
    public async void OnGetAsync(string author)
    {
       var aut = await _repository.FindAuthorByName(author);
        //User.Identity!.Name!
        UserName = aut.Name;
        Email = aut.Email; 

    }


    public async Task<IActionResult> OnPostDeleteAsync(string author)
    {
        if (author == null)
        {
            Console.WriteLine("hej");
        }
        else
        {
            await _repository.DeleteAuthor(author);
            await _signInManager.SignOutAsync();

        }
        return Redirect("/");
    }
}