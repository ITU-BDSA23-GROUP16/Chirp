using Microsoft.EntityFrameworkCore;
namespace Chirp.Infrastructure;
public class CheepRepository : ICheepRepository
{

   private readonly ChirpDBContext _context;

   public CheepRepository(ChirpDBContext context)
   {
      _context = context;
   }

   //ChirpDBContext and repos
   public async Task<IEnumerable<CheepDTO>> GetCheeps(int pageSize = 32, int page = 0)
   {
      return await _context.Cheeps
      .OrderByDescending(c => c.TimeStamp)
      .Skip(page * pageSize)
      .Take(pageSize)
      .Select(c => new CheepDTO(c.Author!.UserName, c.Text!, c.TimeStamp))
      .ToListAsync();
   }

   public async Task<IEnumerable<CheepDTO>> GetAuthor(int pageSize = 32, int page = 0)
   {
      return await _context.Cheeps
      .OrderByDescending(c => c.Author)
      .Skip(page * pageSize)
      .Take(pageSize)
      .Select(c => new CheepDTO(c.Author!.UserName, c.Text!, c.TimeStamp))
      .ToListAsync();
   }

   public async Task<IEnumerable<CheepDTO>> GetByAuthor(string author)
   {
      return await _context.Cheeps
      .Where(a => a.Author.UserName.Contains(author))
      .OrderByDescending(a => a.Author.UserName)
      .Select(a => new CheepDTO(a.Author!.UserName, a.Text!, a.TimeStamp))
      .ToListAsync();
   }


   public async Task CreateCheep(CheepDTO cheep)
   {
      //var newauthor = rep.FindAuthorByName(cheep.Author);
      
      //Find a Author type in the context(database) by using Find(string)
      var aut = _context.Authors.Find(cheep.Author);


      
      var newCheep = new Cheep
      {
         Author = aut!,
         Text = cheep.Message 
      };

         _context.Cheeps.Add(newCheep);
         _context.SaveChanges();
   }
}