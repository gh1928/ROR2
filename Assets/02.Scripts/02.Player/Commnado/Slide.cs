using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Slide : SkillBase
{  
    public static readonly int hashSlide = Animator.StringToHash("Slide");
    public static readonly int hashSlideEnd = Animator.StringToHash("SlideEnd");
    private float normalSpeed;
    public float speedScale = 3f;        
    public bool IsSliding { get; private set; } = false;
    protected override void Start()
    {
        coolTime = 4f;        
        base.Start();              
        normalSpeed = commando.NormalSpeed;        
    }
    protected override void Excute()
    {
        base.Excute();
        
        animator.SetTrigger(hashSlide);       
    }
    public void SlideStart()
    {
        stats.Speed = normalSpeed;
        stats.Speed *= speedScale;
        IsSliding = true;    
    }
    public void SlideEnd()
    {
        animator.SetTrigger(hashSlideEnd);        
        stats.Speed = normalSpeed;
        IsSliding = false;
    }
}
