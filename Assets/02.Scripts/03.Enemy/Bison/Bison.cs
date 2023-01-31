using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bison : EnemyBase
{
    public AttackDef charge;    
    public float attackRange  = 8f;
    public float chargeMaxRange = 40f;
    public float chargeMinRange = 7f;

    private Rigidbody playerRb;
    public float attackedForceY = 3f;
    public float forceScale = 2f;
    protected override void Awake()
    {
        base.Awake();
        playerRb = player.gameObject.GetComponent<Rigidbody>();
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
            
            var lookPos = player.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);

            animator.SetTrigger("Attack");
        }
    }
    public void Hit()
    {
        if (distanceToPlayer > attackRange + 2f)
            return;
        
        var forceDir = player.position - transform.position;
        forceDir.Normalize();
        if (Vector3.Dot(transform.forward, forceDir) < 0.5f)
            return;
 
        forceDir.y = attackedForceY;
        playerRb.AddForce(forceDir * forceScale, ForceMode.Impulse);
        baseAttackDef.ExecuteAttack(gameObject, player.gameObject, Vector3.zero);
    }
}
