using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class CommandoHitEffect : MonoBehaviour
{
    public GameObject go;
    private HitEffectObjPool poolObj;
    private IObjectPool<HitEffect> hitEffectPool;
    private void Start()
    {
        poolObj = go.GetComponent<HitEffectObjPool>();
        hitEffectPool = poolObj.hitEffectPool;
    }
    public void MakeHitEffect(GameObject attacker, Vector3 hitpos)
    {
        var hitEffect = hitEffectPool.Get();
        var hitNormal = attacker.transform.position - hitpos;

        hitEffect.Init(hitpos, hitNormal);
    }
}
