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
        Floting,
    }

    public static readonly int hashSpeed = Animator.StringToHash("Speed");
    public static readonly int hashAttack = Animator.StringToHash("Attack");

    private Stats stats;
    private Animator animator;
    private bool isSpawnEnd = false;
    private States state = States.Spawn;

    private Transform player;
    private Vector3 startPos;    
    private void Awake()
    {
        stats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }
    private void SpawnEnd()
    {
        isSpawnEnd = true;
        state = States.Floting;
        startPos = transform.position;
    }
    private void Update()
    {
        switch (state)
        {      
            case States.Spawn:
                break;
            case States.Floting:
                UpdateFloating();
                break;           
        }

    }
    private void UpdateFloating()
    {        
        var pos = startPos;
        pos.y += Mathf.Cos(Time.time);        
        
        transform.position = pos;
    }
}
