using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody rb;
    public Attack Attack { get; set; }   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("BackToPool", 5f);        
    }
    private void OnEnable()
    {
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
        BackToPool();
    }
    public void BackToPool()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
