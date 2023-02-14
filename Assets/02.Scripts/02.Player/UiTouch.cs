using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UiTouch : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent unityEvent;
    public void OnPointerDown(PointerEventData eventData)
    {
        unityEvent.Invoke();
    }
}
