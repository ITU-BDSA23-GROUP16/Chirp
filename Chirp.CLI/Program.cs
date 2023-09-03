using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    public static void Main(string[] args)
    {
        string file = "chirp_cli_db.csv";
        try
        {
            // Open the text file using a stream reader.
            using (var sr = new StreamReader(file))
            {
                string line;
                string author;
                string message;
                string command = args[0];

                if(command == "read")
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                    //Define pattern
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    //Separating columns to array
                    string[] X = CSVParser.Split(line);

                    if(X.Length >= 3) 
                    {
                        author = X[0];
                        message = X[1];
                        string unixTimeStampString = X[2];
                        if(long.TryParse(unixTimeStampString, out long unixTimeStamp))
                        {
                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
                            string date = dateTimeOffset.ToString("MM/dd/yy HH:mm:ss");
                            Console.WriteLine($"{author} @ {date}: {message}");
                        }
                        
                    } 
                    }
                } else if(command == "cheep")
                {
                    if(args.Length >= 2)
                    {
                    author = Environment.UserName;
                    message = args[1];
                    long unixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    string date = DateTimeOffset.UtcNow.ToString("MM/dd/yy HH:mm:ss");
                    string newLine = $"{author},\"{message}\",{unixTimeStamp}";
                    using (StreamWriter sw = File.AppendText(file))
                    {
                        // Append the new line to the file
                        sw.WriteLine(newLine);
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