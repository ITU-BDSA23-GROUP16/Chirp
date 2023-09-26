using System;
using System.Diagnostics;
using System.IO;
using Xunit;
// using FluentAssertions;

/*
! output and fstCheep strings are empty strings
! output is not updated with reader.ReadToEnd()
*/

public class End2EndTests
{
    [Fact]
    public void Test1()
    {
        string dir = Directory.GetCurrentDirectory();
        Console.WriteLine("This is printed --> " + dir);
        string output = "";
        File.WriteAllText("../../../../../chirp_cli_db.csv", "Author,Message,Timestamp\nropf,\"Hello, BDSA students!\",1690891760");


        using (var process = new Process())
        {
            process.StartInfo.FileName = "dotnet";
            process.StartInfo.Arguments = "run --project src/Chirp.CLI/Chirp.CLI.csproj read 10";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = Path.Combine("..", "..", "..", "..", "..");
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            output = reader.ReadToEnd();
            process.WaitForExit();
        }

        Console.WriteLine("This is output --> " + output);
        string fstCheep = output.Split("\n")[0].Trim();

        Console.WriteLine("This is fstCheep --> " + fstCheep);
        // fstCheep.Should().StartWith("ropf").And.EndWith("Hello World!");
        Assert.StartsWith("ropf", fstCheep);
        Console.WriteLine(fstCheep);
        Assert.EndsWith("Hello, BDSA students!", fstCheep);
    }
}