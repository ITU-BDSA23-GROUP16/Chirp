using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Chirp.Core;
using Microsoft.AspNetCore.Identity;
namespace Chirp.Web.Pages;

public class MyPageModel : PageModel
{


    protected readonly ILogger<MyPageModel> _logger;
    protected readonly IAuthorRepository _repository;
    private readonly SignInManager<Author> _signInManager;

    public string UserName;
    public string Email;



    public MyPageModel(ILogger<MyPageModel> logger, IAuthorRepository repository, SignInManager<Author> signInManager)
    {
        _logger = logger;
        _repository = repository;
        _signInManager = signInManager;

    }
    public async Task<ActionResult> OnGetAsync()
    {
        var aut = await _repository.FindAuthorByName(User!.Identity!.Name);
        //User.Identity!.Name!
        UserName = aut.Name;
        Email = aut.Email;

        return Page();
    }


    public async Task<ActionResult> OnPostDeleteAsync()
    {
        Console.WriteLine("hej her slet");
        //Console.WriteLine(author);

        if (User!.Identity!.Name != null)
        {
            Console.WriteLine("Hej efter if");
            await _signInManager.SignOutAsync();
            await _repository.DeleteAuthor(User!.Identity!.Name);
            // Console.WriteLine(_signInManager.IsSignedIn(User));
        }
        return Redirect("/");
    }
}