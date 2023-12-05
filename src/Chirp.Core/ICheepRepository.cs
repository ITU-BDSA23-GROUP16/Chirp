namespace Chirp.Core;
public record CheepDTO(string Author, string Message, DateTime TimeStamp);

public interface ICheepRepository
{
    Task<IEnumerable<CheepDTO>> GetCheeps(int pageSize, int page);
    Task<IEnumerable<CheepDTO>> GetCheeps();
    Task<IEnumerable<CheepDTO>> GetAuthor(int pageSize, int page);
    Task<IEnumerable<CheepDTO>> GetByAuthor(string author, int pageSize, int page);
    Task<IEnumerable<CheepDTO>> GetByAuthor(string author);
    Task<IEnumerable<CheepDTO>> GetByFollower(string follower);
    Task CreateCheep(CheepDTO cheep);


    //Task Create(CheepDTO cheep);

}