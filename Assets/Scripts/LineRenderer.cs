using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    [Header("Points for the Line")]
    public Vector3[] points;

    private LineRenderer lineRenderer;

    void Awake()
    {
        // LineRenderer vom GameObject holen
        lineRenderer = GetComponent<LineRenderer>();
    }
    // Objekt anlegen, dem Objekt ein point Array hinzufügen, ERST DANACH instanziieren
    void Start()
    {
        DrawLine(points);
    }

    /// <summary>
    /// Zeichnet eine Linie durch die angegebenen Punkte
    /// </summary>
    /// <param name="linePoints">Array mit Positionen</param>
    public void DrawLine(Vector3[] linePoints)
    {
        if (linePoints == null || linePoints.Length == 0)
            return;

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
}
