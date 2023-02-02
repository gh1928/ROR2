using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bison : EnemyBase
{
    public AttackDef charge;    
    public float attackRange  = 8f;
    public float chargeMaxRange = 40f;
    public float chargeMinRange = 20f;
    private float chargeTimer;
    bool isCharging = false;
    bool isAttacking = false;
    private float normalSpeed;
    private float normalAngularSpeed;
    private float normalAccel;
    public float chargeSpeed = 7f;
    public float chargeAngularSpeed = 10f;
    public float chargeAccel = 80f;
    public float attackTurnSpeed = 20f;

    private Rigidbody playerRb;
    public float attackedForceY = 3f;
    public float forceScale = 2f;
    public SphereCollider sphereCollider;

    public static readonly int hashCharge = Animator.StringToHash("Charge");
    public static readonly int hashStopCharge = Animator.StringToHash("StopCharge");
    protected override void Awake()
    {
        base.Awake();
        playerRb = player.gameObject.GetComponent<Rigidbody>();
        sphereCollider.enabled = false;
        
        normalSpeed = agent.speed;        
        normalAngularSpeed = agent.angularSpeed;
        normalAccel = agent.acceleration;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        chargeTimer = charge.speed;
    }
    protected override void Update()
    {
        base.Update();
        TryCharge();        
    }

    private void TryCharge()
    {
        chargeTimer += Time.deltaTime;

        if (distanceToPlayer < chargeMaxRange &&
            distanceToPlayer > chargeMinRange &&            
            chargeTimer > charge.speed) 
        {            
            chargeTimer = 0f;
            isCharging = true;
            PrepareCharge();
        }   
    }
    private void PrepareCharge()
    {
        animator.SetTrigger(hashCharge);
        agent.isStopped = true;        
    }   
    public void StartCharge()
    {        
        agent.isStopped = false;
        agent.speed = chargeSpeed;
        agent.angularSpeed = chargeAngularSpeed;
        agent.acceleration = chargeAccel;
        
        AttackStart();
        Invoke("EndCharge", charge.speed * 0.5f);
    }
    private void EndCharge()
    {
        if (!isCharging)
            return;
        agent.speed = normalSpeed;
        agent.angularSpeed = normalAngularSpeed;
        agent.acceleration = normalAccel;
        isCharging = false;
        AttackEnd();
        State = States.Attack;
        timer = 0f;
        animator.SetTrigger(hashStopCharge);
    }
    protected override void UpdateChase()
    {        
        base.UpdateChase();
        if (distanceToPlayer < attackRange && !isCharging)
        {
            State = States.Attack;
            return;
        }
    }
    protected override void UpdateAttack()
    {
        var dir = player.position - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * attackTurnSpeed);
        base.UpdateAttack();

        if (distanceToPlayer > attackRange &&
            !isAttacking)
        {
            State = States.Chase;
            return;
        }    
        if (timer > baseAttackDef.speed)
        {
            isAttacking = true;
            timer = 0f;
            animator.SetTrigger("Attack");
        }
    }
    public void Hit()
    {        
        var forceDir = player.position - transform.position;
        forceDir.Normalize();
        forceDir.y = attackedForceY;
        float chargeScale = isCharging ? 2f : 1f;
        playerRb.velocity = Vector3.zero;
        playerRb.AddForce(forceDir * forceScale * chargeScale, ForceMode.Impulse);        
        if (isCharging)
            charge.ExecuteAttack(gameObject, player.gameObject, Vector3.zero);
        else
            baseAttackDef.ExecuteAttack(gameObject, player.gameObject, Vector3.zero);

        EndCharge();
    }
    public void AttackStart()
    {
        sphereCollider.enabled = true;
        if(!isCharging)
            agent.isStopped = true;
    }
    public void AttackEnd()
    {
        sphereCollider.enabled = false;
    }
    public void ToChase()
    {
        isAttacking = false;
    }
}
