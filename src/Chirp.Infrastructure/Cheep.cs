namespace Chirp.Infrastructure;

/// <summary>
/// This class represents the messages that a Chirp user can post, also known as a 'Cheep'.
/// A cheep is defined by its Author, a Message and a Timestamp that represents the date and time
/// of when the Cheep has been posted. The cheep also has its constraints which determines its maximum length of the cheep Message.
/// </summary>
/// <remarks>
/// Note: Timestamps are supported in SQLServer but converted to strings in SQLite
/// </remarks>


public class Cheep
{

    public int CheepId { get; set; }

    public string? AuthorId { get; set; }

    public required Author Author { get; set; }


    [StringLength(160, MinimumLength = 5)] 

    public required string Message { get; set; } 

    public DateTime TimeStamp { get; set; }
}
