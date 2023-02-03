using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemurian : EnemyBase
{
    public static readonly int hashFire = Animator.StringToHash("Fire");
    LayerMask mask;

    public AttackDef fireBall;
    public float fireMaxRange = 20f;
    public float fireMinRange = 10f;
    private float fireTimer;

    public float attackRange = 1f;
    public SphereCollider sphereCollider;
    public Transform muzzle;        
    public ParticleSystem hitEffect;
    protected override void Awake()
    {
        base.Awake();        
        sphereCollider.enabled = false;
        fireTimer = fireBall.speed;
        mask = LayerMask.GetMask("Player");

        fireBall = Instantiate(fireBall, transform);
        var fb = fireBall as RangeWeapon;
        fb.muzzle = muzzle;
        fb.isInstanced = false;
        hitEffect = Instantiate(hitEffect);
        hitEffect.transform.localScale = Vector3.one * 0.1f;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        hitEffect.Stop();
    }
    protected override void Update()
    {
        base.Update();        
        TryFire();
    }

    private void TryFire()
    {
        fireTimer += Time.deltaTime;

        if (distanceToPlayer < fireMaxRange &&
            distanceToPlayer > fireMinRange &&
            fireTimer > fireBall.speed &&
            Physics.Raycast(transform.position, player.position - transform.position, fireMaxRange, mask))
        {
            fireTimer = 0f;
            chargingFire();
        }        
    }

    protected override void UpdateChase()
    {
        base.UpdateChase();
        if (distanceToPlayer < attackRange)
        {            
            State = States.Attack;
            return;
        }
    }
    protected override void UpdateAttack()
    {
        var dir = player.position - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.8f);

        base.UpdateAttack();
        if (distanceToPlayer > attackRange)
        {
            State = States.Chase;
            return;
        }
        if (timer > baseAttackDef.speed)
        {
            timer = 0f;       
            animator.SetTrigger("Attack");
        }
    }
    public void Hit()
    {
        var effectPos = player.position;
        effectPos.y += 0.5f;

        hitEffect.transform.position = effectPos;
        hitEffect.Play();
        baseAttackDef.ExecuteAttack(gameObject, player.gameObject, Vector3.zero);       
    }
    public void AttackStart()
    {
        sphereCollider.enabled = true;
    }
    public void AttackEnd()
    {
        sphereCollider.enabled = false;        
    } 
    public void chargingFire()
    {
        agent.isStopped = true;
        transform.LookAt(player.position);
        animator.SetTrigger(hashFire);
    }
    public void Fire()
    {
        fireBall.ExecuteAttack(gameObject, player.gameObject, Vector3.zero);
    }
    public void Move()
    {
        agent.isStopped = false;
    }
    public override void DieTriggered()
    {
        base.DieTriggered();
        AttackEnd();
    }
}
