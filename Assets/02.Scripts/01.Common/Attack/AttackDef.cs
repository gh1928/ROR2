using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
public class AttackDef : ScriptableObject
{
    public float speed;
    public float DamageScale { get; set; } = 1f;
    public virtual void ExecuteAttack(GameObject attacker, GameObject defender, Vector3 hitpos)
    {
        var aStats = attacker.GetComponent<Stats>();
        var dStats = defender.GetComponent<Stats>();
        var attack = CreatAttack(aStats, dStats);
        var attackables = defender.GetComponent<AttackableCollector>().Attackables;

        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack, hitpos);
        }
    }
    protected Attack CreatAttack(Stats attacker, Stats defender)
    {
        float damage = attacker.Damage * DamageScale * 100 / (100 + defender.Armor);        

        var isCritical = Random.value < attacker.CriticalChance;
        if (isCritical)
        {
            damage *= attacker.CriticalMultiplayer;
        }
 
        return new Attack((int)damage, isCritical);
    }
}
