﻿using ConsoleApp1.Model;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string logFilePath =
                "C:\\Users\\Maximilian.Scholz\\GitHubCode\\LogFileProject-Console-\\FW-log(klein).txt";
            LogFileReader test = new LogFileReader(logFilePath);

            ConsoleKeyInfo keyinfo;
            
                test.ReadContent();
                test.SplitFileContent();
                test.StartConvertToLogEntry();
                
            // Speichern der CSV-Datei
             string csvFilePath = "Blocked_IPs.csv";
             csvFilePath = test.SaveToCSV(csvFilePath);

            // Überprüfen, ob die Datei erfolgreich erstellt wurde
            if (File.Exists(csvFilePath))
            {
                Console.WriteLine("Fertig!");
            }
            else
            {
                Console.WriteLine("Die Datei wurde nicht erstellt.");
            }

            Console.WriteLine("Das Programm wird beendet.");
        }
    }
}
    
    