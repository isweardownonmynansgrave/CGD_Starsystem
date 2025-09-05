using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    // Gravity Well
    private double gravityWell_lowerBound = 1 * 10e-6d;

    // SOI
    double soi_formelExponent = 2d / 5d;
    #region Mono
    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region SphereOfInfluence-Mechanic
    private bool InitSOI()
    {
        bool b = false;



        return b;
    }
    public static double GetRsoi(GameObject _subordinateObj, GameObject _zentralObj)
    {
        if (_obj1 is Himmelskoerper && _obj2 is Himmelskoerper)
        {
            double result = Math.Pow(GetDistanceByCalc(_subordinateObj, _zentralObj) // Abstand zum Zentralobjekt, z.B. Sonne für Erde, Erde für Mond etc.
                         * (_subordinateObj.Masse / _zentralObj.Masse), soi_formelExponent);
            return result;
        }
        else
        {
            throw new Exception("Objekte sind nicht vom Typ Himmelskoerper(||inherit)");
        }

    }
    #endregion

    #region Gravity-Well
    public static double GetGravityWellRadius(double _masse1, double _masse2, double _g)
    {
        return Math.Sqrt((_masse1 * _masse2) / _g);
    }
    #endregion

    #region Distanzberechnung
    public double GetDistanceByCalc(GameObject _obj1, GameObject _obj2) // Platzhalter für Tatsächliche Mechanik zur Berechnung des Abstands
    {
        Vector3 pos1 = _obj1.Transform.position;
        Vector3 pos2 = _obj2.Transform.position;
        return Math.Sqrt(
        Math.Pow(pos2.x - pos1.x, 2) +
        Math.Pow(pos2.y - pos1.y, 2) +
        Math.Pow(pos2.z - pos1.z, 2)
    );
    }
    public double GetDistanceByVector(Vector3 _subordinatePos, Vector3 _parentPos)
        => Vector3.Distance(_subordinatePos, _parentPos);
    #endregion
}
