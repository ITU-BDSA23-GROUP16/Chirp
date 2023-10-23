namespace Chirp.Infrastructure;
public class Cheep
{

    public int CheepId { get; set; }

    public int AuthorId { get; set; }

    public Author? Author { get; set; } = null;


    //[StringLength(160, MinumumLength = 5)] // means that minumum it is 5 and maximun the text is 160
    public required string? Text { get; set; } // we have said it can not be null 

    public DateTime TimeStamp { get; set; }
}
