using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOE.asset", menuName = "Attack/AOE")]
public class AOEattack : AttackDef
{
    public float radius = 5f;    
    public void Fire(GameObject attacker, Vector3 position)
    {
        var cols = Physics.OverlapSphere(position, radius);
        foreach (var col in cols)
        {
            if (!col.CompareTag("Enemy"))
                continue;

            var defender = col.transform.root.gameObject;

            var aStats = attacker.GetComponent<Stats>();
            var dStats = defender.GetComponent<Stats>();
            var attack = CreatAttack(aStats, dStats);
            var attackables = defender.GetComponent<AttackableCollector>().Attackables;

            foreach (var attackable in attackables)
            {
                attackable.OnAttack(attacker, attack, defender.transform.position);
            }
        }
    }
}
