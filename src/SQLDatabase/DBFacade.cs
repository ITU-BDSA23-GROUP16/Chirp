using System.Data;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace DB;

public class DBFacade
{
    private static string customDelimiter = "SPLITONTHISSTRINGSPECIFICALLY";

    public List<T> ReturnCheeps<T>() //Needs refactoring!
    {
        //Users temporary database
        //Calling dotnet run will store the database under users temporary directiory tmp, under name chirp.db
        if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("CHIRPDBPATH")))
            Console.WriteLine(Environment.GetEnvironmentVariable("CHIRPDBPATH"));
        Console.WriteLine("hello");

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
            var dataRecord = (IDataRecord)reader;
            Console.WriteLine(dataRecord.FieldCount);
            for (int i = 0; i < dataRecord.FieldCount; i++)
                Console.WriteLine($"{dataRecord.GetName(i)}: {dataRecord[i]}");
            if (dataRecord.FieldCount != 0)
            {
                string stringparse = $"{dataRecord[4]}{customDelimiter}{dataRecord[2]}{customDelimiter}{dataRecord[3]}";

                var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = customDelimiter
                };
                using var readerCSV = new StringReader(stringparse);
                var csv = new CsvReader(readerCSV, config);
                //csv.HasHeaderRecord = false;
                csv.Read();

                var record = csv.GetRecord<T>();
                // Do something with the record.
                //Console.WriteLine(record.ToString());
                returnList.Add(record);

            }
        }

        return returnList;

    }

}
