namespace SimpleDB.Tests;

using Xunit;
using SimpleDB;
using UI;

public class StoredIntegrationTest
{
        private string file = "../../../CSV-test.csv";
        private long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        [Fact] 
        public void IsStoredAbuCheep(){
        //Arrange
        var database = new CSVDatabase<Cheep>(file);
        var cheep = new Cheep("Abu", "This is a message", timestamp);   

        //Act
        database.Store(cheep);

        //Assert
        var storedCheeps = database.Read().ToList();
        Assert.Contains(cheep, storedCheeps);

        //Flush
        File.WriteAllText(file, "Author,Message,Timestamp\n" );
        }


        [Fact] 
        public void IsStoredChristineCheep(){
        //Arrange
        var database = new CSVDatabase<Cheep>(file);
        var cheep = new Cheep("Christine", "Abushabu", timestamp);
                
        //Act
        database.Store(cheep);

        //Assert
        var storedCheeps = database.Read().ToList();
        Assert.Contains(cheep, storedCheeps);

        //Flush
        File.WriteAllText(file, "Author,Message,Timestamp\n" );
        }


        [Fact] 
        public void IsStoredScorpionCheep(){
        //Arrange
        var database = new CSVDatabase<Cheep>(file);
        var cheep = new Cheep("MyScorpion42", "LET ME INNNNN", timestamp);
                
        //Act
        database.Store(cheep);

        //Assert
        var storedCheeps = database.Read().ToList();
        Assert.Contains(cheep, storedCheeps);

        //Flush
        File.WriteAllText(file, "Author,Message,Timestamp\n" );
        }


        [Fact] 
        public void IsStoredStanCheep(){
        //Arrange
        var database = new CSVDatabase<Cheep>(file);
        var cheep = new Cheep("Stanlee", "Genshin 4 life", timestamp);
                
        //Act
        database.Store(cheep);
    
        //Assert
        var storedCheeps = database.Read().ToList();
        Assert.Contains(cheep, storedCheeps);

        //Flush
        File.WriteAllText(file, "Author,Message,Timestamp\n" );
        }


        [Fact] 
        public void IsStoredSayCheep(){
        //Arrange
        var database = new CSVDatabase<Cheep>(file);
        var cheep = new Cheep("Sisse", "Jeg kan ikk lide ketchup", timestamp);
                
        //Act
        database.Store(cheep);

        //Assert
        var storedCheeps = database.Read().ToList();
        Assert.Contains(cheep, storedCheeps);

        //Flush
        File.WriteAllText(file, "Author,Message,Timestamp\n" );
        }


        [Fact] 
        public void IsStoredDimaCheep(){
        //Arrange
        var database = new CSVDatabase<Cheep>(file);
        var cheep = new Cheep("DJ", "pspspspsps", timestamp);
                
        //Act
        database.Store(cheep);

        //Assert
        var storedCheeps = database.Read().ToList();
        Assert.Contains(cheep, storedCheeps);

        //Flush
        File.WriteAllText(file, "Author,Message,Timestamp\n" );
        }



        //Test reading 
        [Fact]
        public void ReadCheep1() {

            //Arrange
            var database = new CSVDatabase<Cheep>(file);
            var cheep = new Cheep("Dima","Today is a bad day:(", timestamp);

            database.Store(cheep);
            var actualCheep = database.Read(1).Last();
            //Act 
            var expectedCheep = new Cheep("Dima","Today is a bad day:(", timestamp);

            //Assert 
            Assert.Equal(expectedCheep, actualCheep);

            //Flush
            File.WriteAllText(file, "Author,Message,Timestamp\n" );

        }

    }

