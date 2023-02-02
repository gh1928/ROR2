using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    public static readonly int hashDie = Animator.StringToHash("Die");
    Animator animator;
    private Stats stats;    
    IDestructible[] destructibles;
    bool isAlive = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<Stats>();        
    }
    private void Start()
    {
        destructibles = GetComponent<DestructibleCollector>().Destructibles;
    }
    private void OnEnable()
    {
        isAlive = true;
    }
    public void OnAttack(GameObject attacker, Attack attack, Vector3 hitpos)
    {
        if (!isAlive)
            return;

        if (stats == null)
            return;

        stats.Health -= attack.Damage;        

        if (stats.Health <= 0)
        {
            isAlive = false;
            stats.Health = 0;
            animator.SetTrigger(hashDie);
        }
    }
    public void Die()
    {
        foreach (var destructible in destructibles)
        {
            destructible.OnDestruction();
        }
    }  
}
