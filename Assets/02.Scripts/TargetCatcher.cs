using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class TargetCatcher : MonoBehaviour
{    
    public UnityEvent<GameObject, Vector3> OnClickEnemy;    
    public CinemachineVirtualCamera vCam;    

    //int enemyMaskValue;
    float maxDistance;
    
    Vector3 viewCenter;    
    private void Start()
    {        
        maxDistance = vCam.m_Lens.FarClipPlane;
        viewCenter = new Vector3(0.5f, 0.5f, 0f);
    }
    void Update()
    {  
        RaycastHit hit;

        if(Input.GetMouseButton(0))
        {
            if(!Physics.Raycast(Camera.main.ViewportPointToRay(viewCenter), out hit, maxDistance))
                return;
            
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
