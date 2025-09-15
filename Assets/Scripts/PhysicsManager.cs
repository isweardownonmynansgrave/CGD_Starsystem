using System;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    // Unity Skybox-Asset
    // assetstore.unity.com/packages/package/92717
    // Singleton
    public static PhysicsManager Instance;
    private static double GRAVITATIONSKONSTANTE;
    // Gravity Well
    private double gravityWell_LOWERBOUND;

    // SOI
    static double soi_FORMEL_EXPONENT;
    static double soi_AVERAGE_ANGULAR_DISTANCE;
    #region MonoBehaviour
    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        // FIXED DATA INIT
        GRAVITATIONSKONSTANTE = 6.674d * 10e-11d;
        gravityWell_LOWERBOUND = 1 * 10e-6d;
        soi_FORMEL_EXPONENT = 2d / 5d;
        soi_AVERAGE_ANGULAR_DISTANCE = 0.9431d;
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
    /// <param name="_subordinateObj">Zielobjekt, dessen SoI gegenüber dem Zentralobjekt, berechnet wird.</param>
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
            double baseFormulaResult = GetDistanceByCalc(_subordinateObj, _zentralObj) * Math.Pow(masseVerhaeltnis, soi_FORMEL_EXPONENT);
            return soi_AVERAGE_ANGULAR_DISTANCE * baseFormulaResult;
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
    public static double GetGravityWellRadius(double _masse1, double _masse2, double _schwellenwert)
    {
        return Math.Sqrt(
            GRAVITATIONSKONSTANTE * ((_masse1 * _masse2) / _schwellenwert)
        );
    }
    #endregion

    #region Formelsammlung
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

    public static double BerechneGewichtskraftInNewton(double _masseInKg, double _radiusInMeter)
    {
        double g = GRAVITATIONSKONSTANTE * (_masseInKg / Math.Pow(_radiusInMeter, 2));
        return g;
    }
    // KeplerOrbit
    public static Vector3[] GenerateOrbitPoints(KeplerOrbit _target, Transform _zentralObjekt, int numPoints) // Access via InitKelperOrbit()
    {
        Vector3[] points = new Vector3[numPoints];

        // Rotation vorbereiten
        Quaternion Rz_Omega = Quaternion.AngleAxis(_target.OmegaDeg, Vector3.up);
        Quaternion Rx_i = Quaternion.AngleAxis(_target.iDeg, Vector3.right);
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
            points[j] = _zentralObjekt != null ? _zentralObjekt.position + Q * rPQW : Q * rPQW;
        }

        return points;
    }
    #endregion
}
