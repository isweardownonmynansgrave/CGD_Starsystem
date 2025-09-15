using System;
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

    // Game-related
    [HideInInspector]
    public GameObject Sun { get; set; }

    #region Mono
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
    #endregion

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
        try
        {
            HKMassereich hk = _obj.GetComponent<HKMassereich>();
            KeplerOrbit orbit = _obj.GetComponent<KeplerOrbit>();
            LineRenderer renderer = _obj.GetComponent<LineRenderer>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        


        // Werte der mittleren Anomalie M erzeugt, Gleichung gelöst, Array zurückgegeben
        hk.OrbitKoordinaten = GenerateOrbitPoints(orbit, _anzahlKoordinaten);

        
    }
    public Vector3[] GenerateOrbitPoints(KeplerOrbit _target, int numPoints)
    {
        Vector3[] points = new Vector3[numPoints];
        Transform sun = Sun.transform; // Mittelpunkt der elliptischen Bahn, um die der HK kreist

        // Rotation vorbereiten
        Quaternion Rz_Omega = Quaternion.AngleAxis(_target.OmegaDeg, Vector3.up);
        Quaternion Rx_i     = Quaternion.AngleAxis(_target.iDeg, Vector3.right);
        Quaternion Rz_omega = Quaternion.AngleAxis(_target.omegaDeg, Vector3.up);
        Quaternion Q = Rz_Omega * Rx_i * Rz_omega;

        for (int j = 0; j < numPoints; j++)
        {
            float Mj = (j / (float)numPoints) * 2f * Mathf.PI;
            float Ej = KeplerOrbit.SolveEccentricAnomaly(Mj, _target.e);

            float cosE = Mathf.Cos(Ej);
            float sinE = Mathf.Sin(Ej);
            float r = _target.a * (1 - _target.e * cosE);
            float nu = Mathf.Atan2(Mathf.Sqrt(1 - _target.e * _target.e) * sinE, cosE - _target.e);

            Vector3 rPQW = new Vector3(r * Mathf.Cos(nu), 0f, r * Mathf.Sin(nu));
            points[j] = sun != null ? sun.position + Q * rPQW : Q * rPQW;
        }

        return points;
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
