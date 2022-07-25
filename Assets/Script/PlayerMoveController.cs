 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE, MOVING, JUMPING, ATTACKING, BOUNCING
}

public class PlayerMoveController : MonoBehaviour
{

    public PlayerState state;
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
       aimScript = GameObject.FindGameObjectWithTag("Aim").GetComponent<AimRotation>();
       rb2d = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>(); 
       state = PlayerState.IDLE;
    }

    

    private void FixedUpdate()
    {
        //Moving
        var movement = Input.GetAxis("Horizontal");
        if (movement != 0 && isAbleToAttack == true)
        {
            rb2d.transform.position += new Vector3(movement, 0, 0) * moveSpeed * Time.fixedDeltaTime;
            if (isGrounded == true)
            {
                ChangeAnimationState(PlayerState.MOVING);
                pointerSprite.enabled = true; //Pointer Sprite renderer
            }
        }
        else if (movement == 0 && isGrounded == true && isAbleToAttack == true)
        {
            ChangeAnimationState(PlayerState.IDLE);
            pointerSprite.enabled = true; //Pointer Sprite renderer
        }

        //Flip the sprite
      
        if (movement > 0 || aimScript.FacingRight == true && isAttacking == true)
        {
                GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movement < 0 || aimScript.FacingRight == false && isAttacking == true)
        {
                GetComponent<SpriteRenderer>().flipX = true;
        }
        
      

    }

    void Update()
    {
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
            ChangeAnimationState(PlayerState.BOUNCING);
            attackCurrentCount = attackMaxCount;
            rb2d.gravityScale = 4;
            attackDelay = 0;
            isAbleToAttack = true;
            isAttacking = false;
            pointerSprite.enabled = true;
            isBouncing = false;
        }
 
        //Attack Recovery
        if (isAttacking == true && isBouncing == false)
        {
            //Stop any speed and gravity that affected player, and then move within linear time toward target
            rb2d.velocity = Vector3.zero;
            rb2d.gravityScale = 0;
            attackDelay -= Time.deltaTime;
            rb2d.position = Vector3.Lerp(transform.position, currentTarget, 6f * Time.fixedDeltaTime);
        
            //If the attack is finished, set gravity to normal
            if (attackDelay <= 0)
            {
                rb2d.gravityScale = 4;
                attackDelay = 0;
                isAbleToAttack = true;
                isAttacking = false;
                pointerSprite.enabled = true;
            }
            if (attackCurrentCount <= 0)
            {
                pointerSprite.enabled = false; //Pointer Sprite renderer
            }
        }
      

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
            case PlayerState.BOUNCING:
                animName = "characterBounce";
                break;
            case PlayerState.ATTACKING:
                animName = "characterAttack";
                aimScript.FacingState();
                pointerSprite.enabled = false; //Pointer Sprite renderer
                currentTarget = aimTarget.position; //Set current target with aim target coordinate 1 time so it doesn't keep overwrite the value every update
                attackDelay = 0.4f; //Time before player could attack again

                break;
        }
        animator.Play(animName);
        Debug.Log(animName);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(groundPos.position, new Vector2(0.2f, 0.2f));
    }
}
