
public record RetrievedCheep(Author Author, string Message, DateTime time);


public interface ICheepRepository{
IEnumerable<RetrievedCheep>GetCheeps();




}