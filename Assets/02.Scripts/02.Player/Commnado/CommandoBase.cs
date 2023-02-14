using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoBase : PlayerBase
{
    public TargetCatcher catcher;

    public Transform muzzleLeft;
    public Transform muzzleRight;
    public ParticleSystem muzzleflash;

    private ParticleSystem leftMuzzleflash;
    private ParticleSystem rightMuzzleflash;

    private CommandoHitEffect hitEffect;
    private Slide slide;

    private void Start()
    {
        SetMuzzleFlash();
        hitEffect = GetComponent<CommandoHitEffect>();
        slide = GetComponent<Slide>();
    }
    public override void TryJump()
    {
        if (slide.IsSliding)
            return; 

        base.TryJump();
    }

    private void SetMuzzleFlash()
    {
        leftMuzzleflash = Instantiate(muzzleflash, muzzleLeft);
        rightMuzzleflash = Instantiate(muzzleflash, muzzleRight);        
    }
    public override void Hit()
    {
        leftMuzzleflash.Play();

        CommonHit(muzzleLeft);
    }
    public void HitRight()
    {
        rightMuzzleflash.Play();

        CommonHit(muzzleRight); 
    } 
    private void CommonHit(Transform muzzle)
    {
        catcher.GetTarget();

        var dir = hitpos - muzzle.position;
        if (Vector3.Dot(muzzle.forward, dir) < 0)
            return;

        hitEffect.MakeHitEffect(gameObject, hitpos);
        base.Hit();
    }
}
