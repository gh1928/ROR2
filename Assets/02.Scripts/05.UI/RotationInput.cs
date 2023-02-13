using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationInput : MonoBehaviour, IPointerDownHandler
{
    public static int ID { get; private set; } = -1;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ID != -1)
            return;

        var lastIndex = Input.touchCount - 1;
        ID = Input.touches[lastIndex].fingerId;        
    }
    private void Update()
    {
        CheckFingerUp();
    }
    private void CheckFingerUp()
    {
        var index = FingerIdMatching();
        Debug.Log(index);
        if (index == -1)
        {
            ID = -1;
            return;
        }
    }
 
    public static int FingerIdMatching()
    {        
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.touches[i].fingerId == ID)
                return i;
        }
        return -1;
    }
}
