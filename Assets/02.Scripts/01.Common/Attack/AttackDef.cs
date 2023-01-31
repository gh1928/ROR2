using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
public class AttackDef : ScriptableObject
{
    public float speed;        
    public float criticalChance;
    public float ciriticalMultiplayer;
    public virtual void ExecuteAttack(GameObject attacker, GameObject defender, Vector3 hitpos)
    {
    }
    public Attack CreatAttack(Stats attacker, Stats defender)
    {
        float damage = attacker.Damage * 100 / (100 + defender.Armor);        

        var isCritical = Random.value < criticalChance;
        if (isCritical)
        {
            damage *= ciriticalMultiplayer;
        }
 
        return new Attack((int)damage, isCritical);
    }
}
