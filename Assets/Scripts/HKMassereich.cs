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

    #endregion

    public void Init(bool _soi = true)
    {
        HasSOI = _soi;
    }

    #region KeplerOrbit-Rendering
    public void InitLineRenderer(int _arrayLaenge)
    {
        //OrbitRenderer.Color
        OrbitRenderer.positionCount = _arrayLaenge;
        OrbitRenderer.SetPositions(OrbitKoordinaten);
        //OrbitRenderer.loop = true; // Erst Orbit visuell anschauen und dann entscheiden
        OrbitRenderer.useWorldSpace = false; // Nötig, falls der Orbit mit dem Objekt mitwandern soll
    }
    #endregion
}
