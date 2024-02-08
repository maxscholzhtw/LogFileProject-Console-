namespace BlazorApp1.Components.Models;

public class Program
{
    private static void Main(string[] args)
    {
        string eingabePfad = "C:\\Users\\Maximilian.Scholz\\Programm Mike\\FW-log(klein).txt";
        LogFileReader test = new LogFileReader(eingabePfad);

        Console.WriteLine("Beliebege Taste drücken, um Ergebnisse auszugeben");
        ConsoleKeyInfo keyinfo;

        do
        {
            keyinfo = Console.ReadKey();
            test.ReadContent();
            test.SplitFileContent();
            test.StarteConvertToLogEintag();
        } while (keyinfo.Key != ConsoleKey.X);

        Console.WriteLine("Bitte G drücken um Datei zu lesen!");
        do
        {
            keyinfo = Console.ReadKey();
            test.Counter();
        } while (keyinfo.Key != ConsoleKey.G);

        // Hier wird die Konsole geöffnet, bis der Benutzer eine Eingabe tätigt
        Console.WriteLine("Programm beendet. Drücken Sie eine beliebige Taste, um zu schließen.");
        Console.ReadKey();

        string csvFilePath = "output.csv";
        test.SaveToCSV(csvFilePath);
    }
}