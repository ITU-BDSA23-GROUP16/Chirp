
public class CheepRepository : ICheepRepository {

private readonly ChirpContext _context;


public CheepRepository(ChirpContext context){

_context = context;

}


public IEnumerable<RetrievedCheep> getCheeps() {

return _context.Cheeps.Select(c => new RetrievedCheep(c.Author, c.Text, c.Time));
}


/*
public IEnumerable<RetrievedCheep> getbyID() {

return _context.Cheeps.Select(c => c.CheepId);
} 


public Cheep getText(string text) {

return _context.Cheeps.Select(c => new RetrievedCheep(c.Text));
}


public  getTimeStamp(DateTime time) {

return _context.Cheeps.Select(c => new RetreivedCheep(c.Time));
}


public Cheep getAuthor(Author author) {

return _context.Cheeps.Select(c => new RetrievedCheep(c.Author));

} 

*/


}