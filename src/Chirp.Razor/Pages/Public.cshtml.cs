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

            var cheepsPerPage = 32;
            var startcheep = ((PageInt-1)*cheepsPerPage);
            var endCheep = PageInt * cheepsPerPage;
            if(startcheep > Cheeps.Count) {
                Cheeps = new List<CheepViewModel>();
            } 
            
            else if (endCheep > Cheeps.Count) {
            var remCheeps = Cheeps.Count-startcheep;
            Cheeps = Cheeps.GetRange(startcheep, remCheeps);

            } else {
                
                Cheeps = Cheeps.GetRange(startcheep, cheepsPerPage);
            }
            

            
        }
        catch (ArgumentException e)
        {
            if (Cheeps.Count > _service.GetCheeps().Count)
            {
                throw new ArgumentException("Argument is invalid");
            }
        } 
        return Page();

        var pageQuery = Request.Query["page"].ToString();
        var hasPage = int.TryParse(Request.Query["page"], out var page);
        page = Math.Max(hasPage ? page : 1, 1);


        }    


     [BindProperty(SupportsGet = true)]
    public string? PageNumber { get; set; }
       
    }

 