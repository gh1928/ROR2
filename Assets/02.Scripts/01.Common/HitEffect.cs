using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitEffect : MonoBehaviour
{    
    private new ParticleSystem particleSystem;
    private IObjectPool<HitEffect> pool;

    public float lifeTime = 0.3f;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    public void SetPool(IObjectPool<HitEffect> pool)
    {
        this.pool = pool;
    }
    public void Init(Vector3 hitpos, Vector3 hitNormal)
    {
        particleSystem.transform.position = hitpos;
        particleSystem.transform.rotation = Quaternion.LookRotation(hitNormal);
        particleSystem.Play();

        Invoke("Realese", lifeTime);
    }
    private void Realese()
    {
        pool.Release(this);
    }

}
