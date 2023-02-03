using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBiteCollider : MonoBehaviour
{
    SphereCollider sphereCollider;
    public Lemurian lemurian;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }  
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            lemurian.Hit();
            sphereCollider.enabled = false;
        }
    }
}
