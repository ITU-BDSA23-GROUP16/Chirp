using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Chirp.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chirp.Infrastructure;
using Chirp.Core;
using AspNet.Security.OAuth.GitHub;

/// <summary>
/// The main process of Chirp!
/// Along with the general setup, this defines what kind of Authorizations are available
/// </summary>

var builder = WebApplication.CreateBuilder(args);

//__________________________
var path = Path.GetTempPath();
var DbPath = System.IO.Path.Join(path, "chirp.db");


builder.Services.AddControllers();
//DBContext definition for SQLServer
builder.Services.AddDbContext<ChirpDBContext>(options =>
options.UseSqlServer("Server=tcp:bdsagroup16-chirpdb.database.windows.net,1433;Initial Catalog=bdsagroup16-chirpdb;Persist Security Info=False;User ID=bdsagroup16adminlogin;Password=Bdsagroup16adminpassword!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

//DBContext definition for SQLite
/*builder.Services.AddDbContext<ChirpDBContext>(options =>
    options.UseSqlite($"Data Source={DbPath}"));*/
//_________________________

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<Author, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ChirpDBContext>()
                .AddDefaultTokenProviders().AddDefaultUI();

builder.Services.AddMvc();

//Github authentication only works on the web app;
//It must be removed in the local, SQLite release
builder.Services.AddAuthentication()
    .AddGitHub(o =>
    {
#pragma warning disable CS8601 // Possible null reference assignment.

        o.ClientId = builder.Configuration["authentication_github_clientId"];
        o.ClientSecret = builder.Configuration["authentication_github_clientSecret"];
#pragma warning restore CS8601 // Possible null reference assignment.

        o.CallbackPath = "/signin-github";
    });


builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

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
        // The default HSTS value is 30 days.
        app.UseHsts();
    }


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

