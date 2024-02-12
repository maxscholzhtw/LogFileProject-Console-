using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlazorApp1.Components.Models
{
    public class LogFileReader
    {
        private List<LogEintrag> LogEintraege = new List<LogEintrag>();

        public string FilePath { get; private set; }
        public string FileContent { get; private set; }
        public string[] SplitContent { get; private set; }

        public LogFileReader(string path)
        {
            if (File.Exists(path))
            {
                this.FilePath = path;
                Console.WriteLine("Beliebige Taste drücken, um Ergebnisse auszugeben");
            }
            else
            {
                throw new Exception($"Die Datei im Pfad:{path} wurde nicht gefunden.");
            }
        }

        public void Counter()
        {
            int wordCount = FileContent.Count(c => c == ' ') + 1;
            Console.WriteLine($"Anzahl der Wörter in der Datei: {wordCount}");
        }

        public void ReadContent()
        {
            FileContent = File.ReadAllText(this.FilePath);
        }

        public void SplitFileContent()
        {
            SplitContent = this.FileContent.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
        
        private void PrintIPFrequency()
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
            foreach (var entry in SplitContent)
            {
                LogEintrag response = ConvertStringToLogEntry(entry);
                this.LogEintraege.Add(response);
            }

            PrintIPFrequency();
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
                        string line = $"{entry.Date},{entry.ID},{entry.Severity},{entry.Sys},{entry.Sub},{entry.Name},{entry.Action},{entry.FWRule},{entry.Initf},{entry.SourceMAC},{entry.DestinationMAC},{entry.SourceIP},{entry.DestinationIP},{entry.Protocol},{entry.Length},{entry.Tos},{entry.Prec},{entry.TTL},{entry.SourcePort},{entry.DestinationPort},{entry.TCPFlags}";
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
            var splitLogContent = logEntry.Split(" ");
            Regex rx = new Regex("\"(.*?)\"");

            for (int i = 0; i < splitLogContent.Length; i++)
            {
                var value = rx.Match(splitLogContent[i]).Groups[1].Value;
                if (i == 0)
                {
                    var date = splitLogContent[i];
                    DateTime createDate = DateTime.Now;
                    var year = date.Substring(0, 10);
                    var time = date.Substring(11, 8);
                    year = year.Replace(':', '/');
                    newLogEntry.Date = Convert.ToDateTime(DateTime.Parse($"{year} {time}"));
                }

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