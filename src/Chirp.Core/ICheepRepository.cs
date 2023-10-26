namespace Chirp.Core;
public record CheepDTO(string Author, string Message, DateTime TimeStamp);

public interface ICheepRepository
{
    IEnumerable<CheepDTO> GetCheeps(int pageSize, int page);
    IEnumerable<CheepDTO> GetAuthor(int pageSize, int page);
    IEnumerable<CheepDTO> GetByAuthor(string author);    

    
    //Task Create(CheepDTO cheep);

}