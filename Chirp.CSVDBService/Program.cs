var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cheeps", () => new Cheep("me", "Hej!", 1684229348));
//Lambda function/anonymous function
//Expression lambda, => expression
app.Run();
public record Cheep(string Author, string Message, long Timestamp);