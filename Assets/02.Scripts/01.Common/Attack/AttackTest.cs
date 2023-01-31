using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, Attack attack, Vector3 hitpos)
    {
        if (attack.IsCritical)
        {
            Debug.Log("Critical Hit!");
        }
        Debug.Log($"{attacker.name} => {gameObject.name} ({attack.Damage})");
    }
}
