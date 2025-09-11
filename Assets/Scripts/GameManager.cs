using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;

    // Timer

    [HideInInspector]
    public int timer_stunde;
    [HideInInspector]
    public int timer_minute;
    [HideInInspector]
    public float timer_sekunde;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        timer_stunde = 0;
        timer_minute = 0;
        timer_stunde = 0;
    }

    void Update()
    {
        // Timer
        AddIntervall();
    }

    #region Init
    /* Ablauf-Klakulation
    1. Nimm dein vorhandenes Kepler-Setup (a, e, i, Ω, ω).
    2. Erzeuge viele Werte der mittleren Anomalie M von 0 … 2π.
    3. Löse für jeden M die Kepler-Gleichung → E.
    4. Berechne die wahre Anomalie ν und den Radius r.
    5. Transformiere in den Weltkoordinaten-Raum wie im Update().
    6. Pack die Punkte in ein Vector3[].
    */
    // Initialisieren der Laufbahnen
    public void InitKeplerOrbit(GameObject _obj, int _anzahlKoordinaten = 128)
    {
        Himmelskoerper hk = _obj.GetComponent<Himmelskoerper>();
        KeplerOrbit orbit = _obj.GetComponent<KeplerOrbit>();

        // Werte der mittleren Anomalie M erzeugen, von 0 … 2π.
        float[] werteDerMittlerenAnomalie = new float[_anzahlKoordinaten];
        // TBD - Calc

        // Löse für jeden M die Kepler-Gleichung → E. 
        foreach (float M in werteDerMittlerenAnomalie)
        {
            
        }
    }
    #endregion

    #region Timer
    public void AddIntervall(int _multiplikator = 1) // Möglichkeiten: x1,x100,x1000
    {
        timer_sekunde += (Time.deltaTime * _multiplikator);
        if (timer_sekunde >= 60)
        {
            timer_minute++;
            timer_sekunde -= 60f;
        }
        if (timer_minute >= 60)
        {
            timer_stunde++;
            timer_minute -= 60;
        }
    }
    #endregion
}
