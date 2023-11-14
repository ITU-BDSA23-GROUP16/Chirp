using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;


public class AuthorRepository : IAuthorRepository
{

    private readonly ChirpDBContext _context;

    public AuthorRepository(ChirpDBContext context)
    {
        _context = context;

    }

    public async Task CreateAuthor(AuthorDTO author)
    {

        var newAuthor = new Author
        {
            UserName = author.Name, //Name of AuthorDTO
            Email = author.Email //Email of AuthorDTO
        };
        var existing = await _context.Authors.Where(c => c.UserName == author.Name).FirstOrDefaultAsync();
        if(existing!=null){
        throw new ArgumentException("Author already exists in database!", nameof(author));
        }
        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();
    }

   /* private static ICollection<CheepDTO> ConvertCheeps(ICollection<Cheep> cheeps) {
        var dtoColl = new List<CheepDTO>();
        foreach (Cheep c in cheeps) {
            var dtoConv = new CheepDTO(c.Author.Name,c.Message,c.TimeStamp);
            dtoColl.Add(dtoConv);
        }
        return dtoColl;
    }*/
    

    public async Task<AuthorDTO> FindAuthorByName(string author){
    
      return await _context.Authors
      .Where(a => a.UserName.Contains(author))
      .OrderByDescending(a => a.UserName)
      .Select(a => new AuthorDTO(a!.UserName, a.Email, a.Cheeps.Select(c => new CheepDTO(c.Author.UserName,c.Text,c.TimeStamp)))).SingleOrDefaultAsync();;
    } 

    
    public async Task<AuthorDTO> FindAuthorByEmail(string email){
    return await _context.Authors 
      .Where(a=>a.Email!= null && a.Email == email)
      .OrderByDescending(a => a.Email)
      .Select(a => new AuthorDTO(a!.UserName, a.Email, a.Cheeps.Select(c => new CheepDTO(c.Author.UserName,c.Text,c.TimeStamp)))).SingleOrDefaultAsync();;
        

    }

    
}