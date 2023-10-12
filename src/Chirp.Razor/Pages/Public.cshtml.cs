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
        bool hasPage = int.TryParse(Request.Query["page"], out var page);
        var PageInt = Math.Max(hasPage ? page : 1, 1);

        try
        {
            Cheeps = _service.GetCheeps();
            var cheepsPerPage = 32;
            var startcheep = ((PageInt - 1) * cheepsPerPage);

            var endCheep = PageInt * cheepsPerPage;
            if (startcheep > Cheeps.Count)
            {
                Cheeps = new List<CheepViewModel>();
            }

            else if (endCheep > Cheeps.Count)
            {
                var remnCheeps = Cheeps.Count - startcheep;
                Cheeps = Cheeps.GetRange(startcheep, remnCheeps);

            }
            else
            {
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



    }



}

