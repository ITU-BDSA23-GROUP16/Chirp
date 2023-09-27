using SimpleDB;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Find file in a folder outside of current directory, inside Chirp.CLI
string file = "../Chirp.CLI/chirp_cli_db.csv";

IDatabaseRepository<Cheep> database = CSVDatabase<Cheep>.Instance(file);

//Endpoint cheeps to read stored cheaps, using an expression lambda (does not work with only one line in a statement lambda)
app.MapGet("/cheeps", () => database.Read(10));

/* Endpoint cheep to store new cheep provided in command call, using an expression lambda (does not work with only one line in a statement lambda)
curl -d '{"author":"christina", "message":"New line?!", "timestamp":"1695753649"}' -H "Content-Type: application/json" -X 
POST http://localhost:5080/cheep

Usage of curl command for post can be found on the following link:
https://gist.github.com/subfuzion/08c5d85437d5d4f00e58 */
app.MapPost("/cheep", (Cheep cheep) => database.Store(cheep));


app.Run();
public record Cheep(string Author, string Message, long Timestamp);