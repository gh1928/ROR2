using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class DamageUI : MonoBehaviour
{
    private IObjectPool<DamageUI> pool;    
    TextMeshProUGUI textMeshPro;
    Vector3 pos;

    private float transSpeed = 1f;
    private float lifeTime = 2f;
    
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();        
    }
    private void Start()
    {
        transform.position = pos;
    }
    public void SetPool(IObjectPool<DamageUI> pool)
    {
        this.pool = pool;
        
    }
    private void Update()
    {
        pos.y += transSpeed * Time.deltaTime;
        transform.position = Camera.main.WorldToScreenPoint(pos);

        textMeshPro.alpha = transform.position.z > 0f ? 1f : 0f;
    }
    public void SetUI(int damage, Vector3 hitpos)
    {
        textMeshPro.text = damage.ToString();
        this.pos = hitpos;
        transform.position = Camera.main.WorldToScreenPoint(pos);
        Invoke("Realese", lifeTime);
    }
    private void Realese()
    {
        pool.Release(this);
    }
}
