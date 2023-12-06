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

      var toReturn = await _context.Cheeps
      .OrderByDescending(c => c.TimeStamp)
      .Skip(page * pageSize)
      .Take(pageSize)
      .Select(c => new CheepDTO(c.Author!.UserName!, c.Message!, c.TimeStamp))
      .ToListAsync();
      if (toReturn==null) {
      return new List<CheepDTO>();
      }
      else return toReturn;
   }


   public async Task<IEnumerable<CheepDTO>> GetAllCheeps()
   {
      return await GetCheeps(int.MaxValue,0);
   }


   public async Task<IEnumerable<CheepDTO>> OrderByAuthor(int pageSize = 32, int page = 0)
   {
      
      var toReturn =  await _context.Cheeps
      .OrderByDescending(c => c.Author)
      .Skip(page * pageSize)
      .Take(pageSize)
      .Select(c => new CheepDTO(c.Author!.UserName!, c.Message!, c.TimeStamp))
      .ToListAsync();

      if (toReturn == null){
         return new List<CheepDTO>();
      } else {
         return toReturn;
      }

   }

   public async Task<IEnumerable<CheepDTO>> GetByAuthor(string author, int pageSize = 32, int page = 0)
   {
      var toReturn = await _context.Cheeps
      .Where(a => a.Author.UserName!.Contains(author))
      .OrderByDescending(a => a.Author.UserName)
      .Skip(page * pageSize)
      .Take(pageSize)
      .Select(a => new CheepDTO(a.Author!.UserName!, a.Message!, a.TimeStamp))
      .ToListAsync();

      if(toReturn== null){
         return new List<CheepDTO>();

      } else{
         return toReturn;

      }
   }

   public async Task<IEnumerable<CheepDTO>> GetAllByAuthor(string author)
   {
      return await GetByAuthor(author,int.MaxValue,0);
   }

   public async Task<IEnumerable<CheepDTO>> GetByFollower(string follower, int pageSize = 32, int page = 0)
   {
      IEnumerable<Author> allfollowed =
      await _context.Follows.Where(f => f.Follower.UserName!.Contains(follower))
      .Select(f => f.Following)
      .ToListAsync();


      IEnumerable<Cheep> cheeplist = new List<Cheep>();
      foreach (Author aut in allfollowed)
      {
         var autlist = await _context.Cheeps
         .Where(a => a.Author == aut)
         .ToListAsync();

         cheeplist = cheeplist.Concat(autlist);
      }

      var toReturn = cheeplist.OrderByDescending(c => c.TimeStamp)
      .Skip(page * pageSize)
      .Take(pageSize)
      .Select(a => new CheepDTO(a.Author!.UserName!, a.Message!, a.TimeStamp));
      //check null
      if (toReturn==null) {
      return new List<CheepDTO>();
      }
      else return toReturn;
   }
public async Task<IEnumerable<CheepDTO>> GetAllByFollower(string follower)
   {
      return await GetByFollower(follower,int.MaxValue,0);
   }
   public async Task CreateCheep(CheepDTO cheep)
   {

      var aut = await _context.Authors.SingleAsync(c => c.UserName == cheep.Author);

      var newCheep = new Cheep
      {
         Author = aut!,
         Message = cheep.Message,
         TimeStamp = cheep.TimeStamp
      };

      _context.Cheeps.Add(newCheep);
      await _context.SaveChangesAsync();
   }

}
