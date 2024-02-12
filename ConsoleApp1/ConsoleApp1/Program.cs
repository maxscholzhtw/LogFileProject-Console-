using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BlazorApp1.Components.Models;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string logFilePath = "\"C:\\Users\\Maximilian.Scholz\\GitHubCode\\LogFileProject-Console-\\FW-log(megaklein).txt\"";
            LogFileReader test = new LogFileReader(logFilePath);

            ConsoleKeyInfo keyinfo;

            do
            {
                keyinfo = Console.ReadKey();
                test.ReadContent();
                test.SplitFileContent();
                test.StartConvertToLogEntry(); // Hier aufrufen
            } while (keyinfo.Key != ConsoleKey.X);
    
            do
            {
                keyinfo = Console.ReadKey();
                test.Counter();
            } while (keyinfo.Key != ConsoleKey.G);

            string csvFilePath = "output.csv";
            test.SaveToCSV(csvFilePath);

            LogFileReader reader = new LogFileReader(logFilePath);
            reader.StartConvertToLogEntry(); // Sicherstellen, dass auch hier die Einträge geladen werden

            reader.SaveToCSV(csvFilePath);

            if (File.Exists(csvFilePath))
            {
                Console.WriteLine("Die Datei wurde erfolgreich erstellt.");
            }
            else
            {
                Console.WriteLine("Die Datei wurde nicht erstellt.");
            }
        }
    }
}

    
    