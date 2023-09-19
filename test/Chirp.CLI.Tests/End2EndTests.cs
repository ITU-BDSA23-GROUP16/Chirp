using System.Diagnostics;
using System.IO;
using Xunit;

public class End2EndTests{
    [Fact]
    public void Test1(){
        string output = "";
        using (var process = new Process()){
            process.StartInfo.FileName = "dotnet";
            process.StartInfo.Arguments = "run --read 10";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            output = reader.ReadToEnd();
            process.WaitForExit();
        }

        string fstCheep = output.Split("\n")[0];

        Assert.StartsWith("ropf", fstCheep, StringComparison.Ordinal);
        Console.WriteLine(fstCheep);
        Assert.EndsWith("Hello World!", fstCheep, StringComparison.Ordinal);
    }
}