namespace Chirp.Core;
public record CheepDTO(string Author, string Message, DateTime TimeStamp);

public interface ICheepRepository
{
    async Task<IEnumerable<CheepDTO>> GetCheeps(int pageSize, int page);
    async Task<IEnumerable<CheepDTO>> GetAuthor(int pageSize, int page);
    async Task<IEnumerable<CheepDTO>> GetByAuthor(string author);    

   async void CreateCheep(CheepDTO cheep);

    
    //Task Create(CheepDTO cheep);

}