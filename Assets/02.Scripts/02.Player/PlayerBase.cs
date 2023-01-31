using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
public class PlayerBase : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject runarrow;

    Rigidbody rb;

    Stats stats;
    private Vector2 direction;
    private float speed;
    public float turnSpeed = 15f;

    Animator animator;
    public static readonly int hashHorizontal = Animator.StringToHash("Horizontal");
    public static readonly int hashVertical = Animator.StringToHash("Vertical");
    public static readonly int hashIsSprint = Animator.StringToHash("IsSprint");
    public static readonly int hashIsFalling = Animator.StringToHash("IsFalling");
    public static readonly int hashIsGround = Animator.StringToHash("IsGround");
    public static readonly int hashJump = Animator.StringToHash("Jump");
    public static readonly int hashIsAttack = Animator.StringToHash("IsAttack");
    public static readonly int hashAttackSpeed = Animator.StringToHash("AttackSpeed");
    private Vector2 inputAxis;
    Vector3 characterRotationY = Vector3.zero;

    public float jumpForce = 10f;
    private bool isGround = true;

    private bool isSprint = false;
    public bool IsSprint { get { return isSprint; } }

    public CinemachineVirtualCamera vCam;
    public float normalFov = 50f;
    public float runFov = 60f;
    public float fovChangeSpeed = 5f;
    private Coroutine coFovChange;

    public AttackDef attackDef;
    protected GameObject attackTarget;
    protected Vector3 hitpos;
    public GameObject AttackTarget { get { return attackTarget; } }
    float baseAttackSpeed;
    float attackSpeedScale = 1f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        
        animator = GetComponent<Animator>();             
        stats = GetComponent<Stats>();
        speed = stats.Speed;
        baseAttackSpeed = attackDef.speed;
        animator.SetFloat(hashAttackSpeed, baseAttackSpeed * attackSpeedScale);
    }
    private void Update()
    {
        UpdateAnimation();
        UpdateRotation();        
        TryJump();
        TrySprint();
        IsFalling();
        TryAttack();
    }
    private void FixedUpdate()
    {
        UpdateMove();        
    }
    private void UpdateAnimation()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        animator.SetFloat(hashHorizontal, inputAxis.x);
        animator.SetFloat(hashVertical, inputAxis.y);
    }
    private void UpdateMove()
    {   
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        Vector3 velocity = (transform.right * direction.x + transform.forward * direction.y).normalized * speed;  
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }
    private void UpdateRotation()
    {        
        float yRotation = Input.GetAxis("Mouse X");
        characterRotationY.y = yRotation * turnSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(characterRotationY));
    } 
    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }        
    }
    private void Jump()
    {
        rb.velocity = transform.up * jumpForce;
        animator.SetTrigger(hashJump);
    }
    private void OnTriggerStay()
    {        
        animator.SetBool(hashIsGround, isGround = true);      
    } 
    private void OnTriggerExit()
    {        
        animator.SetBool(hashIsGround, isGround = false);        
    } 
    private void TrySprint()
    {
        if (Input.GetAxisRaw("Vertical") <= 0 && isSprint)
        {
            StopSprint();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(isSprint)
            {
                StopSprint();
                return;
            }
            Sprint();
        }
    }
    private void Sprint()
    {
        animator.SetBool(hashIsSprint, isSprint = true);
        speed *= 1.5f;

        ChangeSprintState();
    }
    public void StopSprint()
    {
        animator.SetBool(hashIsSprint, isSprint = false);
        speed = stats.Speed;

        ChangeSprintState();
    }
    private void IsFalling()
    {
        if (isGround)
            return;

        animator.SetBool(hashIsFalling, rb.velocity.y <= 0f);
    }
    private void ChangeSprintState()
    {
        SwitchMiddleUi();
        if (coFovChange != null)
        {
            StopCoroutine(coFovChange);
        }
        coFovChange = StartCoroutine(CoChangeFov());
    }
    private void SwitchMiddleUi()
    {        
        crosshair.SetActive(!isSprint);
        runarrow.SetActive(isSprint);
    }
    private IEnumerator CoChangeFov()
    {
        float time = 0f;
        float min = isSprint ? normalFov : runFov;
        float max = isSprint ? runFov : normalFov;
        while(time < 1)
        {
            vCam.m_Lens.FieldOfView = Mathf.Lerp(min, max, time += Time.deltaTime * fovChangeSpeed);
            yield return null;
        }
        yield break;
    }
    private void TryAttack()
    {
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopAttack();
        }
    }
    private void Attack()
    {
        animator.SetBool(hashIsAttack, true);
        if (IsSprint)
        {
            StopSprint();
        }
    }
    private void StopAttack()
    {
        animator.SetBool(hashIsAttack, false);
        attackTarget = null;
    }
    public virtual void Hit()
    {
        if (attackTarget == null)
        {            
            return;
        }
        attackDef.ExecuteAttack(gameObject, attackTarget, hitpos);
    }
    public void SetTarget(GameObject go, Vector3 hitpos)
    {  
        attackTarget = go;
        this.hitpos = hitpos;
    }
}
