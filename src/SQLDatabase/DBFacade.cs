using System.Data;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace DB;

//Before running you need to run the sql files into the temporary database
//sqlite3 /tmp/chirp.db < SQLDatabase/schema.sql
//sqlite3 /tmp/chirp.db < SQLDatabase/dump.sql
public class DBFacade
{
    
    private static string customDelimiter = "SPLITONTHISSTRINGSPECIFICALLY";

    //Eveything we put in <T> is what se work with in List<T>
    public List<T> CheepsFromDB<T>() //Needs refactoring!
    {
        //Users temporary database
        //Calling dotnet run will store the database under users temporary directiory tmp, under name chirp.db
        if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("CHIRPDBPATH")))
            Console.WriteLine(Environment.GetEnvironmentVariable("CHIRPDBPATH"));

        var sqlDBFilePath = "/tmp/chirp.db";

        //Query
        var sqlQuery = @"SELECT message.*, user.username FROM message JOIN user ON message.author_id = user.user_id ORDER BY message.pub_date DESC;";

        //Creating SQL Connection
        var connection = new SqliteConnection($"Data Source={sqlDBFilePath}");
        using (connection) ;
        connection.Open();

        //Creating SQL command
        var command = connection.CreateCommand();

        //Setting the command to be our query
        command.CommandText = sqlQuery;

        //Creating a SQL data reader
        using var reader = command.ExecuteReader();

        //Create list to be added to and be return at  method call
        var returnList = new List<T>();
        //While theres something to read
        while (reader.Read())
        {

            //Casts reader to IDataRecord: https://learn.microsoft.com/en-us/dotnet/api/system.data.idatareader?view=net-7.0
            var dataRecord = (IDataRecord)reader;
            
            //Checks if the columns of the row are not 0, meaning if the row is not empty
            if (dataRecord.FieldCount != 0)
            {
                //it creates string of format: user.username: sisse, message.text: jeg hader ketchup, message.pub_date: 1634896200
                string stringparse = $"{dataRecord[4]}{customDelimiter}{dataRecord[2]}{customDelimiter}{dataRecord[3]}";

                //Changes the configuration of CsvHelper parsing, because we want to split on a specific delimiter, and dont want to consider a header(Because we only parse one row at a time, and not a csv file)
                //From https://joshclose.github.io/CsvHelper/getting-started/
                var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = customDelimiter
                };
                //Using the readerCSV to read the string by using the StringReader (like in StreamReader)
                //In the CSVDatabase, we use using (var ...) but here we take one command at a time
                using var readerCSV = new StringReader(stringparse);
                var csv = new CsvReader(readerCSV, config);
                //Read each row by hand
                csv.Read();
                
                //Assign each record to a variable
                var record = csv.GetRecord<T>();
                
                //Add the record to the list we want to return
                returnList.Add(record);
            }
        }

        return returnList;

    }

}
