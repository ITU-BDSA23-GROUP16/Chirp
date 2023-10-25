namespace Chirp.Infrastructure;
public class CheepRepository : ICheepRepository
{

   private readonly ChirpDBContext _context;

   public CheepRepository(ChirpDBContext context)
   {
      _context = context;
   }

   //ChirpDBContext and repos
   public IEnumerable<CheepDTO> GetCheeps(int pageSize = 32, int page = 0)
   {
      return _context.Cheeps
       .OrderByDescending(c => c.TimeStamp)
       .Skip(page * pageSize)
       .Take(pageSize)
       .Select(c => new CheepDTO(c.Text!, c.Author!.Name, c.TimeStamp));

   }

   public IEnumerable<CheepDTO> GetAuthor(int pageSize = 32, int page = 0)
   {
      return _context.Cheeps
      .OrderByDescending(c => c.Author)
      .Skip(page * pageSize)
      .Take(pageSize)
      .Select(c => new CheepDTO(c.Text!, c.Author!.Name, c.TimeStamp));
   }
}