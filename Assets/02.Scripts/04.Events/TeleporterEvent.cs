using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TeleporterEvent : MonoBehaviour
{
    private Transform player;
    private float distanceToPlayer;
    public GameObject textMesh;
    public float detectionRange = 4f;
    bool isPlayerInRange;
    public GameObject boss;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;        
    }
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer < detectionRange;

        textMesh.SetActive(isPlayerInRange);
        TryStartEvent();
    }
    private void TryStartEvent()
    {
        if(isPlayerInRange &&
            Input.GetKeyDown(KeyCode.E))
        {
            StartEvent();
        }
    }
    private void StartEvent()
    {
        boss.SetActive(true);
    }
}
