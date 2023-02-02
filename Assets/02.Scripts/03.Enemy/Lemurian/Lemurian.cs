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
    private bool isTargetOn = false;
    protected override void Awake()
    {
        base.Awake();        
        sphereCollider.enabled = false;
        fireTimer = fireBall.speed;
        mask = LayerMask.GetMask("Player");
        var fb = fireBall as FireBall;
        fb.muzzle = muzzle;
    }
    protected override void Update()
    {
        base.Update();
        UpdateLookPos();
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
            Fire();
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
        baseAttackDef.ExecuteAttack(gameObject, player.gameObject, Vector3.zero);       
    }
    public void AttackStart()
    {
        sphereCollider.enabled = true;
    }
    public void AttackEnd()
    {
        sphereCollider.enabled = false;
        isTargetOn = false;
    }
    public void LookTarget()
    {        
        isTargetOn = true;
    }
    private void UpdateLookPos()
    {
        if (!isTargetOn)
            return;
     
        transform.LookAt(player.position);
    }
    public void Fire()
    {
        agent.isStopped = true;
        transform.LookAt(player.position);
        animator.SetTrigger(hashFire);
    }
    public void FireEnd()
    {
        fireBall.ExecuteAttack(gameObject, player.gameObject, Vector3.zero);
    }
    public void Move()
    {
        agent.isStopped = false;
    }
}
