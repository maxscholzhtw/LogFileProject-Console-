namespace BlazorApp1.Components.Models;

public class LogEintrag
{
    public DateTime date { get; set; }
    public int id { get; set; }

    public string severity { get; set; }

    public string sys { get; set; }

    public string sub { get; set; }

    public string name { get; set; }

    public string action { get; set; }

    public int fwrule { get; set; }

    public string initf { get; set; }

    public string srcmac { get; set; }

    public string dstmac { get; set; }

    public string srcip { get; set; }

    public string dstip { get; set; }

    public int proto { get; set; }

    public int length { get; set; }

    public string tos { get; set; }

    public string prec { get; set; }

    public int ttl { get; set; }

    public int srcport { get; set; }

    public int dstport { get; set; }

    public string tcpflags { get; set; }

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
    public LogEintrag()
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
    {


    }
}