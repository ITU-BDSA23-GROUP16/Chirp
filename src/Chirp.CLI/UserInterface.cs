namespace UI;

public static class UserInterface {

    public static string ConvertTimestampToDate(long timestamp){
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        string date = dateTimeOffset.ToString("MM/dd/yy HH:mm:ss");
        return date;
    }

    public static string GetCheepFormattedMessage(Cheep cheep){
        string date = ConvertTimestampToDate(cheep.Timestamp);
        return $"{cheep.Author} @ {date}: {cheep.Message}";
    }

    public static void PrintCheeps(IEnumerable<Cheep> cheeps){
        foreach (Cheep cheep in cheeps){
            Console.WriteLine(GetCheepFormattedMessage(cheep));
        }
    }
}
