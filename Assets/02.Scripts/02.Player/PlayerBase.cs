using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
public class PlayerBase : MonoBehaviour
{
    public Joystick moveStick;
    //public Joystick shotStick;

    public static PlayerBase Instance { get; private set; }

    public GameObject crosshair;
    public GameObject runarrow;

    Rigidbody rb;

    protected Stats stats;
    public Stats Stats { get { return stats; } }
    private Vector2 direction;
    private float normalSpeed;
    public float NormalSpeed { get { return normalSpeed; } }
    public float turnSpeed = 15f;

    protected Animator animator;
    public static readonly int hashHorizontal = Animator.StringToHash("Horizontal");
    public static readonly int hashVertical = Animator.StringToHash("Vertical");
    public static readonly int hashIsSprint = Animator.StringToHash("IsSprint");
    public static readonly int hashIsFalling = Animator.StringToHash("IsFalling");
    public static readonly int hashIsGround = Animator.StringToHash("IsGround");
    public static readonly int hashJump = Animator.StringToHash("Jump");
    public static readonly int hashIsAttack = Animator.StringToHash("IsAttack");
    public static readonly int hashAttackSpeed = Animator.StringToHash("AttackSpeed");
    private Vector2 inputAxis;
    private Vector3 characterRotationY = Vector3.zero;
    private bool isRotating = false;

    public float jumpForce = 10f;
    private bool isGround = true;

    private bool isSprint = false;
    public bool IsSprint { get { return isSprint; } }
    public float SprintScale { get; private set; } = 1.5f;

    public CinemachineVirtualCamera vCam;
    public float normalFov = 50f;
    public float runFov = 60f;
    public float fovChangeSpeed = 5f;
    private Coroutine coFovChange;

    public AttackDef attackDef;
    protected GameObject attackTarget;
    protected Vector3 hitpos;
    public GameObject AttackTarget { get { return attackTarget; } }
    private float baseAttackSpeed;
    private float attackSpeedScale = 1f;
    protected virtual void Awake()
    {
        Instance = this;

        attackDef = Instantiate(attackDef);
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        stats = GetComponent<Stats>();
        normalSpeed = stats.Speed;
        baseAttackSpeed = attackDef.speed;
        animator.SetFloat(hashAttackSpeed, baseAttackSpeed * attackSpeedScale);
    }
    protected virtual void Update()
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

        Vector3 velocity = (transform.right * direction.x + transform.forward * direction.y).normalized * stats.Speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }
 
    private void UpdateRotation()
    {


        float yRotation = Input.GetAxis("Mouse X");        


        characterRotationY.y = yRotation * turnSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(characterRotationY));
    } 
    protected virtual void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
        //if (Input.GetAxisRaw("Jump") > 0 && isGround)
        //{
        //    Jump();
        //}        
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

        if (Input.GetKeyDown(KeyCode.LeftControl))
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
        stats.Speed *= SprintScale;

        ChangeSprintState();
    }
    public void StopSprint()
    {
        animator.SetBool(hashIsSprint, isSprint = false);
        stats.Speed = normalSpeed;

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
