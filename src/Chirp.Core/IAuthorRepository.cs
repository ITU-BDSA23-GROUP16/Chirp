namespace Chirp.Core;
public record AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
//Skal en authorDTO's liste være en ICollection eller IEnumerable, Og hvis det skal være Icollection, skal alt returneres som Icollection i CheepRepository?
//Gør det en forskel for vores one to many relationships?
public interface IAuthorRepository
{
// Task Create(AuthorDTO author);

async void CreateAuthor(AuthorDTO author);

async Task<AuthorDTO> FindAuthorByName(string name);


async Task<AuthorDTO> FindAuthorByEmail(string email);




}