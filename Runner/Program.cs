using System;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.WorkingDirectory = @"C:\Users\claq2\source\repos\Spurious2\LcboScraper\bin\Release\netcoreapp2.2\publish";
            process.StartInfo.FileName = @"C:\Users\claq2\source\repos\Spurious2\LcboScraper\bin\Release\netcoreapp2.2\publish\LcboScraper.exe";// D:\home\site\wwwroot\LcboScraper\LcboScraper.dll";
            //process.StartInfo.Arguments = @"C:\Users\claq2\source\repos\Spurious2\LcboScraper\bin\Release\netcoreapp2.2\publish\LcboScraper.exe";
            process.StartInfo.Arguments = "InventoryFromDb 0 10";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            var f= process.StartInfo.ToString();
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine(output);
            Console.WriteLine(err);
        }
    }
}
