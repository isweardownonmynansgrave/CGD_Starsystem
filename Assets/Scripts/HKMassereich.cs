using UnityEngine;

public class HKMassereich : Himmelskoerper
{
    #region Instanzvariablen
    // Physikalische Werte
    private Vector3[] OrbitKoordinaten;

    // Sphere of Influence
    public bool HasSOI { get; set; }

    #endregion

    public void Init(bool _soi = true)
    {
        HasSOI = _soi;
    }

    

    
}
