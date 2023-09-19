using Xunit;
using SimpleDB;
namespace SimpleDB.Tests

public class StoredIntegrationTest{

    private string file = "chirp_cli_db.csv"
    private long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()


        [Fact]
        public void IsStored(){
  
            
        //Arrange
        var database = new CSVDatabase<Cheep>(file);
        var cheep = new Cheep("Abu", "This is a message", timestamp);
                

        //Act
        database.Store(cheep);
        //var stored_cheep = database.Read(cheep)


        //Assert
        var storeCheeps = database.Read().ToList();
        Assert.Contains(cheep, storedCheeps);


        //Flush
        File.Delete(file);

        }
    }

