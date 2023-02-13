using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInputArea : MonoBehaviour
{
    Canvas canvas;
    RectTransform rectTransform;
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        var canvasRect = canvas.GetComponent<RectTransform>();
        
        Vector2 canvasHalfSize = new Vector2();
        canvasHalfSize.x = canvasRect.sizeDelta.x / 2;
        canvasHalfSize.y = canvasRect.sizeDelta.y;

        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = canvasHalfSize;
    }
}
