using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBullet : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private CapsuleCollider capsuleCollider;
    public Attack PhaseAttack { get; set; }
    public float velocity = 20f;
    public float maxSpread = 5f;
    public float lifeTime = 3f;

    public ParticleSystem particle;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        particle = Instantiate(particle, transform);
    }    
    public void FireBlast(Vector3 look, float playerSpeed)
    {
        InitSet();
        var rand = Random.Range(-maxSpread, maxSpread);        
        
        Vector3 dir = look + Vector3.one * rand;
        rb.velocity = dir * (velocity + playerSpeed);        
    }
    private void InitSet()
    {   
        gameObject.SetActive(true);
        meshRenderer.enabled = true;
        capsuleCollider.enabled = true;
        Invoke("StopActive", lifeTime);
    }
    private void StopActive()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayParticle();
        Stop();
        if (other.CompareTag("Enemy"))
        {
            var root = other.transform.root;
            var attack = CreatAttack(PlayerBase.Instance.Stats, root.GetComponent<Stats>());
            var attackables = root.GetComponent<AttackableCollector>().Attackables;

            foreach (var attackable in attackables)
            {
                attackable.OnAttack(PlayerBase.Instance.gameObject, attack, transform.position);
            }
        }
    }
    private void PlayParticle()
    {
        particle.transform.position = transform.position;
        particle.Play();
    }
    private void Stop()
    {   
        meshRenderer.enabled = false;
        capsuleCollider.enabled = false;
    }

    public Attack CreatAttack(Stats attacker, Stats defender)
    {
        float damage = attacker.Damage * 2 * 100 / (100 + defender.Armor);

        var isCritical = Random.value < attacker.CriticalChance;
        if (isCritical)
        {
            damage *= attacker.CriticalMultiplayer;
        }
        return new Attack((int)damage, isCritical);
    }

}
