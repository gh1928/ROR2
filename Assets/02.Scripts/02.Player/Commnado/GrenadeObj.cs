using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeObj : MonoBehaviour
{
    public AttackDef attackDef;
    public ParticleSystem particle;
    private void Start()
    {
        attackDef = Instantiate(attackDef);
        particle = Instantiate(particle);
    }
    private void OnTriggerEnter(Collider other)
    {
        Invoke("Blast", 1f);
    }
    private void Blast()
    {

    }
}
