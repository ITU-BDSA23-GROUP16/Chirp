using System.Data;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace DB;

public class DBFacade
{
    public void printHej()
    {
        Console.WriteLine("hej");
    }
    public List<String> ReturnRow()
    {
        //Users temporary database
        //Calling dotnet run will store the database under users temporary directiory tmp, under name chirp.db
        // 1. Check if CHIRPDBPATH exists
        // 2. IF exists take its value as path
        // 3. ELSE take /tmp/chirp.db
        // CHIRPDBPATH=./chirp.db dotnet...

        if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("CHIRPDBPATH")))
            Console.WriteLine(Environment.GetEnvironmentVariable("CHIRPDBPATH"));
        Console.WriteLine("hello");
        
        var sqlDBFilePath = "/tmp/chirp.db";



        //Query
        var sqlQuery = @"SELECT * FROM message ORDER by message.pub_date desc";

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
        string AID;
        string text;
        string time;

        List<String> returnList = new List<String>();
        while (reader.Read())
        {
            //Array of length of all the columns = 4
            Object[] values = new Object[reader.FieldCount];
            //reader.GetValues(values) fills up values array with the values of the columns
            //And returns the count of the columns.
            int fieldCount = reader.GetValues(values);

            AID = values[1].ToString();
            text = values[2].ToString();
            time = values[3].ToString();

            string returnString = AID + ", " + text + ", " + time;

            returnList.Add(returnString);


        }

        return returnList;

    }


    //Call Read before accessing data.

    /*
    while (reader.Read())
    {

        // https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader?view=dotnet-plat-ext-7.0#examples
        var dataRecord = (IDataRecord)reader;
        for (int i = 0; i < dataRecord.FieldCount; i++)
            //Prints the column of the data, and then the value inside that column
            Console.WriteLine($"{dataRecord.GetName(i)}: {dataRecord[i]}");

        // See https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader.getvalues?view=dotnet-plat-ext-7.0
        // for documentation on how to retrieve complete columns from query results
        //Array of length of all the columns = 4
        Object[] values = new Object[reader.FieldCount];
        //reader.GetValues(values) fills up values array with the values of the columns
        //And returns the count of the columns.
        int fieldCount = reader.GetValues(values);
        for (int i = 0; i < fieldCount; i++)
            //Prints the column and the value for that column
            Console.WriteLine($"{reader.GetName(i)}: {values[i]}");

    }
    */

}
