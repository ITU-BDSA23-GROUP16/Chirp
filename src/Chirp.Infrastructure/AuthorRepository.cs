namespace Chirp.Infrastructure;


public class AuthorRepository : IAuthorRepository
{

    private readonly ChirpDBContext _context;

    public AuthorRepository(ChirpDBContext context)
    {
        _context = context;

    }

    public async void CreateAuthor(AuthorDTO author)
    {

        var newAuthor = new Author
        {
            Name = author.Name, //Name of AuthorDTO
            Email = author.Email //Email of AuthorDTO
        };
        var existing = await _context.Authors.SingleOrDefaultAsync(c => c.Name == author.Name);
        if(existing!=null){
        throw new ArgumentsException("Author already exists in database!", nameof(author));
        }
        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();
    }

   /* private static ICollection<CheepDTO> ConvertCheeps(ICollection<Cheep> cheeps) {
        var dtoColl = new List<CheepDTO>();
        foreach (Cheep c in cheeps) {
            var dtoConv = new CheepDTO(c.Author.Name,c.Text,c.TimeStamp);
            dtoColl.Add(dtoConv);
        }
        return dtoColl;
    }*/
    

    public async Task<AuthorDTO> FindAuthorByName(string author){
    
      return await (AuthorDTO) _context.Authors
      .Where(a => a.Name.Contains(author))
      .OrderByDescending(a => a.Name)
      .Select(a => new AuthorDTO(a!.Name, a.Email, a.Cheeps.Select(c => new CheepDTO(c.Author.Name,c.Text,c.TimeStamp))));
      
    
    } 

    
    public async Task<AuthorDTO> FindAuthorByEmail(string email){
    return await (AuthorDTO)_context.Authors 
      .Where(a => a.Name.Contains(email))
      .OrderByDescending(a => a.Email)
      .Select(a => new AuthorDTO(a!.Name, a.Email, a.Cheeps.Select(c => new CheepDTO(c.Author.Name,c.Text,c.TimeStamp))));
        

    }

    
}