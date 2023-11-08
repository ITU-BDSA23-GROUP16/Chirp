using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Chirp.Core;

namespace Chirp.Authentication.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICheepRepository _repository;
    public IEnumerable<CheepDTO>? Cheeps { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ICheepRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public  async Task<ActionResult> OnGetAsync()
    {
        bool hasPage = int.TryParse(Request.Query["page"], out var page);
        var PageInt = Math.Max(hasPage ? page : 1, 1);
        var cheepsPerPage = 32;
        Cheeps = await _repository.GetCheeps(cheepsPerPage, PageInt);
        /*
                try
                {

                    //var startcheep = (PageInt - 1) * cheepsPerPage;


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
                    if (Cheeps.Count > _repository.GetCheeps(cheepsPerPage, PageInt).Count)
                    {
                        throw new ArgumentException("Argument is invalid");
                    }
                }
                */
        return Page();



    }



}