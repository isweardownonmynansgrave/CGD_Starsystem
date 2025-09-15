using UnityEngine;

[ExecuteAlways]
public class SphereScaler : MonoBehaviour
{
    [Header("Realer Radius (km)")]
    public float realRadiusKm = 6371f;

    float unitScaleKm;
    float radiusBoost;

    #region MonoBehaviour
    void Update()
    {
        // Werte updaten, falls Veränderung vorgenommen wurde
        unitScaleKm = GameManager.SphereScaler_UnitScaleKm;
        radiusBoost = GameManager.SphereScaler_RadiusBoost;

        float radiusUnits = realRadiusKm / unitScaleKm;
        float boosted = radiusUnits * radiusBoost;
        float diameter = boosted * 2f;
        transform.localScale = new Vector3(diameter, diameter, diameter);
    }
    #endregion
}
