using DB;
using System;
using System.IO;
using System.Text.RegularExpressions;
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
            _cheeps = ReturnedCheeps();
        }


        public List<CheepViewModel> ReturnedCheeps()
        {
            var newCheepList = new List<CheepViewModel>();
            var retrievedList = database.CheepsFromDB<CheepViewModel>();
            string newTime = null;

            for (int i = 0; i < retrievedList.Count; i++)
            {
                newTime = UnixTimeStampToDateTimeString2String(retrievedList[i].Timestamp);
                newCheepList.Add(new CheepViewModel(retrievedList[i].Author, retrievedList[i].Message, newTime));
            }

            return newCheepList;

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

        private static string UnixTimeStampToDateTimeString2String(string unixTimeStamp)
        {
            var string2double = Convert.ToDouble(unixTimeStamp);
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(string2double);
            return dateTime.ToString("MM/dd/yy H:mm:ss");
        }
    }


}
