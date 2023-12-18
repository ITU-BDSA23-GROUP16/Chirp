using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
using System.ComponentModel;
namespace Chirp.Infrastructure.Tests;

//This class contains all the unit tests regarding the author properties
public class AuthorTest
{

    [Fact]
    public void Author_Name_2ActualNameTest()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Saynab";
        string message = "Merry Christmas!";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Saynab";

        CheepDTO cheep1 = new CheepDTO("Dima", "Vi ses", date);
        CheepDTO cheep2 = new CheepDTO("Mikkel", "Who?", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        string actual = author1.Name;

        // Assert
        Assert.Equal(expected, actual);
    }



     [Fact]
    public void Author_Name_2ActualNameTest2()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Jeppe";
        string message = "Godt nytår!";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Jeppe";

        CheepDTO cheep1 = new CheepDTO("Søren", "Jeg elsker dig Birgit", date);
        CheepDTO cheep2 = new CheepDTO("Chad", "Hvem sprugte dig?", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        string actual = author1.Name;

        // Assert
        Assert.Equal(expected, actual);
    }




 [Fact]
    public void Author_Name_2ActualNameTest3()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Thore";
        string message = "GG algorithms 4 life";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Thore";

        CheepDTO cheep1 = new CheepDTO("Vlad", "Heyo", date);
        CheepDTO cheep2 = new CheepDTO("Phoenix", "SKRRRR", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        string actual = author1.Name;

        // Assert
        Assert.Equal(expected, actual);
    }



    [Fact]
    public void Author_Name_2WrongNameTest()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Kirsten";
        string message = "Hej kan du ringe tilbage til mig";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Thore";

        CheepDTO cheep1 = new CheepDTO("Diego", "Lol hvad med dig", date);
        CheepDTO cheep2 = new CheepDTO("Courtney", "Im sooooo over this", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        string actual = author1.Name;

        // Assert
        Assert.NotEqual(expected, actual);
    }




[Fact]
    public void Author_Name_2WrongNameTest2()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Chrusty";
        string message = "Hej kan du ringe tilbage til mig";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Dusty";

        CheepDTO cheep1 = new CheepDTO("Puffy", "Hejhej", date);
        CheepDTO cheep2 = new CheepDTO("Jasper", "Like a bulldozer", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        string actual = author1.Name;

        // Assert
        Assert.NotEqual(expected, actual);
    }

[Fact]
    public void Cheep_List_2ActualLengthTest()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Saynab";
        string message = "Merry Christmas!";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        

        CheepDTO cheep1 = new CheepDTO("Dima", "Vi ses", date);
        CheepDTO cheep2 = new CheepDTO("Mikkel", "Who?", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);
         int expected = cheeps.Count();


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        int actual = author1.Cheeps.Count();;

        // Assert
        Assert.Equal(expected, actual);
    }



[Fact]
    public void Cheep_List_2ActualLengthTest2()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Dima";
        string message = "I'm radiant";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        

        CheepDTO cheep1 = new CheepDTO("Kasper", "Byebye", date);
        CheepDTO cheep2 = new CheepDTO("Jacques", "Salam", date);
        CheepDTO cheep3 = new CheepDTO("Stanley", "Aleikum", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);
         cheeps.Add(cheep3);
         int expected = cheeps.Count();


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        int actual = author1.Cheeps.Count();;

        // Assert
        Assert.Equal(expected, actual);
    }


[Fact]
    public void Cheep_List_2WrongLengthTest()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Christine";
        string message = "I'm iron";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        

        CheepDTO cheep1 = new CheepDTO("Kasper", "Byebye", date);
        CheepDTO cheep2 = new CheepDTO("Jacques", "Salam", date);
        CheepDTO cheep3 = new CheepDTO("Stanley", "Aleikum", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);
         cheeps.Add(cheep3);
         


        CheepDTO cheep4 = new CheepDTO("Jacques", "Salam", date);
        CheepDTO cheep5 = new CheepDTO("Stanley", "Aleikum", date);
        List<CheepDTO> cheeps2 = new List<CheepDTO>();
        int expected = cheeps2.Count();


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        int actual = author1.Cheeps.Count();;

        // Assert
        Assert.NotEqual(expected, actual);
    }


[Fact]
    public void Cheep_List_2WrongLengthTest2()
    {

        //AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
        // Arrange
        string author = "Christine";
        string message = "I'm iron";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        

        CheepDTO cheep1 = new CheepDTO("Kasper", "Byebye", date);
        CheepDTO cheep2 = new CheepDTO("Jacques", "Salam", date);
        CheepDTO cheep3 = new CheepDTO("Stanley", "Aleikum", date);
        CheepDTO cheep4 = new CheepDTO("Sisse", "<3 elias", date);
         List<CheepDTO> cheeps = new List<CheepDTO>();
         cheeps.Add(cheep1);
         cheeps.Add(cheep2);
         cheeps.Add(cheep3);
         cheeps.Add (cheep4);


        CheepDTO cheep5 = new CheepDTO("Hooyo", "Hej salwa", date);
        CheepDTO cheep6 = new CheepDTO("Freja", "OKKKKK", date);
        List<CheepDTO> cheeps2 = new List<CheepDTO>();
        int expected = cheeps2.Count();


        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);

        // Act
        int actual = author1.Cheeps.Count();;

        // Assert
        Assert.NotEqual(expected, actual);
    }


}