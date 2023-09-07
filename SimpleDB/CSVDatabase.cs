﻿
// https://joshclose.github.io/CsvHelper/getting-started/



namespace SimpleDB;

public class CSVDatabase<T> : IDatabaseRepository<T>
{
    string file
    public CSVDatabase(string file) {
        this.file=file
        
    }
    
    /* 
    The following code includes parts from "Csvhelper - getting started" (the link below) to help rewrite our csv reader using records
    https://joshclose.github.io/CsvHelper/getting-started/
    */
    public IEnumerable<T> Read(int? limit = null){
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecord<Cheep>();
            }
    }


    public void Store(T record){
        using (var writer = new StreamWriter(file))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecord(record);
            writer.Flush(); 

        }
        


    }
}