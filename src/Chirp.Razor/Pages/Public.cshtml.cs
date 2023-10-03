using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel> Cheeps { get; set; }

    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    public ActionResult OnGet()
    {
        try
        {
            int PageInt;
            if (string.IsNullOrEmpty(PageNumber))
            {
                PageInt = 1;
            }
            else
            {
                PageInt = Int32.Parse(PageNumber);
            }
            Cheeps = _service.GetCheeps();


            Cheeps = Cheeps.GetRange(((PageInt - 1) * 32), 32);
            
        }
        catch (ArgumentException e)
        {
            if (Cheeps.Count > _service.GetCheeps().Count)
            {
                throw new ArgumentException("Argument is invalid");
            }
        } 
       return Page();
        }    


     [BindProperty(SupportsGet = true)]
    public string? PageNumber { get; set; }
       
    }

 