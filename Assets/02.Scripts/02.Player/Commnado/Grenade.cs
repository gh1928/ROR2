using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : SkillBase
{
    public static readonly int hashGrenade = Animator.StringToHash("Grenade");
    public GameObject grenade;
    
    public Transform rightHand;
    public float throwStr = 5f;
    
    protected override void Start()
    {
        base.Start();        
        grenade = Instantiate(grenade);
        grenade.SetActive(false);        
    }   
    protected override void Excute()
    {
        base.Excute();
        animator.SetTrigger(hashGrenade);
    }
    public void Throw()
    {
        grenade.transform.position = rightHand.transform.position;
        grenade.SetActive(true);               
        grenade.GetComponent<Rigidbody>().velocity = 
            Camera.main.transform.forward * (throwStr + stats.Speed);
    }
}
