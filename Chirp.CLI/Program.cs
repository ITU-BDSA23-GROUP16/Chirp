//Parts of the code have been taken from the following links:
//Read text from a file https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-text-from-a-file
//Append text(string) https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendtext?view=net-7.0
//Unix timestamp to a typical timestamp https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tounixtimeseconds?view=net-7.0 
//and https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.utcnow?view=net-7.0
//Regular expressions https://stackoverflow.com/questions/3507498/reading-csv-files-using-c-sharp/34265869#34265869

using System;
using System.IO;
using System.Text.RegularExpressions;
using SimpleDB;

class Program {
    public static void Main(string[] args) {
        string file = "chirp_cli_db.csv";
        
        try {
            //First command line argument "read" or cheep"
            string command = args[0];
            
            //
            IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>(file);

            //Displays cheeps
            if(command == "read") {
                
                //Returns IEnumerable<T>
                var cheeps = database.Read(10); 
                

                foreach(Cheep cheep in cheeps){
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp);
                    string date = dateTimeOffset.ToString("MM/dd/yy HH:mm:ss");
                    Console.WriteLine($"{cheep.Author} @ {date}: {cheep.Message}");
                }
            //adds cheep
            } else if(command == "cheep") {

                
                //checks if there are at least two command line arguments besides cheep itself
                //if there are at least 2 arguments
                //sets author to logged in user's name
                //create new message with author, message and unix timestamp
                if(args.Length >= 2)
                {

                long date = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                
                
                var author    = Environment.UserName;
                var message   = args[1];
                
                Cheep cheep = new Cheep(author,message,date);
                database.Store(cheep);
                
              
                }
            }
            // Read the stream as a string, and write the string to the console.
            //i.e. prints contents of csv file to console
           // Console.WriteLine(sr.ReadToEnd());
        } catch (IOException e) {
            
        Console.WriteLine("The file could not be read:");
        Console.WriteLine(e.Message);
        
        } catch (IndexOutOfRangeException e) {
            Console.WriteLine("Parameters missing. Try: \"read\" or \"cheep \"<your message>\"\" ");
            Console.WriteLine(e.Message);
        }
    }
    
}