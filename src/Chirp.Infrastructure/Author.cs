namespace Chirp.Infrastructure;
public class Author
{

    public int AuthorId { get; set; }

    public required string Name { get; set; }

    //[EmailAdress] // data annation - attribute - it is adding something on top of the code

    public required string Email { get; set; }
    //Name is used for searching and queries, Email is used to log in.
    //Default values for get-set properties:
    //https://www.tutorialsteacher.com/articles/set-default-value-to-property-in-csharp
    public ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();



}