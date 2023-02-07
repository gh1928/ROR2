using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEattack : AttackDef
{
    public float radius = 5f;

    public GameObject effectPrefab;    
    

    public void Fire(GameObject attacker, Vector3 position)
    {
        if (attacker == null)
            return;

        var cols = Physics.OverlapSphere(position, radius);
        foreach (var col in cols)
        {
            //if (attacker == col.gameObject)
            //    continue;

            //var defender = col.gameObject;

            //var aStats = attacker.GetComponent<CharacterStats>();
            //var dStats = defender.GetComponent<CharacterStats>();
            //var attack = CreatAttack(aStats, dStats);
            //var attackables = defender.GetComponentsInChildren<IAttackable>();

            //foreach (var attackable in attackables)
            //{
            //    attackable.OnAttack(attacker, attack);
            //}
        }
    }
}
