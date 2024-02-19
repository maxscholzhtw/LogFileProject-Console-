using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlazorApp1.Components.Models
{
    public class LogFileReader
    {
        public List<LogEintrag> LogEintraege { get; } = new List<LogEintrag>(); // Liste ohne redundante Typspezifikation

        public string logFilePath { get; } // Nur get; verwenden, da die Eigenschaft nur im Konstruktor gesetzt wird

        public string FileContent { get; private set; }
        public string[] SplitContent { get; private set; }

        public LogFileReader(string logFilePath)
        {
            this.logFilePath = logFilePath;
            if (File.Exists(logFilePath))
            {
                Console.WriteLine("Beliebige Taste drücken, um Ergebnisse auszugeben");
            }
            else
            {
                throw new Exception($"Die Datei im Pfad:{logFilePath} wurde nicht gefunden.");
            }
        }

        public void Counter()
        {
            int wordCount = FileContent.Count(c => c == ' ') + 1;
            Console.WriteLine($"Anzahl der Wörter in der Datei: {wordCount}");
        }

          public void ReadContent()
          {
                try
                {
                    FileContent = File.ReadAllText(logFilePath);
                }
                catch (Exception ex)
                { 
                    Console.WriteLine("");
                    Console.WriteLine($"Fehler beim Lesen der Datei: {ex.Message}");
                }
          }

          public void SplitFileContent()
          {
              try
              {
                  SplitContent = FileContent.Split('\n');
              }
              catch (Exception ex)
              {
                  Console.WriteLine($"Fehler beim Aufteilen des Dateiinhalts: {ex.Message}");
              }
          }
        
        public void PrintIPFrequency()
        {
            var ipFrequency = LogEintraege.GroupBy(x => x.SourceIP)
                .Select(g => new { IP = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            foreach (var item in ipFrequency)
            {
                Console.WriteLine($"IP-Adresse: {item.IP}, Häufigkeit: {item.Count}");
            }
        }

          public void StartConvertToLogEntry()
          {
                try
                {
                 foreach (var entry in SplitContent)
                 {
                      LogEintraege.Add(ConvertStringToLogEntry(entry));
                 }
                }
                catch (Exception ex)
                {
                 Console.WriteLine($"Fehler beim Konvertieren des Dateiinhalts in Log-Einträge: {ex.Message}");
                }
          }

        public void SaveToCSV(string csvFilePath)
        {
            try
            {
                string fullPath = Path.Combine("C:\\Users\\Maximilian.Scholz\\GitHubCode", csvFilePath);
                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    foreach (var entry in LogEintraege)
                    {
                        string line = $"{entry.ID},{entry.Severity},{entry.Sys},{entry.Sub},{entry.Name},{entry.Action},{entry.FWRule},{entry.Initf},{entry.SourceMAC},{entry.DestinationMAC},{entry.SourceIP},{entry.DestinationIP},{entry.Protocol},{entry.Length},{entry.Tos},{entry.Prec},{entry.TTL},{entry.SourcePort},{entry.DestinationPort},{entry.TCPFlags}";
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine($"Die Ausgabe wurde erfolgreich in die CSV-Datei '{fullPath}' gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Ausgabe in die CSV-Datei: {ex.Message}");
            }
        }

        public LogEintrag ConvertStringToLogEntry(string logEntry)
        {
            LogEintrag newLogEntry = new LogEintrag();
            var splitLogContent = logEntry.Split(' '); // Splitte die Zeichenfolge am Leerzeichen
            Regex rx = new Regex("\"(.*?)\"");

            for (int i = 0; i < splitLogContent.Length; i++)
            {
                var value = rx.Match(splitLogContent[i]).Groups[1].Value;

                /*
                if (i == 0)
                {
                    var datetimePart = logEntry.Substring(0, 19); // Die ersten 19 Zeichen für Datum und Uhrzeit
                    var year = datetimePart.Substring(0, 4);
                    var month = datetimePart.Substring(5, 2);
                    var day = datetimePart.Substring(8, 2);
                    var hour = datetimePart.Substring(11, 2);
                    var minute = datetimePart.Substring(14, 2);
                    var second = datetimePart.Substring(17, 2);
                }
                */
                
                if (splitLogContent[i].Contains("id")){
                    newLogEntry.ID = Convert.ToInt32(value);
                }
                
                if (splitLogContent[i].Contains("severity")){
                    newLogEntry.Severity = value;
                }
                
                if (splitLogContent[i].Contains("sys")){
                    newLogEntry.Sys = value;
                }

                if (splitLogContent[i].Contains("sub")){                   
                    newLogEntry.Sub = value;
                }
               
                if (splitLogContent[i].Contains("name")){
                    newLogEntry.Name = value;
                }
                
                if (splitLogContent[i].Contains("action")){
                    newLogEntry.Action = value;
                }
                
                if (splitLogContent[i].Contains("fwrule")){
                    newLogEntry.FWRule = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("initf")){
                    newLogEntry.Initf = value;
                }

                if (splitLogContent[i].Contains("srcmac")){
                    newLogEntry.SourceMAC = value;
                }
                
                if (splitLogContent[i].Contains("dstmac")){
                    newLogEntry.DestinationMAC = value;
                }
                
                if (splitLogContent[i].Contains("srcip")){
                    newLogEntry.SourceIP = value;
                }

                if (splitLogContent[i].Contains("dstip")){
                    newLogEntry.DestinationIP = value;
                }

                if (splitLogContent[i].Contains("proto")){
                    newLogEntry.Protocol = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("length")){
                    newLogEntry.Length = Convert.ToInt32(value);
                }
                
                if (splitLogContent[i].Contains("tos")){
                    newLogEntry.Tos = value;
                }

                if (splitLogContent[i].Contains("prec")){
                    newLogEntry.Prec = value;
                }

                if (splitLogContent[i].Contains("ttl")){
                    newLogEntry.TTL = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("srcport")){
                    newLogEntry.SourcePort = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("dstport")){
                    newLogEntry.DestinationPort = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("tcpflags")){
                    newLogEntry.TCPFlags = value;
                }
            }
            return newLogEntry;
        }
    }
}