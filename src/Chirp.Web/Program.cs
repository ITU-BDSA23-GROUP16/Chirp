using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
        
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var DbPath = System.IO.Path.Join(path, "chirp.db");
Console.WriteLine($"Database path: {DbPath}.");

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddSingleton<ICheepService, CheepService>();
//Read about GetConnectionString
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite($"Data Source={DbPath}"));
builder.Services.AddScoped<ICheepRepository, CheepRepository>();


var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ChirpDBContext>();
    context.Database.Migrate();
    //Then you can use the context to seed the database for example
    DbInitializer.SeedDatabase(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
public partial class Program { }