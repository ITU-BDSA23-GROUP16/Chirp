using System;
using System.Linq;

//This line creates a new instance of the ChirpContext, which is a database context class for Entity Framework Core. 
//It establishes a connection to the database specified in your context's configuration
using var db = new ChirpDBContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new author");
//Creates a new author object with an Email and a Name
var author = new Author { Email = "Say123@example.com", Name = "Saynab Liibaan" };
//Adds to the Authors DbSet (database table) within the ChirpContext.
db.Add(author);
db.SaveChanges();

// Read
Console.WriteLine("Querying for an author");
//Creating a new author by retreiving an author from the database by using db.Authors
//it sorts all emails in ascending order: https://www.tutorialsteacher.com/linq/linq-sorting-operators-orderby-orderbydescending#google_vignette
var retrievedAuthor = db.Authors
    .OrderBy(a => a.Email)
    .First();


// Update
Console.WriteLine("Updating the author and adding a cheep");
//This part updates the Name value of the retrievedAuthor from "Saynab Liibaan" to "Saynab"
retrievedAuthor.Name = "Saynab";
//This code of line adds a new cheep assosicated with the author by using the cheep collection
retrievedAuthor.cheep.Add(new Cheep { Text = "Whats uuuuuuup!", Time = DateTime.Now });
db.SaveChanges();

// Delete
Console.WriteLine("Delete the author");
//And u can delete an author from the database by using the Remove()
db.Remove(retrievedAuthor);
db.SaveChanges();



/*
using System.Linq;

using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
db.SaveChanges();

// Read
Console.WriteLine("Querying for a blog");
var blog = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
db.SaveChanges();

// Delete
Console.WriteLine("Delete the blog");
db.Remove(blog);
db.SaveChanges();



*/