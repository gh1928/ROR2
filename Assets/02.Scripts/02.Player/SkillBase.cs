using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBase : MonoBehaviour
{
    protected Animator animator;
    protected Stats stats;
    protected CommandoBase commando;
    public float coolTime;
    protected float timer = 0f;
    protected bool isOn;    
       
    public RawImage img;
    public Image outLine;    
    protected virtual void Start()
    {
        commando = GetComponent<CommandoBase>();
        animator = GetComponent<Animator>();
        stats = GetComponent<Stats>();        
        InitTimeSet();
    }
    protected virtual void Update()
    {
        CheckCoolDown();
        UpdateUI();       
    }
    protected void CheckCoolDown()
    {
        timer += Time.deltaTime;
        isOn = timer > coolTime;
    }
    protected virtual void Excute()
    {
        
    }
    private void UpdateUI()
    {
        UpdateImg();
        UpdateOutLine();        
    }
    private void UpdateImg()
    {
        var color = Color.white;
        color.a = isOn ? 1f : 0.2f;
        img.color = color;
    } 
    protected void InitTimeSet()
    {
        timer = coolTime;
    }
    private void UpdateOutLine()
    {        
        outLine.fillAmount = timer / coolTime;
    }
    public void TryExcute()
    {
        if (!isOn)
            return;

        Excute();
        timer = 0f;
    }  
}
