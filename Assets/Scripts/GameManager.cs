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

    // Update is called once per frame
    void Update()
    {
        // Timer
        AddIntervall();
    }

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

    /*
    Nimm dein vorhandenes Kepler-Setup (a, e, i, Ω, ω).
    Erzeuge viele Werte der mittleren Anomalie M von 0 … 2π.
    Löse für jeden M die Kepler-Gleichung → E.
    Berechne die wahre Anomalie ν und den Radius r.
    Transformiere in den Weltkoordinaten-Raum wie im Update().
    Pack die Punkte in ein Vector3[].
    */
}
