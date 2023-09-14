static class UserInterface {

public static void PrintCheeps(IEnumerable<Cheep> cheeps){
     foreach (Cheep cheep in cheeps){
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp);
        string date = dateTimeOffset.ToString("MM/dd/yy HH:mm:ss");
        Console.WriteLine($"{cheep.Author} @ {date}: {cheep.Message}");
        }
    }
   
}