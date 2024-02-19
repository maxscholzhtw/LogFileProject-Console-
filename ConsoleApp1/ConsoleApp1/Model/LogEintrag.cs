public class LogEintrag
{
    public int ID { get; set; }
    public string? Severity { get; set; }
    public string? Sys { get; set; }
    public string? Sub { get; set; }
    public string? Name { get; set; }
    public string? Action { get; set; }
    public int FWRule { get; set; }
    public string? Initf { get; set; }
    public string? SourceMAC { get; set; }
    public string? DestinationMAC { get; set; }
    public string? SourceIP { get; set; }
    public string? DestinationIP { get; set; }
    public int Protocol { get; set; }
    public int Length { get; set; }
    public string? Tos { get; set; }
    public string? Prec { get; set; }
    public int TTL { get; set; }
    public int SourcePort { get; set; }
    public int DestinationPort { get; set; }
    public string? TCPFlags { get; set; }

}
// Path: ConsoleApp1/Model/LogFileReader.cs
