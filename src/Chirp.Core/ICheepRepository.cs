namespace Chirp.Core;
public record CheepDTO(Author Author, string Message, DateTime time);

public interface ICheepRepository
{
    Task<IEnumerable<CheepListDto>> GetCheeps {int pageSize = 32, int page = 0};
    Task<IEnumerable<CheepListDto>> GetAuthor { String Author, int pageSize = 32, int page = 0};
    Task Create(CheepDTO cheep);

}