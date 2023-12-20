using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Chirp.Core;
using Microsoft.AspNetCore.Identity;
namespace Chirp.Web.Pages;


/// <summary>
/// This class displays the username and email of the user that is signed in.
/// Since the Email is the same as the Username, the same name is displayed twice.
/// This page also contains the 'Forget Me' button that deletes the user and automatically logs them out when it's pressed.
/// </summary>


public class MyPageModel : PageModel
{


    protected readonly ILogger<MyPageModel> _logger;
    protected readonly IAuthorRepository _repository;
    private readonly SignInManager<Author> _signInManager;

    public string UserName = "";
    public string Email = "";



    public MyPageModel(ILogger<MyPageModel> logger, IAuthorRepository repository, SignInManager<Author> signInManager)
    {
        _logger = logger;
        _repository = repository;
        _signInManager = signInManager;

    }
    public async Task<ActionResult> OnGetAsync()
    {
        var aut = await _repository.FindAuthorByName(User!.Identity!.Name!);
        if(aut != null)
        {
        UserName = aut.Name;
        Email = aut.Email;
        }
        return Page();
    }


    public async Task<ActionResult> OnPostDeleteAsync()
    {

        if (User!.Identity!.Name != null)
        {
            await _repository.DeleteAuthor(User!.Identity!.Name);
            await _signInManager.SignOutAsync();
        }
        return RedirectToPage();
    }
}