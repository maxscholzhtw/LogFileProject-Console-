using BlazorApp1.Components.Models;

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

            do
            {
                test.ReadContent();
                test.SplitFileContent();
                test.PrintIPFrequency();
                test.StartConvertToLogEntry(); // Hier aufrufen

                keyinfo = Console.ReadKey(true); // Das Argument true bewirkt, dass die Eingabetaste nicht erwartet wird
            } while (keyinfo.Key != ConsoleKey.X);

            // Speichern der CSV-Datei
            string csvFilePath = "output.csv";
            test.SaveToCSV(csvFilePath);

            // Überprüfen, ob die Datei erfolgreich erstellt wurde
            if (File.Exists(csvFilePath))
            {
                Console.WriteLine("Die Datei wurde erfolgreich erstellt.");
            }
            else
            {
                Console.WriteLine("Die Datei wurde nicht erstellt.");
            }

            Console.WriteLine("Das Programm wird beendet.");
        }
    }
}
    
    