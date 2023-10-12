// another class

public class CheepRepository : ICheepRepository{

private readonly ChirpDBContext _context;


public CheepRepository(ChirpDBContext context)
   {
   _context = context;
   }


  public IEnumerable<CheepDTO>GetCheeps()
   {
   return  _context.Cheeps.Select(c => new CheepDTO(c.Author,c.Text, c.Time));
       
   }


   public IEnumerable<AuthorDTO> getAuthors() 
   {
   return _context.Authors.Select(a => new AuthorDTO(a.Name, a.Email, a.cheep));

   }


}