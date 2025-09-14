using System.Collections.Generic;

public class HKInfo
{
    public string AtmosphaerischeZusammensetzung { get; set; }
    public double Masse { get; set; }
    public string DurchmesserInKm { get; set; }
    public string BahnInfo { get; set; }
    public int AnzahlMonde { get; set; }
    public List<string> Monde { get; set; }

    public HKInfo()
    {
        //TBD
    }
}
