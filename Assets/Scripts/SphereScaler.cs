using UnityEngine;

[ExecuteAlways]
public class SphereScaler : MonoBehaviour
{
    [Header("Real Radius (km)")]
    public float realRadiusKm = 6371f; 

    [Header("Scale Settings")]
    public float unitScaleKm = 100000f; // 1 Unity Unit = 100.000 km
    public float radiusBoost = 50f;

    void Update()
    {
        float radiusUnits = realRadiusKm / unitScaleKm;
        float boosted = radiusUnits * radiusBoost;
        float diameter = boosted * 2f;
        transform.localScale = new Vector3(diameter, diameter, diameter);
    }
}
