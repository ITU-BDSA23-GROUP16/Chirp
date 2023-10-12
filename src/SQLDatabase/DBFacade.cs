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
    private string customDelimiter = "SPLITONTHISSTRINGSPECIFICALLY";

    //Filepath to the database
    private string sqlDBFilePath;

    //Attributes
    private SqliteConnection connection;
    //Query
    private string sqlQuery = @"SELECT message.*, user.username FROM message JOIN user ON message.author_id = user.user_id ORDER BY message.pub_date DESC;";
    private string sqlQueryForAuthor = @"SELECT message.*, user.username FROM message JOIN user ON message.author_id = user.user_id ORDER BY message.pub_date DESC;";

    public DBFacade()
    {
        //Calling dotnet run will store the database under users temporary directiory tmp, under name chirp.db
        //But calling CHIRPDBPATH=./mychirp.db dotnet run
        //From StackOverFlow: https://stackoverflow.com/questions/22451172/finding-whether-environment-variable-is-defined-or-not-in-c-sharp
        if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("CHIRPDBPATH")))
            sqlDBFilePath = Environment.GetEnvironmentVariable("CHIRPDBPATH");
        else
            sqlDBFilePath = "/tmp/chirp.db";

        //Creating SQL Connection, by instantiating attribute connection
        //This connection we want to use in the query methods
        connection = new SqliteConnection($"Data Source={sqlDBFilePath}");
        connection.Open();
    }


    //The method is meant to use a specific query, that can be used in the ParseRowFromData
    private IDbCommand SetCommandQuery(string query)
    {
        //Creating SQL command
        var command = connection.CreateCommand();

        //Setting the command to be our query
        command.CommandText = query;

        return command;
    }

    //Return list of Generic T that contains whats necessesary for a CheepViewModel.
    public List<T> CheepReturn<T>()
    {
        return ParseRowFromData<T>(SetCommandQuery(sqlQuery));
    }

    //Eveything we put in <T> is what se work with in List<T>
    private List<T> ParseRowFromData<T>(IDbCommand command)
    {
        //Creating a SQL data reader (command.ExecuteReader())
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

    /*
       public List<T> CheepReturnFromAuthorNotWorking<T>(string? au = null)
       {
           string sqlQueryCustom = "";
           string baseQuery = "SELECT message.*, user.username FROM message JOIN user ON message.author_id = user.user_id ";
           string orderSuffix = " ORDER BY message.pub_date DESC;";

           if (au == null)
           {
               sqlQueryCustom = baseQuery + orderSuffix;
           }
           else
           {
               sqlQueryCustom = baseQuery + "WHERE user.username = $AUTHOR" + orderSuffix;
           }

           IDbCommand command;

           using (connection)
           {
               //connection.Open();
               command = createConnection(sqlQueryCustom);

               if (au != null)
               {
                   // Cast the command.Parameters collection to a SQLiteParameterCollection object.
                   var sqliteParameters = (SQLiteParameterCollection)command.Parameters;

                   // Add the authorid parameter to the command.
                   sqliteParameters.AddWithValue("$AUTHOR", au);
               }
           }


           return CheepsFromDB<T>(command);
       }
       */



}