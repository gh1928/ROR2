using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class TargetCatcher : MonoBehaviour
{    
    public UnityEvent<GameObject, Vector3> OnClickEnemy;    
    public CinemachineVirtualCamera vCam;

    public Transform aimStart;
    public Transform aimDir;
    private Recoil recoil;

    //int enemyMaskValue;
    float maxDistance;
    
    Vector3 recoilDir;
    private void Start()
    {        
        maxDistance = vCam.m_Lens.FarClipPlane;        
        recoil = GetComponent<Recoil>();        
    }
    private void UpdateRecoil()
    {
        if(recoil == null) 
            return;

        recoilDir = aimDir.position + recoil.CurrRecoil();        
    }
    void Update()
    {          
        UpdateRecoil();
    }

    public void GetTarget()
    {
        RaycastHit hit;        

        if (!Physics.Raycast(aimStart.position, recoilDir - aimStart.position, out hit, maxDistance))
            return;

        if (hit.collider.tag == "Enemy")
        {            
            OnClickEnemy.Invoke(hit.transform.root.gameObject, hit.point);
        }
        else
        {
            OnClickEnemy.Invoke(null, hit.point);
        }
    }
}
