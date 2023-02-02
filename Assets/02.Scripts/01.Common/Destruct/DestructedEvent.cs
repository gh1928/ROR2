using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedEvent : MonoBehaviour, IDestructible
{     
    public void OnDestruction()
    {
        gameObject.SetActive(false);
    }
}
