using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCollector : MonoBehaviour
{
    IDestructible[] destructibles;
    public IDestructible[] Destructibles { get { return destructibles; } }
    void Awake()
    {
        destructibles = gameObject.GetComponentsInChildren<IDestructible>();
    }
}
