using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chirp.Infrastructure;
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        // Add EF services to the services container.
        services.AddDbContext<ChirpDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<Author, IdentityRole>().AddEntityFrameworkStores<ChirpDbContext>()
                .AddDefaultTokenProviders();


        //services.AddDefaultIdentity<Author>()
        //            .AddEntityFrameworkStores<ChirpDbContext>().AddDefaultTokenProviders().AddDefaultUI();
        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the app's middleware
    }
}