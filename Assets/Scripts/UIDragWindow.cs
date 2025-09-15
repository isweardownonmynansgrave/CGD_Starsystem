using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            Vector3 newPos = localPoint - offset;
            rectTransform.localPosition = ClampToCanvas(rectTransform, newPos, canvas);
        }
    }

    private Vector3 ClampToCanvas(RectTransform window, Vector3 targetPos, Canvas canvas)
    {
        RectTransform canvasRect = canvas.transform as RectTransform;

        // Fenster- und Canvasgröße
        Vector2 windowSize = window.rect.size;
        Vector2 canvasSize = canvasRect.rect.size;

        // Berechnung der Min/Max-Werte
        float minX = -canvasSize.x / 2 + windowSize.x / 2;
        float maxX =  canvasSize.x / 2 - windowSize.x / 2;
        float minY = -canvasSize.y / 2 + windowSize.y / 2;
        float maxY =  canvasSize.y / 2 - windowSize.y / 2;

        // Clamp anwenden
        float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPos.y, minY, maxY);

        return new Vector3(clampedX, clampedY, targetPos.z);
    }
}
