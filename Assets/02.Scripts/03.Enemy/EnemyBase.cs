using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public enum States
    {
        None = -1,
        Spawn,
        Idle,
        Patrol,
        Chase,
        Attack,
        GameOver,
    }

    protected Stats stats;
    protected Animator animator;
    public static readonly int hashSpeed = Animator.StringToHash("Speed");
    public static readonly int hashAttack = Animator.StringToHash("Attack");
    protected Rigidbody rb;
    public AttackDef baseAttackDef;
    public float aggroRange = 200f;
    protected float speed;
    
    protected bool isSpawnEnd = false;

    protected Transform player;
    protected NavMeshAgent agent;

    protected float distanceToPlayer;       

    public float chaseInterval = 0.25f;
    public float idleTime = 2f;
    protected float timer = 0f;
        
    public float patrolRaidus = 30f;
    public float patrolTime = 3f;
    protected float distanceToDest;
    protected Vector3 dest;

    private float baseHp;
    private float baseDamage;
    public float MaxHp { get; private set; }
    public float Damage { get; private set; }
    protected States state = States.None;

    public CapsuleCollider mainCollider;
    public States State
    {
        get { return state; }
        protected set
        {
            var prevState = state;
            state = value;

            if (prevState == state)
                return;

            switch (state)
            {
                case States.Spawn:
                    isSpawnEnd = false;
                    agent.isStopped = true;
                    break;
                case States.Idle:
                    timer = 0f;
                    agent.speed = speed;
                    agent.isStopped = true;
                    break;
                case States.Patrol:
                    timer = 0f;
                    agent.speed = speed;
                    if(GetRandomPoint(transform.position, patrolRaidus, out dest))
                    {
                        agent.SetDestination(dest);
                    }                    
                    agent.isStopped = false;
                    break;
                case States.Chase:
                    timer = 0f;                                        
                    agent.SetDestination(player.position);
                    agent.isStopped = false;
                    break;
                case States.Attack:
                    timer = baseAttackDef.speed;
                    agent.velocity = Vector3.zero;
                    if(gameObject.activeSelf)
                        agent.isStopped = true;
                    break;
                case States.GameOver:
                    agent.isStopped = true;
                    break;
            }
        }
    }
    protected virtual void Awake()
    {
        stats = GetComponent<Stats>();
        baseHp = stats.Health;
        baseDamage = stats.Damage;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        speed = stats.Speed;
        player = GameObject.FindWithTag("Player").transform;
        baseAttackDef = Instantiate(baseAttackDef);
    } 
    protected virtual void OnEnable()
    {
        State = States.Spawn;        
        mainCollider.enabled = true;
        UpdateStats();
        stats.Health = MaxHp;
    }
 
    protected virtual void Update()
    {
        UpdateDistances();
        UpdateStates();       
        animator.SetFloat(hashSpeed, agent.velocity.magnitude);
        UpdateStats();
    }

    private void UpdateStates()
    {
        switch (state)
        {
            case States.Spawn:
                UpdateSpawn();
                break;
            case States.Idle:
                UpdateIdle();
                break;
            case States.Patrol:
                UpdatePatrol();
                break;
            case States.Chase:
                UpdateChase();
                break;
            case States.Attack:
                UpdateAttack();
                break;
        }
    }

    private void UpdateDistances()
    {
        if (State == States.GameOver)
            return;

        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        distanceToDest = Vector3.Distance(transform.position, dest);
    }
    private void UpdateStats()
    {
        if (GameInfo.Instance == null)
            return;

        var lvl = GameInfo.Instance.GameLevel;
        MaxHp = baseHp + stats.HealthInc * lvl;
        stats.Damage = baseDamage + stats.DamageInc * lvl;
    }
    public virtual void DieTriggered()
    {
        mainCollider.enabled = false;       
    }
    protected void OnPlayerDie()
    {
        State = States.GameOver;
    }
    bool GetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    protected void UpdateSpawn()
    {
        if(isSpawnEnd)
        {
            State = States.Patrol;
        }
    }
    protected void SpawnEnd()
    {
        isSpawnEnd = true;
    }
    protected virtual void UpdateAttack()
    {
        timer += Time.deltaTime;
    }
    protected virtual void UpdateChase()
    {
        timer += Time.deltaTime;
 
        if (timer > chaseInterval)
        {
            agent.SetDestination(player.position);
            timer = 0f;
        }
    }
    protected void UpdatePatrol()
    {
        timer += Time.deltaTime;

        if (distanceToPlayer < aggroRange)
        {
            State = States.Chase;
            return;
        }

        if (distanceToDest <= agent.stoppingDistance ||
            timer > patrolTime)
        {
            State = States.Idle;
            return;
        }
    }
    protected void UpdateIdle()
    {
        timer += Time.deltaTime;

        if (distanceToPlayer < aggroRange)
        {
            State = States.Chase;
            return;
        }
        if (timer > idleTime)
        {
            State = States.Patrol;
            return;
        }
    }
}
