using System;
using System.Collections.Generic;
using System.IO;
//using Newtonsoft.Json;

[Serializable]
public class HKInfo
{
    // Infos
    public string AtmosphaerischeZusammensetzung { get; set; }
    public double Masse { get; set; }
    public string DurchmesserInKm { get; set; }
    public string BahnInfo { get; set; }
    public int AnzahlMonde { get; set; }
    public List<string> Monde { get; set; }

    // Verwaltung
    public string Name { get; set; }

    public HKInfo()
    {
        //TBD
    }
    /*
    public static HKInfo LadeInfoAusDatei(string dateiPfad, string name)
    {
        if (!File.Exists(dateiPfad))
            throw new FileNotFoundException("Datei nicht gefunden", dateiPfad);

        string json = File.ReadAllText(dateiPfad);

        // Dictionary mit Planet-Namen als Keys
        var planeten = JsonConvert.DeserializeObject<Dictionary<string, Planet>>(json);

        if (planeten != null && planeten.ContainsKey(name))
        {
            return planeten[name];
        }

        throw new ArgumentException($"Planet '{name}' nicht in JSON gefunden.");
    }
    */
}
