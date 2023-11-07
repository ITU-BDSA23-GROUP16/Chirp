namespace Chirp.Core;
public record AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
//Skal en authorDTO's liste være en ICollection eller IEnumerable, Og hvis det skal være Icollection, skal alt returneres som Icollection i CheepRepository?
//Gør det en forskel for vores one to many relationships?
public interface IAuthorRepository
{
// Task Create(AuthorDTO author);

public void CreateAuthor(AuthorDTO author);

public AuthorDTO FindAuthorByName(string name);


public AuthorDTO FindAuthorByEmail(string email);




}