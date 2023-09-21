//Parts of the code have been taken from the following links:
//Read text from a file https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-text-from-a-file
//Append text(string) https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendtext?view=net-7.0
//Unix timestamp to a typical timestamp https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tounixtimeseconds?view=net-7.0 
//and https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.utcnow?view=net-7.0


using System;
using System.IO;
using System.Text.RegularExpressions;
using SimpleDB;
using DocoptNet;
using UI;

string file = "chirp_cli_db.csv";
    

IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>(file);

const string usage = @"Chirp CLI version.

Usage:
    chirp read <limit> | read
    chirp cheep <message>
    chirp --help
    chirp --version

Options:
    --help        Show this screen.
    --version     Show version.
";

var arguments = new Docopt().Apply(usage, args, version: "1.0", exit: true)!;

/*
    Uses arguments variable to check for usage command
    if arguments contains "read", run code for printing cheeps,
    else if arguments contains "cheep", append to the database. The <message> provided in the arguments is converted to a string, to create a Cheep. 
*/
if(arguments["read"].IsTrue){
    //Returns IEnumerable<T>
    var cheeps = database.Read(10); 
    UserInterface.PrintCheeps(cheeps);

    return 0;

} else if (arguments["cheep"].IsTrue){
    long date = DateTimeOffset.UtcNow.ToUnixTimeSeconds();   
    var author    = Environment.UserName;
    var message   = arguments["<message>"].ToString(); 
    
    Cheep cheep = new Cheep(author,message,date);
    database.Store(cheep);
    
    return 0;
} 

return 1;