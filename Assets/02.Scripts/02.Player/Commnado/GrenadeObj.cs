using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GrenadeObj : MonoBehaviour
{
    public AOEattack attackDef;
    public ParticleSystem particle;
    public float effectScale = 2f;
    private SphereCollider sphereCollider;
    private GameObject attacker;
    private CinemachineImpulseSource impulseSource;
    private void Start()
    {
        attackDef = Instantiate(attackDef);
        attackDef.DamageScale = 10f;
        particle = Instantiate(particle);
        var main = particle.main;
        main.startSizeMultiplier = effectScale;
        attacker = PlayerBase.Instance.gameObject;
        sphereCollider = GetComponent<SphereCollider>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Invoke("Blast", 0.5f);
        sphereCollider.enabled = false;
    }
    private void Blast()
    {        
        impulseSource.GenerateImpulse(Vector3.up * 0.1f);
        attackDef.Fire(attacker, transform.position);
        sphereCollider.enabled = true;
        gameObject.SetActive(false);
        particle.transform.position = transform.position;
        particle.Play();
    }
}
