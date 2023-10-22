namespace Chirp.Infrastructure;
public class Cheep
{
    public int CheepId { get; set; }
    public string Text { get; set; }

    public DateTime Time { get; set; }

    public Author? Author { get; set; }

}
