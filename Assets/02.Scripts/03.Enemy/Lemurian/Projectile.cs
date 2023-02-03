using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool isEffectInstanced = false;
    public float speed = 30f;
    private Rigidbody rb;
    public float lifetime = 8f;
    public Attack Attack { get; set; }
    public ParticleSystem hitEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("BackToPool", lifetime);
    }
    public void Fire(Vector3 pos)
    {
        transform.LookAt(pos);
        rb.velocity = transform.forward * speed;
    } 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var attackables = other.GetComponent<AttackableCollector>().Attackables;

            foreach (var attackable in attackables)
            {
                attackable.OnAttack(gameObject, Attack, Vector3.zero);
            }            
        }
        HitEffect();
        BackToPool();
    }
    private void HitEffect()
    {        
        if(!isEffectInstanced)
        {
            hitEffect = Instantiate(hitEffect);
            isEffectInstanced = true;
        }        
        hitEffect.transform.position = transform.position;
        hitEffect.Play();        
    }
    public void BackToPool()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
