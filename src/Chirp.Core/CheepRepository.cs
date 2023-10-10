// another class

public class CheepRepository : ICheepRepository{

private readonly ChirpContext _context;


public CheepRepository(ChirpContext context)
    {
    _context = context;
     }


  public IEnumerable<RetrievedCheep>GetCheeps()
     {
    return  _context.Cheeps.Select(c => new RetrievedCheep(c.Author,c.Text, c.Time));
       
     }




}