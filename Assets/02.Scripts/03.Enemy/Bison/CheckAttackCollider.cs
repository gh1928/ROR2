using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackCollider : MonoBehaviour
{
    SphereCollider sphereCollider;
    public Bison bison;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bison.Hit();
            sphereCollider.enabled = false;
        }        
    }
}
