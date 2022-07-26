 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE, MOVING, JUMPING, ATTACKING
}

public class PlayerMoveController : MonoBehaviour
{

    public PlayerState state;
    private UnitStatus status;
    private Rigidbody2D rb2d;
    private Animator animator;
    public SpriteRenderer pointerSprite;
    private AimRotation aimScript;

    public Transform aimTarget;
    private Vector3 currentTarget;
    public Transform groundPos;
    public LayerMask ground;

    private float moveSpeed = 5f;
    private float jumpForce = 15f;

    public int attackMaxCount = 1;
    private int attackCurrentCount;
    private float attackDelay;
    private bool isBouncing;
    private bool isGrounded;
    private bool isAttacking;
    private bool isAbleToAttack = true;



    void Start()
    {
       status = this.GetComponent<UnitStatus>();
       aimScript = GameObject.FindGameObjectWithTag("Aim").GetComponent<AimRotation>();
       rb2d = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>(); 
       state = PlayerState.IDLE;
    }
    private void FixedUpdate()
    {
        if (groundPos.parent == null)
        {
            groundPos.position = rb2d.transform.position + new Vector3(0,-0.8f,0);
        }
    }

    void Update()
    {
        Debug.Log(status.HealthPoint);
        //Moving
        var movement = Input.GetAxis("Horizontal");
        if (movement != 0 && isAbleToAttack == true)
        {
            rb2d.transform.position += new Vector3(movement, 0, 0) * moveSpeed * Time.deltaTime;
            if (isGrounded == true)
            {
                ChangeAnimationState(PlayerState.MOVING);
                pointerSprite.enabled = true; //Pointer Sprite renderer
            }
        }
        if (movement == 0 && isGrounded == true && isAbleToAttack == true)
        {
            ChangeAnimationState(PlayerState.IDLE);
            pointerSprite.enabled = true; //Pointer Sprite renderer
        }

        //Flip the sprite
            //Flip when movement
        if (movement > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movement < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

            //Flip when attacking
        if (aimScript.FacingRight == true && isAttacking == true)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (aimScript.FacingRight == false && isAttacking == true)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }

        //Jumping and Ground Check
        isGrounded = Physics2D.OverlapBox(groundPos.position, new Vector2(0.2f, 0.1f), 1f, ground);
        if (Input.GetButtonDown("Jump") == true && isGrounded == true)
        {
            rb2d.velocity += Vector2.up * jumpForce;
            isGrounded = false;
            ChangeAnimationState(PlayerState.JUMPING);
        }
        if (isGrounded == false && isAttacking == false)
        {
            ChangeAnimationState(PlayerState.JUMPING);
        }
        if (isGrounded == true)
        {
            attackCurrentCount = attackMaxCount;
        }

        //Attacking
        if (Input.GetButtonDown("Attack") && isAbleToAttack == true && attackCurrentCount > 0)
        {
            ChangeAnimationState(PlayerState.ATTACKING);
            isAttacking = true;
            isAbleToAttack = false;
            attackCurrentCount--;
            
        }
        //Bouncing
        if (isBouncing == true)
        {          
            ResetGroundPosition();
            ResetStatus();
            attackCurrentCount = attackMaxCount;      
            isBouncing = false;
        }
 
        //Attack Recovery
        if (isAttacking == true && isBouncing == false)
        {
            //Stop any speed and gravity that affected player, and then move within linear time toward target
            groundPos.transform.parent = null;
            rb2d.transform.rotation = Quaternion.Euler(new Vector3(0,0,aimScript.mouseAngle));
            rb2d.velocity = Vector3.zero;
            rb2d.gravityScale = 0;
            attackDelay -= Time.deltaTime;
            rb2d.position = Vector3.Lerp(transform.position, currentTarget, 6f * Time.fixedDeltaTime);
        
            //If the attack is finished, set every important aspect to normal
            if (attackDelay <= 0)
            {
                ResetGroundPosition();
                ResetStatus();     
            }
            if (attackCurrentCount <= 0)
            {
                pointerSprite.enabled = false; //Pointer Sprite renderer
            }
        }
    }
    void ResetStatus()
    {
        rb2d.transform.rotation = Quaternion.Euler(Vector3.zero);
        GameObject.FindGameObjectWithTag("Hurtbox").GetComponent<BoxCollider2D>().enabled = true;
        rb2d.gravityScale = 4;
        attackDelay = 0;
        isAbleToAttack = true;
        isAttacking = false;
        pointerSprite.enabled = true;
    }
    void ResetGroundPosition()
    {
        groundPos.parent = this.gameObject.transform;
        groundPos.position = rb2d.transform.position;
        groundPos.rotation = rb2d.transform.rotation;
        groundPos.localScale = Vector3.one;
        groundPos.localPosition = new Vector3(groundPos.localPosition.x, groundPos.localPosition.y + -0.8f, groundPos.localPosition.z);
    }
    public bool Attacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }
    public bool Bouncing
    {
        get { return isBouncing; }
        set { isBouncing = value; }
    }
    public bool Grounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    //Enum to set the current state for animation
    void ChangeAnimationState(PlayerState newState)
    {
        if (state == newState) return;
        state = newState;
        string animName = string.Empty;
        switch (newState)
        {
            case PlayerState.IDLE:
                animName = "characterIdle";
                break;
            case PlayerState.MOVING:
                animName = "characterMove";
                break;
            case PlayerState.JUMPING:
                animName = "characterJump";
                break;
            case PlayerState.ATTACKING:
                animName = "characterAttack";
                GameObject.FindGameObjectWithTag("Hurtbox").GetComponent<BoxCollider2D>().enabled = false;
                aimScript.FacingState();
                aimScript.Angle();
                pointerSprite.enabled = false; //Pointer Sprite renderer
                currentTarget = aimTarget.position; //Set current target with aim target coordinate 1 time so it doesn't keep overwrite the value every update
                attackDelay = 0.4f; //Time before player could attack again

                break;
        }
        animator.Play(animName);
        //Debug.Log(animName);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(groundPos.position, new Vector2(0.2f, 0.2f));
    }
}
