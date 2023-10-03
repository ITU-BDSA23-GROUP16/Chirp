using DB;
public record CheepViewModel(string Author, string Message, string Timestamp);


public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
}

namespace Cheep
{
    public class CheepService : ICheepService
    {
        DBFacade database = new DBFacade();

        // These would normally be loaded from a database for example
        //Need author var, message var, time


        private readonly List<CheepViewModel> _cheeps;

        public CheepService()
        {
            _cheeps = parseCheep();
        }

        private List<CheepViewModel> parseCheep()
        {

            var retrievedList = database.ReturnRow();
            Console.WriteLine(retrievedList.Count);
            var returnList = new List<CheepViewModel>();
            string[] variables;
            foreach (String str in retrievedList)
            {
                variables = str.Split(",");
                returnList.Add(new CheepViewModel(variables[0], variables[1], variables[2]));

            }

            return returnList;
        }


        public List<CheepViewModel> GetCheeps()
        {
            return _cheeps;
        }


        public List<CheepViewModel> GetCheepsFromAuthor(string author)
        {
            // filter by the provided author name
            return _cheeps.Where(x => x.Author == author).ToList();
        }

        private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);
            return dateTime.ToString("MM/dd/yy H:mm:ss");
        }
    }


}
