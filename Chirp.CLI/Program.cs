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
                //Defines variables for later use
                string line;
                string author;
                string message;
                //First command line argument "read" or cheep"
                string command = args[0];
                
                //Displays cheeps
                if(command == "read")
                {
                    //loops through each line of csv file
                    while ((line = sr.ReadLine()) != null)
                    {
                    //Define pattern
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    //Separating columns to array
                    string[] X = CSVParser.Split(line);

                    //check if there are three columns author, message, timestamp in each line
                    //converts unixtime to readable date and prints formatted line to console
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
                //adds cheep
                } else if(command == "cheep")
                {
                    //checks if there are at least two command line arguments besides cheep itself
                    //if there are at least 2 arguments
                    //sets author to logged in user's name
                    //create new message with author, message and unix timestamp
                    if(args.Length >= 2)
                    {
                    author = Environment.UserName;
                    message = args[1];
                    long unixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    string date = DateTimeOffset.UtcNow.ToString("MM/dd/yy HH:mm:ss");
                    string newLine = $"{author},\"{message}\",{unixTimeStamp}";
                    //appends new line to csv file using StreamWriter
                    using (StreamWriter sw = File.AppendText(file))
                    {
                        sw.WriteLine(newLine);
                    }
                    }
                }
                // Read the stream as a string, and write the string to the console.
                //i.e. prints contents of csv file to console
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