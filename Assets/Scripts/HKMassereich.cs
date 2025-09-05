using UnityEngine;

public class HKMassereich : Himmelskoerper
{
    #region Instanzvariablen
    // Sphere of Influence
    public bool HasSOI { get; set; }

    #endregion

    public void Init(bool _soi = false)
    {
        HasSOI = _soi;
    }

    

    
}
