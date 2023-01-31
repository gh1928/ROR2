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

    private Stats stats;
    private Animator animator;
    private Rigidbody rb;
    public AttackDef attackDef;
    public float aggroRange = 200f;
    protected float speed;
    public float patrolRaidus = 3f;
    protected bool isSpawnEnd = false;

    private Transform player;
    private NavMeshAgent agent;

    private float distanceToPlayer;       
    private float distanceToDest;
    private Vector3 dest;
    public float chaseInterval = 0.25f;
    public float idleTime = 2f;
    protected float timer = 0f;    

    private States state = States.None;
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
                    agent.speed = speed;
                    NavMeshHit hit;
                    Vector3 randomDirection;
                    do
                    {
                        randomDirection = Random.insideUnitSphere * patrolRaidus;
                        randomDirection += transform.position;                        
                    }
                    while (NavMesh.SamplePosition(randomDirection, out hit, patrolRaidus, 2));
                    dest = hit.position;
                    agent.SetDestination(dest);
                    agent.isStopped = false;
                    break;
                case States.Chase:
                    timer = 0f;                                        
                    agent.SetDestination(player.position);
                    agent.isStopped = false;
                    break;
                case States.Attack:
                    timer = attackDef.speed;            
                    break;
                case States.GameOver:
                    agent.isStopped = true;
                    break;
            }
        }
    }
    private void Awake()
    {
        stats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        speed = stats.Speed;
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        State = States.Spawn;
    }
    private void Update()
    {
        if (State != States.GameOver)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            distanceToDest = Vector3.Distance(transform.position, dest);
        }
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

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
    protected void OnPlayerDie()
    {
        State = States.GameOver;
    }
    protected void UpdateSpawn()
    {
        if(isSpawnEnd)
        {
            State = States.Idle;
        }
    }
    protected void SpawnEnd()
    {
        isSpawnEnd = true;
    }
    protected void UpdateAttack()
    {
        throw new System.NotImplementedException();
    }
    protected void UpdateChase()
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
        if (distanceToPlayer < aggroRange)
        {
            State = States.Chase;
            return;
        }

        if (distanceToDest <= agent.stoppingDistance)
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
