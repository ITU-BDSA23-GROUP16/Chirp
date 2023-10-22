namespace Chirp.Core;
public record AuthorDTO(string Name, string Email, ICollection<Cheep> cheeps);
public interface IAuthorRepository
{
    Task Create(AuthorDTo author);
}