namespace Chirp.Infrastructure;
public class Author
{

    public int AuthorId { get; set; }

    public string Name { get; set; }

    //[EmailAdress] // data annation - attribute - it is adding something on top of the code

    public string Email { get; set; }


    public ICollection<Cheep> Cheeps { get; set; }



}