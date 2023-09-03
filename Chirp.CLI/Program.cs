using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    public static void Main()
    {
        try
        {
            // Open the text file using a stream reader.
            using (var sr = new StreamReader("chirp_cli_db.csv"))
            {
                string line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    //Define pattern
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    //Separating columns to array
                    string[] X = CSVParser.Split(line);

                    if(X.Length >= 3) 
                    {
                        string author = X[0];
                        string message = X[1];
                        string unixTimeStampString = X[2];
                        if(long.TryParse(unixTimeStampString, out long unixTimeStamp))
                        {
                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
                            string date = dateTimeOffset.ToString("MM/dd/yy HH:mm:ss");
                            Console.WriteLine($"{author} @ {date}: {message}");
                        }
                        
                    } 
                }
                // Read the stream as a string, and write the string to the console.
                Console.WriteLine(sr.ReadToEnd());
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        
    }
}