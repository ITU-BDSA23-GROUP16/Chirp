namespace Chirp.Infrastructure;
public class CheepRepository : ICheepRepository
{

   private readonly ChirpContext _context;


   public CheepRepository(ChirpContext context)
   {
      _context = context;
   }

   public async IEnumerable<CheepListDto> GetCheeps(int pageSize = 32, int page = 0) =>
     {
       await _context.Cheeps
       .OrderByDesecnding(c => c.Time)
       .Skip(page* pageSize)
       .Take(pakesixe)
       .Select(c => new CheepListDto(c.Message, c.Author.Name, c.PubDate )
       .toListAsync()
}

public async IEnumerable<CheepListDto> GetCheepsAuthor(int pageSize = 32, int page = 0) =>
     {
   await _context.Cheeps
   .OrderByDesecnding(c => c.Author)
   .Skip(page* pageSize)
   .Take(pakesixe)
   .Select(c => new CheepListDto(c.Message, c.Author.Name, c.PubDate)
   .toListAsync()
}