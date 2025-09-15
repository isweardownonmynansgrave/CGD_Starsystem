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

    

    
}
