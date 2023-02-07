using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : SkillBase
{  
    public static readonly int hashSlide = Animator.StringToHash("Slide");
    public static readonly int hashSlideEnd = Animator.StringToHash("SlideEnd");
    private float normalSpeed;
    public float speedScale = 3f;    
    private Rigidbody rb;
    public bool IsSliding { get; private set; } = false;

    private bool slidingStart = false;
    private bool slidingEnd = false;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();        
        coolTime = 4f;        
        normalSpeed = commando.NormalSpeed;        
    }
    protected override bool Trigger()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    protected override void Excute()
    {
        base.Excute();
        animator.SetTrigger(hashSlide);       
    }
    //private void FixedUpdate()
    //{
    //    if(slidingStart)
    //    {
    //        ReduceCollider();
    //        slidingStart = false;
    //    }
    //    if(slidingEnd)
    //    {
    //        EnlargeCollider();
    //        slidingEnd = false;
    //    }
    //}
    public void SlideStart()
    {
        stats.Speed = normalSpeed;
        stats.Speed *= speedScale;
        IsSliding = true;
        slidingStart = true;
    }
    public void SlideEnd()
    {
        animator.SetTrigger(hashSlideEnd);        
        stats.Speed = normalSpeed;
        IsSliding = false;

        slidingEnd = true;
    }
    //private void ReduceCollider()
    //{
    //    capsuleCollider.height = 0f;
    //    capsuleCollider.center = Vector3.up * -0.6f;
        
    //    rb.AddForce(Vector3.up * 3, ForceMode.Impulse);
    //}
    //private void EnlargeCollider()
    //{        
    //    capsuleCollider.height = 1.8f;
    //    capsuleCollider.center = Vector3.zero;
    //}
}
