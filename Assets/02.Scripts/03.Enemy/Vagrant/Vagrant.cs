using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vagrant : MonoBehaviour
{
    enum States
    {
        None,
        Spawn,
        Idle,
    }

    public static readonly int hashSpeed = Animator.StringToHash("Speed");
    public static readonly int hashAttack = Animator.StringToHash("Attack");
    public static readonly int hashStorm = Animator.StringToHash("Storm");

    private Stats stats;
    private Animator animator;
    private bool isSpawnEnd = false;
    private States state = States.Spawn;

    private Transform player;
    private Vector3 anchorPos;    
    private void Awake()
    {
        stats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }
    private void SpawnEnd()
    {
        isSpawnEnd = true;
        state = States.Idle;
        anchorPos = transform.position;
    }
    private void Update()
    {
        switch (state)
        {      
            case States.Spawn:
                break;
            case States.Idle:
                UpdateIdle();
                break;           
        }

    }
    private void UpdateIdle()
    {        
        var pos = anchorPos;
        pos.y += Mathf.Cos(Time.time);        
        
        transform.position = pos;
    }
    public void EnterIdle()
    {

    }
    public void Storm()
    {

    }
    public void Nova()
    {

    }
    public void Bomb()
    {

    }
}
