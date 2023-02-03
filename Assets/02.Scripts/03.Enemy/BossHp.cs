using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    public Slider hpBar;    
    private Stats stats;
    private float maxHp;
    private CanvasGroup canvasGroup;
    private Coroutine coroutine;

    public float eventTime = 0.1f;
    private void Awake()
    {        
        stats = GetComponent<Stats>();
        maxHp = stats.Health;
        canvasGroup = hpBar.GetComponent<CanvasGroup>();
    }
    private void Update()
    {       
        hpBar.value = stats.Health / maxHp;
    }
    private void SpawnStart()
    {
        coroutine = StartCoroutine(CoHpAlphaEvent());
    }
    IEnumerator CoHpAlphaEvent()
    {
        float timer = 0f;
        hpBar.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        while (timer < eventTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / eventTime);            
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
