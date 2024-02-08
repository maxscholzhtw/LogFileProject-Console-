using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlazorApp1.Components.Models;

public class LogFileReader
    {
        private List<LogEintrag> Logeintraege = new List<LogEintrag>();

        public string Path { get; private set; }
        public string FileContent { get; private set; }
        public string[] SplitContent { get; private set; }

        public LogFileReader(string path)
        {
            if (File.Exists(path))
            {
                this.Path = path;
                Console.WriteLine("Beliebege Taste drücken, um Ergebnisse auszugeben");
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
            FileContent = File.ReadAllText(this.Path);
        }

        public void SplitFileContent()
        {
            SplitContent = this.FileContent.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        public void StarteConvertToLogEintag()
        {
            foreach (var eintrag in SplitContent)
            {
                LogEintrag responese = ConvertStringToLogEintrag(eintrag);
                this.Logeintraege.Add(responese);
            }
        }

        
        public void SaveToCSV(string csvFilePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(csvFilePath))
                {
                    foreach (var eintrag in Logeintraege)
                    {
                        string line = $"{eintrag.date},{eintrag.id},{eintrag.severity},{eintrag.sys},{eintrag.sub},{eintrag.name},{eintrag.action},{eintrag.fwrule},{eintrag.initf},{eintrag.srcmac},{eintrag.dstmac},{eintrag.srcip},{eintrag.dstip},{eintrag.proto},{eintrag.length},{eintrag.tos},{eintrag.prec},{eintrag.ttl},{eintrag.srcport},{eintrag.dstport},{eintrag.tcpflags}";
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine($"Die Ausgabe wurde erfolgreich in die CSV-Datei '{csvFilePath}' gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Ausgabe in die CSV-Datei: {ex.Message}");
            }
        }
        

        private LogEintrag ConvertStringToLogEintrag(string logEintrag)
        {
            LogEintrag newLogEintrag = new LogEintrag();
            var splitLogContent = logEintrag.Split(" ");
            Regex rx = new Regex("\"(.*?)\"");

            for (int i = 0; i < splitLogContent.Length; i++)
            {
                var value = rx.Match(splitLogContent[i]).Groups[1].Value;
                if (i == 0)
                {
                    var date = splitLogContent[i];
                    DateTime createDate = DateTime.Now;
                    var jahr = date.Substring(0, 10);
                    var uhr = date.Substring(11, 8);
                    jahr = jahr.Replace(':', '/');
                    newLogEintrag.date = Convert.ToDateTime(DateTime.Parse($"{jahr} {uhr}"));
                }

                if (splitLogContent[i].Contains("id")){
                    newLogEintrag.id = Convert.ToInt32(value);
                }
                
                if (splitLogContent[i].Contains("severity")){
                    newLogEintrag.severity = value;
                }
                
                if (splitLogContent[i].Contains("sys")){
                    newLogEintrag.sys = value;
                }

                if (splitLogContent[i].Contains("sub")){                   
                    newLogEintrag.sub = value;
                }
               
                if (splitLogContent[i].Contains("name")){
                    newLogEintrag.name = value;
                }
                
                if (splitLogContent[i].Contains("action")){
                    newLogEintrag.action = value;
                }
                
                if (splitLogContent[i].Contains("fwrule")){
                    newLogEintrag.fwrule = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("initf")){
                    newLogEintrag.initf = value;
                }

                if (splitLogContent[i].Contains("srcmac")){
                    newLogEintrag.srcmac = value;
                }
                
                if (splitLogContent[i].Contains("dstmac")){
                    newLogEintrag.dstmac = value;
                }
                
                if (splitLogContent[i].Contains("srcip")){
                    newLogEintrag.srcip = value;
                }

                if (splitLogContent[i].Contains("dstip")){
                    newLogEintrag.dstip = value;
                }

                if (splitLogContent[i].Contains("proto")){
                    newLogEintrag.proto = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("length")){
                    newLogEintrag.length = Convert.ToInt32(value);
                }
                
                if (splitLogContent[i].Contains("tos")){
                    newLogEintrag.tos = value;
                }

                if (splitLogContent[i].Contains("prec")){
                    newLogEintrag.prec = value;
                }

                if (splitLogContent[i].Contains("ttl")){
                    newLogEintrag.ttl = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("srcport")){
                    newLogEintrag.srcport = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("dstport")){
                    newLogEintrag.dstport = Convert.ToInt32(value);
                }

                if (splitLogContent[i].Contains("tcpflags")){
                    newLogEintrag.tcpflags = value;
                }
            }
            return newLogEintrag;
        }

    }