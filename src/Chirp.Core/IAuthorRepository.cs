namespace Chirp.Core;
public record AuthorDTO(string Name, string Email, ICollection<CheepDTO> cheeps);
public interface IAuthorRepository
{
// Task Create(AuthorDTO author);

void CreateAuthor(AuthorDTO author);

/*AuthorDTO FindAuthorByName();
AuthorDTO FindAuthorByEmail();
*/



}