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
       .Select(c => new CheepDTO(c.Author!.Name, c.Text!, c.TimeStamp));

   }

   public IEnumerable<CheepDTO> GetAuthor(int pageSize = 32, int page = 0)
   {
      return _context.Cheeps
      .OrderByDescending(c => c.Author)
      .Skip(page * pageSize)
      .Take(pageSize)
      .Select(c => new CheepDTO(c.Author!.Name, c.Text!, c.TimeStamp));
   }

   public IEnumerable<CheepDTO> GetByAuthor(string author)
   {
      return _context.Cheeps
      .Where(a => a.Author.Name.Contains(author))
      .OrderByDescending(a => a.Author.Name)
      .Select(a => new CheepDTO(a.Author!.Name, a.Text!, a.TimeStamp));




   }


   public void CreateCheep(CheepDTO cheep, AuthorRepository rep)
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