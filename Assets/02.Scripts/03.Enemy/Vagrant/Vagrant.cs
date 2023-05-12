using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
    public static readonly int hashStorm = Animator.StringToHash("Storm");
    public static readonly int hashNova = Animator.StringToHash("Nova");
    public static readonly int hashBomb = Animator.StringToHash("Bomb");

    private Animator animator;
    private bool isSpawnEnd = false;
    private States state = States.Spawn;    
    
    private Stats stats;
    private float baseHp;
    private float baseDamage;
    public float MaxHp { get; private set; }
    public float Damage { get; private set; }

    private Transform player;
    private float posTimer = 0f;
    private Vector3 anchorPos;

    public RangeWeapon bomb;
    private RangeWeapon[] bombs;
    public float fireRate = 0.3f;
    int bombIndex = 0;
    public Transform muzzle;
    public float bombCoolDown = 6f;
    private float bombTimer;
    private bool bombOn = false;
    
    public TeleporterEvent telepoter;
    private GameObject hpBar;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        baseHp = stats.Health;
        baseDamage = stats.Damage;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        hpBar = GetComponent<BossHp>().hpBar.gameObject;
    }
    private void SpawnEnd()
    {        
        isSpawnEnd = true;
        state = States.Idle;
        anchorPos = transform.position;
    }
    private void Start()
    {
        UpdateStats();
        stats.Health = MaxHp;
        InitBombSet();
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

        UpdateStats();
        UpdateBomb();
    }
    private void UpdateStats()
    {
        if (GameInfo.Instance == null)
            return;

        var lvl = GameInfo.Instance.GameLevel;
        MaxHp = baseHp + stats.HealthInc * lvl;
        stats.Damage = baseDamage + stats.DamageInc * lvl;

        if(stats.Health <= 0)
        {
            BossClear();
        }            
    }

    private void BossClear()
    {
        hpBar.SetActive(false);
        gameObject.SetActive(false);
        telepoter.ClearBoss();
    }

    private void UpdateIdle()
    {
        posTimer += Time.deltaTime;

        var pos = anchorPos;
        pos.y += Mathf.Cos(posTimer);        
        
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
    private void InitBombSet()
    {
        bombs = new RangeWeapon[4];
        for(int i = 0; i < bombs.Length; i++)
        {
            bombs[i] = Instantiate(bomb);
            bombs[i].muzzle = muzzle;
            bombs[i].DamageScale = 4f;
        }        
        bombTimer = bombCoolDown/2;
    }
    private void UpdateBomb()
    {
        CheckBombCool();
        TryBomb();
    }
    private void CheckBombCool()
    {
        bombTimer -= Time.deltaTime;
        bombOn = bombTimer < 0f;
    }
    private void TryBomb()
    {
        if (bombOn)
            ExcuteBomb();
    }
    private void ExcuteBomb()
    {
        animator.SetTrigger(hashBomb);
    }
    public void Bomb()
    {      

        for(int i = 0; i < bombs.Length; ++i)
        {
            Invoke(nameof(ShotBomb), i * fireRate);
        }        
        bombTimer = bombCoolDown;
    }
    private void ShotBomb()
    {
        var index = bombIndex++ % 4;
        bombs[index].ExecuteAttack(gameObject, player.gameObject, Vector3.zero);
    }
}
