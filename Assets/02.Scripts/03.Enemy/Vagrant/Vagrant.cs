using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vagrant : MonoBehaviour
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

    public static readonly int hashSpeed = Animator.StringToHash("Speed");
    public static readonly int hashAttack = Animator.StringToHash("Attack");

    protected Stats stats;
    protected Animator animator;
    protected bool isSpawnEnd = false;

    States State = States.None;
    protected Transform player;
    protected virtual void Awake()
    {
        stats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    protected void SpawnEnd()
    {
        isSpawnEnd = true;
    }
    private void OnEnable()
    {
        State = States.Spawn;
    }
}
