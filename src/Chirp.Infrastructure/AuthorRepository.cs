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
            Name = author.Name, //Name of AuthorDTO
            Email = author.Email //Email of AuthorDTO
        };

        _context.Authors.Add(newAuthor);
        _context.SaveChanges();
    }

   /* private static ICollection<CheepDTO> ConvertCheeps(ICollection<Cheep> cheeps) {
        var dtoColl = new List<CheepDTO>();
        foreach (Cheep c in cheeps) {
            var dtoConv = new CheepDTO(c.Author.Name,c.Text,c.TimeStamp);
            dtoColl.Add(dtoConv);
        }
        return dtoColl;
    }*/
    

    public AuthorDTO FindAuthorByName(string author){
    
      return (AuthorDTO) _context.Authors
      .Where(a => a.Name.Contains(author))
      .OrderByDescending(a => a.Name)
      .Select(a => new AuthorDTO(a!.Name, a.Email, a.Cheeps.Select(c => new CheepDTO(c.Author.Name,c.Text,c.TimeStamp))));
      
    
    } 

    
    public AuthorDTO FindAuthorByEmail(string email){
        return (AuthorDTO)_context.Authors 
      .Where(a => a.Name.Contains(email))
      .OrderByDescending(a => a.Email)
      .Select(a => new AuthorDTO(a!.Name, a.Email, a.Cheeps.Select(c => new CheepDTO(c.Author.Name,c.Text,c.TimeStamp))));
        

    }

    
}