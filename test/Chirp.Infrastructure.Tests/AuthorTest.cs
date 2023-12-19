namespace Chirp.Infrastructure.Tests;
/// <summary>
/// This class contains all the unit tests regarding the author properties
/// These ensure the properties of Author and AuthorDTO match as expected.
/// Since DTO's are standins for the same objects, we do not consider this an integration test.
/// </summary>
public class AuthorTest
{

    [Fact]
    public void Author_Name_2ActualNameTest()
    {
        // Arrange
        string author = "Saynab";
        string message = "Merry Christmas!";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Saynab";

        CheepDTO cheep1 = new CheepDTO("Dima", "Vejret er så dejligt", date);
        CheepDTO cheep2 = new CheepDTO("Mikkel", "Spiste verdens bedste is", date);
        List<CheepDTO> cheeps = new List<CheepDTO>();
        cheeps.Add(cheep1);
        cheeps.Add(cheep2);

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        string actual = author1.Name;

        // Assert
        Assert.Equal(expected, actual);
    }



    [Fact]
    public void Author_Name_2ActualNameTest2()
    {
        // Arrange
        string author = "Jeppe";
        string message = "Godt nytår!";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Jeppe";

        CheepDTO cheep1 = new CheepDTO("Søren", "Jeg elsker dig Birgit", date);
        CheepDTO cheep2 = new CheepDTO("Chad", "Im going to the mall today", date);
        List<CheepDTO> cheeps = new List<CheepDTO>();
        cheeps.Add(cheep1);
        cheeps.Add(cheep2);

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        string actual = author1.Name;

        // Assert
        Assert.Equal(expected, actual);
    }


    [Fact]
    public void Author_Name_2ActualNameTest3()
    {
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

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        string actual = author1.Name;

        // Assert
        Assert.Equal(expected, actual);
    }



    [Fact]
    public void Author_Name_2WrongNameTest()
    {
        // Arrange
        string author = "Kirsten";
        string message = "Hej kan du ringe tilbage til mig";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Thore";

        CheepDTO cheep1 = new CheepDTO("Diego", "Når man har det godt, skal det fejres", date);
        CheepDTO cheep2 = new CheepDTO("Courtney", "Im sooooo over this", date);
        List<CheepDTO> cheeps = new List<CheepDTO>();
        cheeps.Add(cheep1);
        cheeps.Add(cheep2);

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        string actual = author1.Name;

        // Assert
        Assert.NotEqual(expected, actual);
    }




    [Fact]
    public void Author_Name_2WrongNameTest2()
    {
        // Arrange
        string author = "Chrusty";
        string message = "Hej kan du ringe tilbage til mig";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");
        string expected = "Dusty";

        CheepDTO cheep1 = new CheepDTO("Puffy", "Hejhej", date);
        CheepDTO cheep2 = new CheepDTO("Jasper", "Er såååå træt af alle og alt", date);
        List<CheepDTO> cheeps = new List<CheepDTO>();
        cheeps.Add(cheep1);
        cheeps.Add(cheep2);

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        string actual = author1.Name;

        // Assert
        Assert.NotEqual(expected, actual);
    }

    [Fact]
    public void Cheep_List_2ActualLengthTest()
    {
        // Arrange
        string author = "Saynab";
        string message = "Merry Christmas!";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");

        CheepDTO cheep1 = new CheepDTO("Dima", "Håber i har det godt gutter", date);
        CheepDTO cheep2 = new CheepDTO("Mikkel", "Who asked is the real question to everything?", date);
        List<CheepDTO> cheeps = new List<CheepDTO>();
        cheeps.Add(cheep1);
        cheeps.Add(cheep2);
        int expected = cheeps.Count();

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        int actual = author1.Cheeps.Count(); ;

        // Assert
        Assert.Equal(expected, actual);
    }



    [Fact]
    public void Cheep_List_2ActualLengthTest2()
    {
        // Arrange
        string author = "Dima";
        string message = "I'm radiant";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");

        CheepDTO cheep1 = new CheepDTO("Kasper", "Byebye", date);
        CheepDTO cheep2 = new CheepDTO("Jacques", "Salam Aleikum", date);
        CheepDTO cheep3 = new CheepDTO("Stanley", "Bonjour", date);
        List<CheepDTO> cheeps = new List<CheepDTO>();
        cheeps.Add(cheep1);
        cheeps.Add(cheep2);
        cheeps.Add(cheep3);
        int expected = cheeps.Count();

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        int actual = author1.Cheeps.Count(); ;

        // Assert
        Assert.Equal(expected, actual);
    }


    [Fact]
    public void Cheep_List_2WrongLengthTest()
    {
        // Arrange
        string author = "Christine";
        string message = "I'm iron";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");

        CheepDTO cheep1 = new CheepDTO("Kasper", "Hej mine venner og veninder", date);
        CheepDTO cheep2 = new CheepDTO("Jacques", "I dag er en rigtig lorte dag", date);
        CheepDTO cheep3 = new CheepDTO("Stanley", "Jeg hader mit liv", date);
        List<CheepDTO> cheeps = new List<CheepDTO>();
        cheeps.Add(cheep1);
        cheeps.Add(cheep2);
        cheeps.Add(cheep3);


        CheepDTO cheep4 = new CheepDTO("Jacques", "Hvad skal i lave i dag?", date);
        CheepDTO cheep5 = new CheepDTO("Stanley", "Jeg elsker Harry Styles", date);
        List<CheepDTO> cheeps2 = new List<CheepDTO>();
        cheeps2.Add(cheep4);
        cheeps2.Add(cheep5);

        int expected = cheeps2.Count();

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        int actual = author1.Cheeps.Count(); ;

        // Assert
        Assert.NotEqual(expected, actual);
    }


    [Fact]
    public void Cheep_List_2WrongLengthTest2()
    {
        // Arrange
        string author = "Jeppe";
        string message = "Whaaaaaat uuup!";
        DateTime date = DateTime.Parse("09/19/23 18:10:55");

        CheepDTO cheep1 = new CheepDTO("Kasper", "Øhmmm hvorfor skal alle bare være søde, det sus", date);
        CheepDTO cheep2 = new CheepDTO("Jacques", "Min mor sagde at hun hader jer alle", date);
        CheepDTO cheep3 = new CheepDTO("Stanley", "Åbenbart er jeg problematic for at elske Harry Styles", date);
        CheepDTO cheep4 = new CheepDTO("Sisse", "<3 elias", date);
        List<CheepDTO> cheeps = new List<CheepDTO>();
        cheeps.Add(cheep1);
        cheeps.Add(cheep2);
        cheeps.Add(cheep3);
        cheeps.Add(cheep4);

        CheepDTO cheep5 = new CheepDTO("Hooyo", "Hej salwa", date);
        CheepDTO cheep6 = new CheepDTO("Freja", "Min far vil ikke lade mig gå ud, han er så dum", date);
        List<CheepDTO> cheeps2 = new List<CheepDTO>();
        cheeps2.Add(cheep5);
        cheeps2.Add(cheep6);
        int expected = cheeps2.Count();

        // Act
        AuthorDTO author1 = new AuthorDTO(author, message, cheeps);
        int actual = author1.Cheeps.Count();

        // Assert
        Assert.NotEqual(expected, actual);
    }

}