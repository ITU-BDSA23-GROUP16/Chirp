using SimpleDB;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string file = "../Chirp.CLI/chirp_cli_db.csv";

IDatabaseRepository<Cheep> database = CSVDatabase<Cheep>.Instance(file);

app.MapGet("/cheeps", () => database.Read(10));

app.MapPost("/cheep", (Cheep cheep) => database.Store(cheep));

//curl -d '{"author":"christina", "message":"New line?!", "timestamp":"1695753649"}' -H "Content-Type: application/json" -X POST http://localhost:5080/cheep

app.Run();
public record Cheep(string Author, string Message, long Timestamp);