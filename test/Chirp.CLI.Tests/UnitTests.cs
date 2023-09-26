namespace Chirp.CLI.Tests;
using Xunit;
using UI;
using SimpleDB;

//To get UNIX timestamps and correlating user readable time, the following website has been used: https://www.unixtimestamp.com/?ref=dtf.ru

public class UnitTests
{
    //UNIX Timestamp conversion to user readable time
    [Fact]
    public void UNIX_1695054881_2ActualDateTest()
    {
        // Arrange
        long UNIX2Convert = 1695054881;
        string expected   = "09/18/23 16.34.41";

        // Act
        string actual     = UserInterface.ConvertTimestampToDate(UNIX2Convert);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void UNIX_1660239950_2ActualDateTest()
    {
        // Arrange
        long UNIX2Convert = 1660239950;
        string expected   = "08/11/22 17.45.50";

        // Act
        string actual     = UserInterface.ConvertTimestampToDate(UNIX2Convert);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void UNIX_1695054881_2WrongDateTest()
    {
        // Arrange
        long UNIX2Convert  = 1695054881;
        string notExpected = "07/10/23 17.25.09";

        // Act
        string actual      = UserInterface.ConvertTimestampToDate(UNIX2Convert);

        // Assert
        Assert.NotEqual(notExpected, actual);
    }

    [Fact]
    public void UNIX_1695063038_2WrongDateTest()
    {
        // Arrange
        long UNIX2Convert  = 1695063038;
        string notExpected = "09/19/23 18.10.55";

        // Act
        string actual      = UserInterface.ConvertTimestampToDate(UNIX2Convert);

        // Assert
        Assert.NotEqual(notExpected, actual);
    }




    //Formatting of a Cheep record to user interface 
    [Fact]
    public void FormatCheep1_2ActualMessageTest()
    {
        // Arrange
        string author   = "christina";
        string message  = "I am currently testing this";
        long date       = 1695107575;
        string expected = "christina @ 09/19/23 07.12.55: I am currently testing this";
        Cheep cheep        = new Cheep(author,message,date);

        // Act
        string actual      = UserInterface.GetCheepFormattedMessage(cheep);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FormatCheep2_2ActualMessageTest()
    {
        // Arrange
        string author   = "christina";
        string message  = "Merry Christmas!";
        long date       = 1703445150;
        string expected = "christina @ 12/24/23 19.12.30: Merry Christmas!";
        Cheep cheep        = new Cheep(author,message,date);

        // Act
        string actual      = UserInterface.GetCheepFormattedMessage(cheep);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FormatCheep_2WrongMessageTest()
    {
        // Arrange
        string author   = "qwerty";
        string message  = "I dont know who I am";
        long date       = 1695107575;
        string expected = "qwerty @ 09/19/23 07.12.55: I am Qwerty, the one and only";
        Cheep cheep        = new Cheep(author,message,date);

        // Act
        string actual      = UserInterface.GetCheepFormattedMessage(cheep);

        // Assert
        Assert.NotEqual(expected, actual);
    }

}