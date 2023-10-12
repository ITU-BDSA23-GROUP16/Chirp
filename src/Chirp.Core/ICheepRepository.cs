
public record CheepDTO(Author Author, string Message, DateTime time);
public record AuthorDTO(string Name, string Email, ICollection<Cheep> cheeps);

public interface ICheepRepository{
IEnumerable<CheepDTO>GetCheeps();


IEnumerable<AuthorDTO> getAuthors();

/*
Author getAuthorById();



string getAuthorMail();


CheepDTO getCheepById();

string getCheepMessage();

DateTime getCheepTime();
*/


}