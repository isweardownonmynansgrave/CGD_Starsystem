using System;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
#pragma warning disable IDE0052 // "Value never used"-Warnung abstellen
    // Gravity Well
    private double gravityWell_LOWERBOUND;
#pragma warning restore IDE0052
    
    // SOI
    static double soi_FORMELEXPONENT;
    #region Mono
    // Wird MONO-BEHAVIOUR überhaupt benötigt?
    void Awake()
    {
        // FIXED DATA INIT
        gravityWell_LOWERBOUND = 1 * 10e-6d;
        soi_FORMELEXPONENT = 2d / 5d;
    }
    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region SphereOfInfluence-Mechanic
    private bool InitSOI() // WIP
    {
        bool b = false;

        // TBD??

        return b;
    }

    /// <summary>
    /// Berechnet den Sphere-of-Influence Radius, für den gegebenen Körper, gegenüber dem Zentralobjekt.
    /// </summary>
    /// <param name="_subordinateObj">Zielobjekt, dessen SoI berechnet gegenüber dem Zentralobjekt, berechnet wird.</param>
    /// <param name="_zentralObj">Das Zentralobjekt, das die lokale Gravitation dominiert (z.B. Stern, Schwarzes Loch).</param>
    /// <returns><double> SoI-Radius in km.</returns>
    public static double GetRsoi(GameObject _subordinateObj, GameObject _zentralObj)
    {
        Himmelskoerper _subordinate;
        Himmelskoerper _zentral;

        if (_subordinateObj.TryGetComponent(out _subordinate) &&
             _zentralObj.TryGetComponent(out _zentral))
        {
            double masseVerhaeltnis = _subordinate.Masse / _zentral.Masse;
            double result = GetDistanceByCalc(_subordinateObj, _zentralObj) * Math.Pow(masseVerhaeltnis, soi_FORMELEXPONENT);
            return result;
        }
        else
        {
            return 0;
        }

    }
    #endregion

    #region Gravity-Well
    /// <summary>
    /// Gibt den GravityWell-Radius eines Himmelskörpers als <double> zurück.
    /// </summary>
    /// <param name="_masse1">Die Masse des XY Objektes.</param>
    /// <param name="_masse2">Die Masse des XY Objektes.</param>
    /// <param name="_g">?</param>
    public static double GetGravityWellRadius(double _masse1, double _masse2, double _g)
    {
        return Math.Sqrt((_masse1 * _masse2) / _g);
    }
    #endregion

    #region Distanzberechnung
    /// <summary>
    /// Gibt einen <double> zurück, der die Distanz zwischen 2 Objekten darstellt (Unityscale/Realscale??). Mathematisch berechnet.
    /// </summary>
    /// <param name="_obj1"></param>
    /// <param name="_obj2"></param>
    /// <returns></returns>
    public static double GetDistanceByCalc(GameObject _obj1, GameObject _obj2) // Platzhalter für Tatsächliche Mechanik zur Berechnung des Abstands
    {
        Vector3 pos1 = _obj1.transform.position;
        Vector3 pos2 = _obj2.transform.position;
        return Math.Sqrt(
        Math.Pow(pos2.x - pos1.x, 2) +
        Math.Pow(pos2.y - pos1.y, 2) +
        Math.Pow(pos2.z - pos1.z, 2)
    );
    }
    /// <summary>
    /// Gibt einen <double> zurück, der die Distanz zwischen 2 Objekten darstellt (Unityscale/Realscale??). Via Vectoren-Methode von UnityEngine.Vector3.
    /// </summary>
    /// <param name="_subordinatePos"></param>
    /// <param name="_parentPos"></param>
    /// <returns></returns>
    public double GetDistanceByVector(Vector3 _subordinatePos, Vector3 _parentPos)
        => Vector3.Distance(_subordinatePos, _parentPos);
    #endregion
}
