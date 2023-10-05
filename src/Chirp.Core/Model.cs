using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class BloggingContext : DbContext
{
    public DbSet<Author> author { get; set; }
    public DbSet<Cheep> cheep { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}


public class Cheep
{
    public string Text { get; set; }

    public DateTime Time { get; set; }

    public Author? Author {get; set}



}

//https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
//The link above is a Microsoft documentation about one-to-many relationships in EF Core that has been used in line 45
public class Author
{
    public string Email { get; set; }

    public string Name { get; set; }

     public ICollection<Cheep> cheep {get; set} = new List<Cheep>()
 

}