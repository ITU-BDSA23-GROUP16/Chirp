namespace Chirp.Core;
public record CheepDTO(string Author, string Message, DateTime TimeStamp);

/// <summary>
/// ICheepRepository is the interface defining the methods for retrieving data, using data transfer objects. 
/// This interface defines all the relevant methods of a CheepDTO and its properties.
/// The interface defines methods that can retrieve our cheeps as well as our authors and also the Follower.
/// </summary>



public interface ICheepRepository
{
    Task<IEnumerable<CheepDTO>> GetCheeps(int pageSize, int page);
    Task<IEnumerable<CheepDTO>> GetAllCheeps();
    Task<IEnumerable<CheepDTO>> GetByAuthor(string author, int pageSize, int page);
    Task<IEnumerable<CheepDTO>> GetAllByAuthor(string author);
    Task<IEnumerable<CheepDTO>> GetByFollower(string follower, int pageSize, int page);
    Task<IEnumerable<CheepDTO>> GetAllByFollower(string follower);
    Task CreateCheep(CheepDTO cheep);


}