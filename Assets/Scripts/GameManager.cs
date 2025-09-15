using System;
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
    [HideInInspector] public static int timer_bound_minute;
    [HideInInspector] public static int timer_bound_stunde;
    [HideInInspector] public static int timer_bound_tag;
    [HideInInspector] public static int timer_bound_jahr;

    // Game-related
    [HideInInspector]
    public GameObject Sun { get; set; }

    // Events
    public Action InitInfosCall { get; set; }

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
        timer_bound_minute = 60;
        timer_bound_stunde = 60;
        timer_bound_tag = 24;
        timer_bound_jahr = 365;

        // Events
        InitInfosCall?.Invoke();
    }

    void Update()
    {
        // Timer
        AddIntervall();
    }
    #endregion

    #region Init
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

    #region Timer
    public void AddIntervall(int _multiplikator = 1) // Möglichkeiten: x1,x100,x1000
    {
        timer_sekunde += (Time.deltaTime * _multiplikator);
        if (timer_sekunde >= timer_bound_minute)
        {   // WIP, Multiplikator mitbedenken
            timer_minute += (int)(timer_sekunde / timer_bound_minute);
            timer_sekunde = timer_sekunde % timer_bound_minute;
        }
        if (timer_minute >= timer_bound_stunde)
        {
            timer_stunde += (int)(timer_minute / timer_bound_stunde);
            timer_minute = timer_minute % timer_bound_stunde;
        }
        if (timer_stunde >= timer_bound_tag)
        {
            timer_tage += (int)(timer_stunde / timer_bound_tag);
            timer_stunde = timer_stunde % timer_bound_tag;
        }
        if (timer_tage >= timer_bound_jahr)
        {
            timer_jahre += (int)(timer_tage / timer_bound_jahr);
            timer_tage = timer_tage % timer_bound_jahr;
        }
    }
    #endregion
}
