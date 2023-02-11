using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform point;
    private RectTransform back;
    public float radius = 100f;
    private Vector2 originalPoint;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        back = GetComponent<RectTransform>();
        originalPoint = point.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetAxis(string axis)
    {
        switch (axis)
        {
            case "Horizontal":
                return direction.x;
            case "Vertical":
                return direction.y;
        }
        return 0f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var newPos = eventData.position - back.anchoredPosition;
        var delta = Vector3.ClampMagnitude(newPos - originalPoint, radius);
        point.anchoredPosition = newPos;

        direction = delta / radius;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        point.anchoredPosition = Vector2.zero;
    }

}
