using ConsoleApp1.Model;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            LogFileReader test = null;

            Console.WriteLine("Möchten Sie eine CSV-Datei erstellen? (ja/nein):");
            string createCSV = Console.ReadLine().ToLower();

            if (createCSV == "ja")
            {
                Console.WriteLine("Bitte geben Sie den Dateipfad zur Logdatei ein:");
                string logFilePath = Console.ReadLine().Trim('\"'); // Entferne Anführungszeichen

                test = new LogFileReader(logFilePath);

                test.ReadContent();
                test.SplitFileContent();
                test.StartConvertToLogEntry();

                Console.WriteLine("Bitte geben Sie den Dateipfad für die CSV-Datei ein:");
                string csvFilePath = Console.ReadLine().Trim('\"'); // Entferne Anführungszeichen

                // Speichern der CSV-Datei
                Console.WriteLine("");
                csvFilePath = test.SaveToCSV(csvFilePath);
                Console.WriteLine("");
                Console.WriteLine($"Die CSV-Datei wurde erfolgreich erstellt! Pfad: {csvFilePath}");
            }
            else
            {
                Console.WriteLine("Bitte geben Sie den Dateipfad zur Logdatei ein:");
                string logFilePath = Console.ReadLine().Trim('\"'); // Entferne Anführungszeichen

                test = new LogFileReader(logFilePath);

                test.ReadContent();
                test.SplitFileContent();
                test.StartConvertToLogEntry();

                // Ausgabe der IP-Adressen mit ihrer Häufigkeit
                PrintIPFrequency(test);
            }

            Console.WriteLine("");
            Console.WriteLine("Das Programm wird beendet.");
        }

        private static void PrintIPFrequency(LogFileReader test)
        {
            List<Format> ipFrequencyList = test.PrintIPFrequency();
            
            {
                Console.WriteLine("");
                Console.WriteLine("Es wurden keine IP-Adressen gefunden.");
            }
        }
    }
}

//Change
    
    