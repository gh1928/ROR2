using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class TargetCatcher : MonoBehaviour
{    
    public UnityEvent<GameObject, Vector3> OnClickEnemy;    
    public CinemachineVirtualCamera vCam;
    private Recoil recoil;

    //int enemyMaskValue;
    float maxDistance;
    
    Vector3 viewCenter;
    Vector3 aimResult;
    private void Start()
    {        
        maxDistance = vCam.m_Lens.FarClipPlane;
        viewCenter = new Vector3(0.5f, 0.5f, 0f);
        recoil = GetComponent<Recoil>();
        aimResult = viewCenter;
    }
    private void UpdateRecoil()
    {
        if(recoil== null) 
            return;

        aimResult = viewCenter + recoil.CurrRecoil();
        
    }
    void Update()
    {  
        RaycastHit hit;
        UpdateRecoil();

        if (Input.GetMouseButton(0))
        {
            if(!Physics.Raycast(Camera.main.ViewportPointToRay(aimResult), out hit, maxDistance))
                return;

            Debug.Log(recoil.CurrRecoil().x);

            if (hit.collider.tag == "Enemy")
            {                
                OnClickEnemy.Invoke(hit.collider.transform.root.gameObject, hit.point);                
            }
            else
            {
                OnClickEnemy.Invoke(null, hit.point);
            }            
        }   
    }
}
