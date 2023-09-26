
// https://joshclose.github.io/CsvHelper/getting-started/


using CsvHelper;
using System.Globalization;

namespace SimpleDB;

public sealed class CSVDatabase<T> : IDatabaseRepository<T>
{
    static string file = "chirp_cli_db.csv";

    private static readonly CSVDatabase<T> instance = new CSVDatabase<T>(file);

    static CSVDatabase()
    {

    }

    private CSVDatabase(string file)
    {
        CSVDatabase<T>.file = file;
    }

    public static CSVDatabase<T> Instance()
    {
        return instance;

    }
    public static CSVDatabase<T> Instance(string file)
    {
        CSVDatabase<T>.file = file;
        return instance;
    }





    /* 
The following code includes parts from "Csvhelper - getting started" (the link below) to help rewrite our csv reader using records
https://joshclose.github.io/CsvHelper/getting-started/
*/
    public IEnumerable<T> Read(int? limit = null)
    {
        try
        {
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToList();
            }
        }
        catch (System.IO.FileNotFoundException e)
        {
            Init();
            return Read(limit);
        }
    }


    public void Store(T record)
    {
        try
        {
            using (var stream = File.Open(file, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {

                csv.WriteRecord(record);
                csv.NextRecord();


            };
        }
        catch (System.IO.FileNotFoundException e)
        {
            Init();
            Store(record);
        }
    }

    public void Init()
    {
        File.WriteAllText(file, "Author,Message,Timestamp\n");

    }
}