using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlast : SkillBase
{
    public static readonly int hashPhase = Animator.StringToHash("Phase");
    public Transform leftMuzzle;
    public Transform rightMuzzle;
    public ParticleSystem particle;
    private ParticleSystem leftParticle;
    private ParticleSystem rightParticle;    
    
    public PhaseBullet prefab;
    private PhaseBullet[] instances;

    protected override void Start()
    {
        base.Start();
        coolTime = 3f;        

        SetMuzzleFlash();
        SetBullets();
    } 
    private void SetMuzzleFlash()
    {
        leftParticle = Instantiate(particle, leftMuzzle.transform);        
        rightParticle = Instantiate(particle, rightMuzzle.transform);
    }
    private void SetBullets()
    {
        instances = new PhaseBullet[8];

        for (int i = 0; i < instances.Length; i++)
        {
            instances[i] = Instantiate(prefab, transform);
            instances[i].gameObject.SetActive(false);
        }
    }
    protected override void Excute()
    {
        base.Excute();
        animator.SetTrigger(hashPhase);
    }
    private void PlayParticles()
    {
        leftParticle.Play();
        rightParticle.Play();
    }
    public void Blast()
    {
        PlayParticles();

        for (int i = 0; i < instances.Length / 2; i++)
        {
            instances[i].transform.position = leftMuzzle.position;            
            instances[i].FireBlast(Camera.main.transform.forward, stats.Speed);
            instances[i + 4].transform.position = rightMuzzle.position;
            instances[i + 4].FireBlast(Camera.main.transform.forward, stats.Speed);
        }
    } 
}
