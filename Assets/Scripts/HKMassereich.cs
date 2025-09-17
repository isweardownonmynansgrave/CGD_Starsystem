using UnityEngine;

public class HKMassereich : Himmelskoerper
{
    #region Instanzvariablen
    // Physikalische Werte

    // KeplerOrbit
    public Vector3[] OrbitKoordinaten { get; set; }
    public LineRenderer OrbitRenderer { get; set; }

    // Sphere of Influence
    public bool HasSOI { get; set; }

    // Verwaltung
    protected GameManager gm = GameManager.Instance;

    #endregion

    public void Init(bool _soi = true)
    {
        HasSOI = _soi;
        gm = GameManager.Instance;
        OrbitRenderer = gameObject.GetComponent<LineRenderer>();

        // KeplerOrbit
        GameManager.InitKeplerOrbit(gameObject, gm.Sun);
        transform.position = OrbitKoordinaten[0];
    }

    #region KeplerOrbit-Rendering
    public void InitLineRenderer(int _arrayLaenge)
    {
        //OrbitRenderer.Color
        OrbitRenderer.positionCount = _arrayLaenge;
        OrbitRenderer.SetPositions(OrbitKoordinaten);
        OrbitRenderer.startWidth = 0.1f;
        OrbitRenderer.loop = true; // Anfangs- und Endpunkt verbinden
        OrbitRenderer.useWorldSpace = true; // false Nötig, falls der Orbit mit dem Objekt mitwandern soll
        Debug.Log(OrbitKoordinaten[0]);
    }
    #endregion
}
