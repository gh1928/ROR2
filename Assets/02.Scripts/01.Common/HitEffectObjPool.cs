using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitEffectObjPool : MonoBehaviour
{
    public HitEffect hitEffect;
    public IObjectPool<HitEffect> hitEffectPool { get; private set; }
    private void Awake()
    {
        hitEffectPool = new ObjectPool<HitEffect>(
            Create,
            OnGet,
            OnRealese,
            OnDestroyEffect,
            maxSize: 128
            );  
    }
    private HitEffect Create()
    {
        HitEffect effect = Instantiate(hitEffect, transform);
        effect.SetPool(hitEffectPool);
        return effect;
    } 
    private void OnGet(HitEffect effect)
    {
        effect.gameObject.SetActive(true);
    }
    private void OnRealese(HitEffect effect)
    {
        effect.gameObject.SetActive(false);
    }
    private void OnDestroyEffect(HitEffect effect)
    {
        Destroy(effect.gameObject);
    }
}
