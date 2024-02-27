using ConsoleApp1.Model;


namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            bool programRunning = true;

            while (programRunning)
            {
                LogFileReader test = null;

                string createCSV;

                // Frage nach dem Erstellen einer CSV-Datei
                do
                {
                    Console.WriteLine("Möchten Sie eine CSV-Datei erstellen? (ja/nein):");
                    createCSV = Console.ReadLine().ToLower();
                    if (createCSV != "ja" && createCSV != "nein")
                    {
                        Console.WriteLine("Falsche Eingabe!");
                    }
                } while (createCSV != "ja" && createCSV != "nein");

                // Verarbeitung entsprechend der Antwort
                switch (createCSV)
                {
                    case "ja":
                        try
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
                        catch
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Falsche Eingabe!");
                            continue; // Zurück zur Frage "Möchten Sie eine CSV-Datei erstellen? (ja/nein):"
                        }
                        break;

                    case "nein":
                        try
                        {
                            Console.WriteLine("Bitte geben Sie den Dateipfad zur Logdatei ein:");
                            string logFilePath = Console.ReadLine().Trim('\"'); // Entferne Anführungszeichen

                            test = new LogFileReader(logFilePath);

                            test.ReadContent();
                            test.SplitFileContent();
                            test.StartConvertToLogEntry();

                            // Ausgabe der IP-Adressen mit ihrer Häufigkeit
                            test.PrintIPFrequency();
                        }
                        catch
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Falsche Eingabe!");
                            continue; // Zurück zur Frage "Möchten Sie eine CSV-Datei erstellen? (ja/nein):"
                        }
                        break;

                    default:
                        Console.WriteLine("");
                        Console.WriteLine("Falsche Eingabe!");
                        break;
                }

                string endProgram;

                // Frage nach dem Beenden des Programms
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("Möchten Sie das Programm beenden? (ja/nein):");
                    endProgram = Console.ReadLine().ToLower();
                    if (endProgram != "ja" && endProgram != "nein")
                    {
                        Console.WriteLine("Falsche Eingabe!");
                    }
                } while (endProgram != "ja" && endProgram != "nein");

                // Verarbeitung entsprechend der Antwort
                switch (endProgram)
                {
                    case "ja":
                        programRunning = false; // Beende das Programm
                        break;

                    case "nein":
                        // Starte das Programm neu
                        break;

                    default:
                        Console.WriteLine("Falsche Eingabe!");
                        break;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Das Programm wird beendet.");
        }
    }
}


//Change
    
    