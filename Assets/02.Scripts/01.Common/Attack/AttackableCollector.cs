using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableCollector : MonoBehaviour
{
    IAttackable[] attackables;
    public IAttackable[] Attackables { get { return attackables; } }
    void Awake()
    {
        attackables = gameObject.GetComponentsInChildren<IAttackable>();
    } 
}
