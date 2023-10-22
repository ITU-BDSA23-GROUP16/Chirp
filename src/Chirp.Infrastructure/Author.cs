namespace Chirp.Infrastructure;
public class Author
{
    public int AuthorId { get; set; }
    public string Email { get; set; }

    public string Name { get; set; }

    public ICollection<Cheep> cheep { get; set; } = new List<Cheep>();


}