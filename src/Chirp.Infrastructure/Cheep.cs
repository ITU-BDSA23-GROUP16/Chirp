using System.ComponentModel.DataAnnotations;
namespace Chirp.Infrastructure;
public class Cheep
{

    public int CheepId { get; set; }

    public int AuthorId { get; set; }

    public required Author Author { get; set; }


    [StringLength(160, MinimumLength = 5)] // means that minimum it is 5 and maximum the text is 160
    
    public required string Text { get; set; } // we have said it can not be null 

    public DateTime TimeStamp { get; set; }
}
