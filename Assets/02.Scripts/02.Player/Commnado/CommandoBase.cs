using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoBase : PlayerBase
{
    public GameObject muzzleLeft;
    public GameObject muzzleRight;
    public ParticleSystem muzzleflash;

    private ParticleSystem leftMuzzleflash;
    private ParticleSystem rightMuzzleflash;

    private CommandoHitEffect hitEffect;
    private void Start()
    {
        SetMuzzleFlash();
        hitEffect = GetComponent<CommandoHitEffect>();
    }
    private void SetMuzzleFlash()
    {
        leftMuzzleflash = Instantiate(muzzleflash, muzzleLeft.transform);
        rightMuzzleflash = Instantiate(muzzleflash, muzzleRight.transform);        
    }
    public override void Hit()
    {    
        base.Hit();
        leftMuzzleflash.Play();
        hitEffect.MakeHitEffect(gameObject, hitpos);
    }
    public void HitRight()
    {
        base.Hit();
        rightMuzzleflash.Play();
        hitEffect.MakeHitEffect(gameObject, hitpos);
    } 
}
