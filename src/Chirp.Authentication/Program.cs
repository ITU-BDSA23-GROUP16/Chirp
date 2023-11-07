using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Chirp.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chirp.Infrastructure;
using Chirp.Core;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
        
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var DbPath = System.IO.Path.Join(path, "chirp.db");
Console.WriteLine($"Database path: {DbPath}.");

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddControllers();
builder.Services.AddDbContext<ChirpDBContext>(options =>
    options.UseSqlite(connectionString));
    //sqlserver?
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<Author, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ChirpDBContext>()
                .AddDefaultTokenProviders().AddDefaultUI();

        //builder.Services.AddDefaultIdentity<Author>(options => options.SignIn.RequireConfirmedAccount = true)
        //            .AddEntityFrameworkStores<ChirpDBContext>();
builder.Services.AddMvc();


builder.Services.AddRazorPages();
//builder.Services.AddSingleton<ICheepService, CheepService>();
//Read about GetConnectionString
//builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite($"Data Source={DbPath}"));
builder.Services.AddScoped<ICheepRepository, CheepRepository>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

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
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
//public partial class Program { }