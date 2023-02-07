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
       
    public Image coolDownImg;
    public RawImage outLine;
    public TextMeshProUGUI coolNumTex;
    protected virtual void Start()
    {
        commando = GetComponent<CommandoBase>();
        animator = GetComponent<Animator>();
        stats = GetComponent<Stats>();
    }
    protected virtual void Update()
    {
        CheckCoolDown();
        UpdateUI();
        TryExcute();
    }
    protected void CheckCoolDown()
    {
        timer -= Time.deltaTime;
        isOn = timer < 0f;
    }
    protected virtual void TryExcute()
    {
        if (!isOn)
            return;

        if(Trigger())
        {
            Excute();
            timer = coolTime;            
        }
    }
    protected virtual bool Trigger()
    {
        return false;
    }
    protected virtual void Excute()
    {
        
    }
    private void UpdateUI()
    {
        UpdateImg();
        UpdateOutLine();
        UpdateCooldownText();
    }
    private void UpdateImg()
    {
        coolDownImg.fillAmount = timer / coolTime;        
    } 
    private void UpdateOutLine()
    {
        outLine.gameObject.SetActive(isOn);
    }
    private void UpdateCooldownText()
    {
        coolNumTex.gameObject.SetActive(!isOn);
        coolNumTex.text = ((int)Mathf.Clamp(timer + 1, 1, coolTime + 1)).ToString();
    }
}
