using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
using System.ComponentModel;
namespace Chirp.Infrastructure.Tests;

//This class contains all the unit tests regarding the cheep properties


public class CheepTest
{
    //UNIX Timestamp conversion to user readable time
    [Fact]
    public void UNIX_1695054881_2ActualDateTest()
    {

        // Arrange
        //long UNIX2Convert = 1695054881;
        string time = "09/18/23 16:34:41";
        DateTime expected = DateTime.Parse(time); 
        

        // Act
        CheepDTO cheep = new CheepDTO("Saynab", "Hej med dig min ven",expected);
        DateTime actual = cheep.TimeStamp;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void UNIX_1660239950_2ActualDateTest()
    {
        // Arrange
       // long UNIX2Convert = 1660239950;
        string time = "08/11/22 17:45:50";
        DateTime expected = DateTime.Parse(time); 

        // Act
        CheepDTO cheep = new CheepDTO("Dima", "Vises",expected);
        DateTime actual = cheep.TimeStamp;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void UNIX_1695054881_2WrongDateTest()
    {
        // Arrange
        //long UNIX2Convert = 1695054881;
        DateTime notExpected = DateTime.Parse("07/10/23 17:25:09");
        DateTime expected = DateTime.Parse("08/11/22 17:45:50");  

        // Act
        CheepDTO cheep = new CheepDTO("Mikkel", "Who?",expected);
        DateTime actual = cheep.TimeStamp;

        // Assert
        Assert.NotEqual(notExpected, actual);
    }

    [Fact]
    public void UNIX_1695063038_2WrongDateTest()
    {
        // Arrange
        //long UNIX2Convert = 1695063038;
        DateTime notExpected = DateTime.Parse("09/19/23 18:10:55");
        DateTime expected = DateTime.Parse("07/10/23 17:25:09");
        // Act
        CheepDTO cheep = new CheepDTO("Herman", "Hello folks",expected);
        DateTime actual = cheep.TimeStamp;

        // Assert
        Assert.NotEqual(notExpected, actual);
    }




    //Formatting of a Cheep record to user interface 
    [Fact]
    public void FormatCheep1_2ActualMessageTest()
    {
        // Arrange
        string author = "christina";
        string message = "I am currently testing this";
        DateTime date = DateTime.Parse("07/10/23 17:25:09");
        string expected = "I am currently testing this";
        CheepDTO cheep = new CheepDTO(author, message, date);

        // Act
        string actual = cheep.Message;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FormatCheep2_2ActualMessageTest()
    {
        // Arrange
        string author = "Saynab";
        string message = "Merry Christmas!";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Merry Christmas!";
        CheepDTO cheep = new CheepDTO(author, message, date);

        // Act
        string actual = cheep.Message;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FormatCheep_2WrongMessageTest()
    {
        // Arrange
        string author = "qwerty";
        string message = "I dont know who I am";
        DateTime date = DateTime.Parse("08/11/22 17:45:50");
        string expected = "I am Qwerty, the one and only";
        CheepDTO cheep = new CheepDTO(author, message, date);

        // Act
        string actual = cheep.Message;

        // Assert
        Assert.NotEqual(expected, actual);
    }




    [Fact]
    public void FormatCheep_2WrongMessageTest2()
    {
        // Arrange
        string author = "haiwan";
        string message = "Hejhej jeg går min vej";
        DateTime date = DateTime.Parse("08/11/22 17:45:50");
        string expected = "Ses med dig broski";
        CheepDTO cheep = new CheepDTO(author, message, date);

        // Act
        string actual = cheep.Message;

        // Assert
        Assert.NotEqual(expected, actual);
    }


}