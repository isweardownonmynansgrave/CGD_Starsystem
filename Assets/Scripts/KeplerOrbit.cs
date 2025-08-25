using UnityEngine;

public class KeplerOrbit : MonoBehaviour
{
    [Header("Refs")]
    public Transform sun;        // zentraler Fokus (Sonne)
    public Transform planetMesh; // das Kugel-Mesh (uniform scale!)

    [Header("Kepler Elements")]
    public float a = 10f;                 // semi-major axis (units)
    [Range(0f, 0.99f)] public float e;    // eccentricity
    public float iDeg;                    // inclination i
    public float omegaDeg;                // argument of periapsis ω
    public float OmegaDeg;                // longitude of ascending node Ω
    public float periodSeconds = 60f;     // orbital period T
    public float meanAnomalyDegAtEpoch;   // M0 at t=0

    float M; // mean anomaly (rad)

    void Start()
    {
        // Uniform scale safety
        var s = planetMesh.localScale.x;
        planetMesh.localScale = new Vector3(s, s, s);
        M = meanAnomalyDegAtEpoch * Mathf.Deg2Rad;
    }

    void Update()
    {
        if (periodSeconds <= 0f) return;

        // Update mean anomaly
        float n = 2f * Mathf.PI / periodSeconds; // mean motion (rad/s)
        M += n * Time.deltaTime;

        // Solve Kepler's equation: M = E - e sin E (Newton-Raphson)
        float E = SolveEccentricAnomaly(M, e);

        // True anomaly ν and radius r
        float cosE = Mathf.Cos(E);
        float sinE = Mathf.Sin(E);
        float r = a * (1 - e * cosE);
        float nu = Mathf.Atan2(Mathf.Sqrt(1 - e * e) * sinE, cosE - e);

        // Position in orbital plane (PQW frame: x toward periapsis)
        Vector3 rPQW = new Vector3(r * Mathf.Cos(nu), 0f, r * Mathf.Sin(nu));

        // Rotation to inertial frame: Rz(Ω) * Rx(i) * Rz(ω)
        Quaternion Rz_Omega = Quaternion.AngleAxis(OmegaDeg, Vector3.up);
        Quaternion Rx_i     = Quaternion.AngleAxis(iDeg, Vector3.right);
        Quaternion Rz_omega = Quaternion.AngleAxis(omegaDeg, Vector3.up);
        Quaternion Q = Rz_Omega * Rx_i * Rz_omega;

        Vector3 worldPos = sun.position + Q * rPQW;
        planetMesh.position = worldPos;
    }

    float SolveEccentricAnomaly(float M, float e, int iters = 8)
    {
        M = Mathf.Repeat(M, 2f * Mathf.PI); // wrap
        float E = e < 0.8f ? M : Mathf.PI;  // initial guess
        for (int k = 0; k < iters; k++)
        {
            float f = E - e * Mathf.Sin(E) - M;
            float fp = 1f - e * Mathf.Cos(E);
            E -= f / fp;
        }
        return E;
    }
}
