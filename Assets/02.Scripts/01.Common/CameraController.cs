using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraPivot;
    public float cameraRotationLimit = 40;
    private float currentCameraRotationX = -40;    
    public float turnSpeed = 15f;

    Vector3 cameraRotation;

    public static readonly int hashAimY = Animator.StringToHash("AimY");
    Animator animator;    
    private void Start()
    {
        animator = GetComponent<Animator>();        
    }
    void Update()
    {        
        CameraXRotation();
        UpdateAnimation();
    }
    private void CameraXRotation()
    {
        //float xRotation = Input.GetAxis("Mouse Y");
        
        if (RotationInput.ID == -1)
            return;

        var index = RotationInput.FingerIdMatching();
        if (index == -1)
            return;

        float xRotation = Input.touches[index].deltaPosition.y / Screen.height * 70f;

        float cameraRotationX = xRotation * turnSpeed;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        cameraRotation.x = currentCameraRotationX;
        cameraPivot.transform.localEulerAngles = cameraRotation;
    }
    private void UpdateAnimation()
    {
        animator.SetFloat(hashAimY, (1 - currentCameraRotationX / cameraRotationLimit ) * 0.45f);
    }    
}
