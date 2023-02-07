using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCustomGravity : MonoBehaviour
{
    float globalGravity;
    public float gravityScale = 1.0f;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        globalGravity = GameInfo.Instance.globalGravity;
    }
    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.down;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
