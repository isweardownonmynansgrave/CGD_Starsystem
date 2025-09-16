using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;

    // Timer
    [HideInInspector] public int    timer_stunde;
    [HideInInspector] public int    timer_minute;
    [HideInInspector] public float  timer_sekunde;
    [HideInInspector] public int    timer_tage;
    [HideInInspector] public int    timer_jahre;
    [HideInInspector] public static int timer_bound_sekundenProMinute;
    [HideInInspector] public static int timer_bound_MinutenProStunde;
    [HideInInspector] public static int timer_bound_stundenProTag;
    [HideInInspector] public static int timer_bound_tageProJahr;

    // Game-related
    [HideInInspector]
    public GameObject Sun { get; set; }

    // Info-System
    public static string info_Dateipfad = "hkinfo.txt";
    public Dictionary<string, HKInfo> Infos { get; private set; }

    // SphereScaler-Sync
    public static float SphereScaler_UnitScaleKm = 100000f; // 1 Unity Unit = 100.000 km
    public static float SphereScaler_RadiusBoost = 1f;

    #region Mono
    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        // Timer Vars, bei Savegame den Stand aus Datei laden & setzen
        timer_stunde = 0;
        timer_minute = 0;
        timer_stunde = 0;

        // Erden-Jahr als Zeitachsen-Schwellenwerte
        timer_bound_sekundenProMinute = 60;
        timer_bound_MinutenProStunde = 60;
        timer_bound_stundenProTag = 24;
        timer_bound_tageProJahr = 365;   

    }

    void Update()
    {
        // Timer
        AddIntervall();
    }
    #endregion

    #region Init
    #region Init-Orbits
    /* Ablauf-Kalkulation
    1. Nimm dein vorhandenes Kepler-Setup (a, e, i, Ω, ω).
    2. Erzeuge viele Werte der mittleren Anomalie M von 0 … 2π.
    3. Löse für jeden M die Kepler-Gleichung → E.
    4. Berechne die wahre Anomalie ν und den Radius r.
    5. Transformiere in den Weltkoordinaten-Raum wie im Update().
    6. Pack die Punkte in ein Vector3[].
    */
    // Initialisieren der Laufbahnen
    public static void InitKeplerOrbit(GameObject _obj, GameObject _zentralObj, int _anzahlKoordinaten = 128)
    {
        HKMassereich hk = null;
        KeplerOrbit kepler = null;
        try
        {
            hk = _obj.GetComponent<HKMassereich>();
            kepler = _obj.GetComponent<KeplerOrbit>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        // Werte der mittleren Anomalie M erzeugt, Gleichung gelöst, Koords-Array zurückgegeben
        hk.OrbitKoordinaten = PhysicsManager.GenerateOrbitPoints(kepler, _zentralObj.transform, _anzahlKoordinaten);

        hk.InitLineRenderer(_anzahlKoordinaten);
    }
    #endregion
    #region Init-Infos
    public static Dictionary<string, HKInfo> InfoDictFuellenAusTxt(string _pfad)
    {
        Dictionary<string, HKInfo> tempDict = new();
        StreamReader sr = new StreamReader(_pfad);
        while (!sr.EndOfStream)
        {
            HKInfo temp = new HKInfo();
            bool isObjFinished = false;
            while (!isObjFinished)
            {
                string line = sr.ReadLine();
                string[] split = line.Split(';');
                switch (split[0])
                {
                    case "Name":
                        temp.Name = split[1];
                        break;
                    case "Atmo":
                        temp.AtmosphaerischeZusammensetzung = split[1];
                        break;
                    case "Masse":
                        temp.Masse = Convert.ToDouble(split[1]);
                        break;
                    case "Durchm":
                        temp.DurchmesserInKm = split[1];
                        break;
                    case "Bahn":
                        temp.BahnInfo = split[1];
                        break;
                    case "AnzMo":
                        temp.AnzahlMonde = Convert.ToInt32(split[1]);
                        break;
                    case "Monde":
                        temp.Monde = split[1].Split(',').ToList();
                        break;
                    case "END":
                        isObjFinished = true;
                        break;
                }
            }
            tempDict.Add(temp.Name.ToLower(), temp);
        }
        sr.Close();
        return tempDict;
    }
    #endregion
    #endregion

    #region Timer
    public void AddIntervall(int _multiplikator = 1) // Möglichkeiten: x1,x100,x1000
    {
        timer_sekunde += (Time.deltaTime * _multiplikator);
        if (timer_sekunde >= timer_bound_sekundenProMinute)
        {   // WIP, Multiplikator mitbedenken
            timer_minute += (int)(timer_sekunde / timer_bound_sekundenProMinute);
            timer_sekunde = timer_sekunde % timer_bound_sekundenProMinute;
        }
        if (timer_minute >= timer_bound_MinutenProStunde)
        {
            timer_stunde += (int)(timer_minute / timer_bound_MinutenProStunde);
            timer_minute = timer_minute % timer_bound_MinutenProStunde;
        }
        if (timer_stunde >= timer_bound_stundenProTag)
        {
            timer_tage += (int)(timer_stunde / timer_bound_stundenProTag);
            timer_stunde = timer_stunde % timer_bound_stundenProTag;
        }
        if (timer_tage >= timer_bound_tageProJahr)
        {
            timer_jahre += (int)(timer_tage / timer_bound_tageProJahr);
            timer_tage = timer_tage % timer_bound_tageProJahr;
        }
    }
    #endregion
}
