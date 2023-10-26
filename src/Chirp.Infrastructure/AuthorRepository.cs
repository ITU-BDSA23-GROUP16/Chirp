namespace Chirp.Infrastructure;


public class AuthorRepository : IAuthorRepository
{

    private readonly ChirpDBContext _context;

    public AuthorRepository(ChirpDBContext context)
    {
        _context = context;

    }

    public void CreateAuthor(AuthorDTO author)
    {

        var newAuthor = new Author
        {
            name = newAuthor.Name,
            email = newAuthor.Email
        };

        _context.Authors.Add(newAuthor);
        _context.SaveChanges();
    }


    /*AuthorDTO FindAuthorByName(); 
    AuthorDTO FindAuthorByEmail();

    */
}