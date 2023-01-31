using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class AttackedDamageUI : MonoBehaviour, IAttackable
{
    public GameObject damageUI;
    
    private DamageUiObjPool pools;
    private IObjectPool<DamageUI> normalPool;
    private IObjectPool<DamageUI> critPool;    

    private void Start()
    {        
        pools = damageUI.GetComponent<DamageUiObjPool>();
        normalPool = pools.NormalPool;
        critPool = pools.CritPool;
    }
    public void OnAttack(GameObject attacker, Attack attack, Vector3 hitpos)
    {
        var dmgUI = attack.IsCritical ? critPool.Get() : normalPool.Get();
        dmgUI.SetUI(attack.Damage, hitpos);
    }
}
