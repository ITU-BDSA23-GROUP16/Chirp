
public interface ICheepRepository {

IEnumerable<RetrievedCheep> getCheeps();

/*
IEnumerable<RetrievedCheep> getbyID();


RetrievedCheep getText(string text);

RetrievedCheep getTimeStamp(DateTime time);

RetrievedCheep getAuthor(Author author);

} */


}
public record RetrievedCheep(Author Author, string Message, DateTime Time);

