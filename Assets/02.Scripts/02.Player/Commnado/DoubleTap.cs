using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[CreateAssetMenu(fileName = "DoubleTap.asset", menuName = "Attack/DoubleTap")]
public class DoubleTap : AttackDef
{    
    public override void ExecuteAttack(GameObject attacker, GameObject defender, Vector3 hitpos)
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
}
