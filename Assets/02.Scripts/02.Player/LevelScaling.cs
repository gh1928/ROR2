using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScaling : MonoBehaviour
{
    private Stats stats;
    private PlayerLevel level;
    public float MaxHealth { get; private set; }
    private float baseHealth;
    private float baseDamage;
    private float baseRegen;
    private float regenTimer = 0f;
    private int currLevel = 1;
    void Start()
    {
        stats = GetComponent<Stats>();
        baseHealth = stats.Health;
        baseDamage = stats.Damage;
        baseRegen = stats.HealthRegen;
        level = GetComponent<PlayerLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();        
        if(CheckLevelUp())
        {            
            stats.Health += stats.HealthInc;           
        }
        HealthRegen();
    }
    private void UpdateStats()
    {
        MaxHealth = baseHealth + stats.HealthInc * (level.Level - 1);
        stats.Damage = baseDamage + stats.DamageInc * (level.Level - 1);
        stats.HealthRegen = baseRegen + stats.HealthRegenInc * (level.Level - 1);
    }
    private void HealthRegen()
    {
        regenTimer += Time.deltaTime;
        if(regenTimer > 1f)
        {
            stats.Health += stats.HealthRegen;
            if(stats.Health > MaxHealth)
            {
                stats.Health = MaxHealth;
            }
            regenTimer = 0f;
        }
    }

    private bool CheckLevelUp()
    {
        var lvl = level.Level;
        if (currLevel == lvl)
            return false;
        
        currLevel = lvl;
        return true;
    }
}
