using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

//use the following commands to update your database:
/*dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update */




public class ChirpContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cheep> Cheeps { get; set; }

     

    public string DbPath { get; }

    public ChirpContext()
    {
        
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
         DbPath = System.IO.Path.Join(path, "chirp.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    //create the migration that construct the sqlite by using the UseSqlite, which means that it understands Sqlite

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}



//https://learn.microsoft.com/en-us/ef/core/modeling/keyless-entity-types?tabs=data-annotations
// A problem is that Cheep requires a primary key in the old code Post() had a PostId
public class Cheep
{
    public int CheepId { get; set; } 
    public string Text { get; set; }

    public DateTime Time { get; set; }

    public Author? Author {get; set;} 



}

//https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
//The link above is a Microsoft documentation about one-to-many relationships in EF Core that has been used in line 45

//https://learn.microsoft.com/en-us/ef/core/modeling/keyless-entity-types?tabs=data-annotations
// A problem is that Author requires a primary key in the old code Blog() had a BlogID

public class Author
{
    public int AuthorId { get; set; } 
    public string Email { get; set; }

    public string Name { get; set; }

     public ICollection<Cheep> cheep {get; set;} = new List<Cheep>();
 

}