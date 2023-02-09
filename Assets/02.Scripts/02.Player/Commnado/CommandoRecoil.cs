using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CommandoRecoil : MonoBehaviour
{
    public Recoil recoil;
    private CinemachineImpulseSource impulseSource;
    public float impulseStrenth = 10f;
    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    public void Hit()
    {
        CameraShake();
    }
    public void HitRight()
    {
        CameraShake();
    }
    private void CameraShake()
    {        
        impulseSource.GenerateImpulse(recoil.CurrRecoil() * impulseStrenth);        
    }

}
