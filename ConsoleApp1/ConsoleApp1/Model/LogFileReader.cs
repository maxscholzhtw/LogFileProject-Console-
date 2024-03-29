using System.Text.RegularExpressions;

namespace ConsoleApp1.Model
{
    public class Format
    {
        public string IPAdresse;
        public int Count;
    }

    public class LogFileReader
    {
        public List<LogEintrag> LogEintraege { get; } = new List<LogEintrag>();

        public string logFilePath { get; }
        public string FileContent { get; private set; } // nullable deklarieren
        public string[] SplitContent { get; private set; } // nullable deklarieren
        
        public LogFileReader(string logFilePath)
        {
            this.logFilePath = logFilePath;
            if (File.Exists(logFilePath))
            {
                Console.WriteLine("");
                Console.WriteLine("Pfad gefunden!");
                Console.WriteLine("");
            }
            else
            {
                throw new Exception($"Die Datei im Pfad:{logFilePath} wurde nicht gefunden.");
            }
        }

        public void ReadContent()
        {
            try
            {
                FileContent = File.ReadAllText(logFilePath);
                Console.WriteLine("Die Datei wurde erfolgreich eingelesen!");
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
                Console.WriteLine("Der Dateninhalt wurde erfolgreich aufgeteilt!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Aufteilen des Dateiinhalts: {ex.Message}");
            }
        }

        public List<Format> PrintIPFrequency()
        {
            List<Format> _listipFrequency = new List<Format>();
            
            var ipFrequency = LogEintraege.GroupBy(x => x.SourceIP)
                .Select(g => new { IP = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);
            
            foreach (var item in ipFrequency)
            {
                _listipFrequency.Add(new Format() {IPAdresse = item.IP, Count = item.Count});
                Console.WriteLine($"IP-Adresse: {item.IP}, Häufigkeit: {item.Count}");
            }
            return _listipFrequency;
        }
        
        public void StartConvertToLogEntry()
        {
            try
            {
                foreach (var entry in SplitContent)
                {
                    LogEintraege.Add(ConvertStringToLogEntry(entry));
                }
                Console.WriteLine("Die Datei wurde erfolgreich in Log-Einträge konvertiert!");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Konvertieren des Dateiinhalts in Log-Einträge: {ex.Message}");
                Console.WriteLine("");
            }
        }
        
        public string SaveToCSV(string csvFilePath)
        {
            try
            {
                // Kombiniere das Verzeichnis und den Dateinamen zu einem vollständigen Pfad
                string fullPath = Path.Combine(csvFilePath, "Blocked_IPs.csv");

                // Schreibe die CSV-Daten in die Datei
                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    var list = PrintIPFrequency();
                    foreach (var item in list)
                    {
                        writer.WriteLine($"{item.IPAdresse}; {item.Count}");
                    }
                }
                
                Console.WriteLine("");
                Console.WriteLine($"Die CSV-Datei wurde erfolgreich erstellt! Pfad: {csvFilePath}");
                return fullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Ausgabe in die CSV-Datei: {ex.Message}");
                Console.WriteLine("");
            }

            return csvFilePath;
        }

        public LogEintrag ConvertStringToLogEntry(string logEntry)
        {
            LogEintrag newLogEntry = new LogEintrag();
            var splitLogContent = logEntry.Split(' '); // Splitte die Zeichenfolge am Leerzeichen
            Regex rx = new Regex("\"(.*?)\"");

            for (int i = 0; i < splitLogContent.Length; i++)
            {
                var value = rx.Match(splitLogContent[i]).Groups[1].Value;

                if (splitLogContent[i].Contains("id"))
                {
                    newLogEntry.ID = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("severity"))
                {
                    newLogEntry.Severity = value;
                }

                if (splitLogContent[i].Contains("sys"))
                {
                    newLogEntry.Sys = value;
                }

                if (splitLogContent[i].Contains("sub"))
                {
                    newLogEntry.Sub = value;
                }

                if (splitLogContent[i].Contains("name"))
                {
                    newLogEntry.Name = value;
                }

                if (splitLogContent[i].Contains("action"))
                {
                    newLogEntry.Action = value;
                }

                if (splitLogContent[i].Contains("fwrule"))
                {
                    newLogEntry.FWRule = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("initf"))
                {
                    newLogEntry.Initf = value;
                }

                if (splitLogContent[i].Contains("srcmac"))
                {
                    newLogEntry.SourceMAC = value;
                }

                if (splitLogContent[i].Contains("dstmac"))
                {
                    newLogEntry.DestinationMAC = value;
                }

                if (splitLogContent[i].Contains("srcip"))
                {
                    newLogEntry.SourceIP = value;
                }

                if (splitLogContent[i].Contains("dstip"))
                {
                    newLogEntry.DestinationIP = value;
                }

                if (splitLogContent[i].Contains("proto"))
                {
                    newLogEntry.Protocol = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("length"))
                {
                    newLogEntry.Length = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("tos"))
                {
                    newLogEntry.Tos = value;
                }

                if (splitLogContent[i].Contains("prec"))
                {
                    newLogEntry.Prec = value;
                }

                if (splitLogContent[i].Contains("ttl"))
                {
                    newLogEntry.TTL = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("srcport"))
                {
                    newLogEntry.SourcePort = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("dstport"))
                {
                    newLogEntry.DestinationPort = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("tcpflags"))
                {
                    newLogEntry.TCPFlags = value;
                }
            }
            return newLogEntry;
        }
    }
}