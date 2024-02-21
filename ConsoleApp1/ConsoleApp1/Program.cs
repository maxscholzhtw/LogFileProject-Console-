using ConsoleApp1.Model;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Bitte geben Sie den Dateipfad zur Logdatei ein:");
            string logFilePath = Console.ReadLine().Trim('\"'); // Entferne Anführungszeichen;

            Console.WriteLine("Bitte geben Sie den Dateipfad für die CSV-Datei ein:");
            string csvFilePath = Console.ReadLine().Trim('\"'); // Entferne Anführungszeichen;;
            
            LogFileReader test = new LogFileReader(logFilePath);

            ConsoleKeyInfo keyinfo;

            test.ReadContent();
            test.SplitFileContent();
            test.StartConvertToLogEntry();

            // Speichern der CSV-Datei
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

//Change
    
    