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
    private Rigidbody2D rb2d;
    private Animator animator;
    public SpriteRenderer pointerSprite;

    public Transform aimTarget;
    private Vector3 currentTarget;
    public Transform groundPos;
    public LayerMask ground;

    private float moveSpeed = 5f;
    private float jumpForce = 15f;

    public int attackMaxCount = 2;
    private int attackCurrentCount;
    private float attackDelay;
    private bool isJumping;
    private bool isGrounded;
    private bool isAttacking;
    private bool isAbleToAttack = true;



    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>(); 
       state = PlayerState.IDLE;
       //currentTarget = new List<Vector3>();
    }

    

    private void FixedUpdate()
    {
        var movement = Input.GetAxis("Horizontal");
        if (movement != 0 && isAbleToAttack == true)
        {
            rb2d.transform.position += new Vector3(movement, 0, 0) * moveSpeed * Time.fixedDeltaTime;
            if (isGrounded == true)
            {
                ChangeAnimationState(PlayerState.MOVING);
            }
        }
        else if (movement == 0 && isGrounded == true)
        {
            ChangeAnimationState(PlayerState.IDLE);
        }
       

        
    }

    void Update()
    {
        //Jumping and Ground Check
        isGrounded = Physics2D.OverlapCircle(groundPos.position, 0.325f, ground);
        if (Input.GetButtonDown("Jump") == true && isGrounded == true)
        {
            rb2d.velocity += Vector2.up * jumpForce;
            isJumping = true;
            isGrounded = false;
        }
        if (isGrounded == false && isAttacking == false)
        {
            ChangeAnimationState(PlayerState.JUMPING);
        }
        if (isGrounded == true)
        {
            attackCurrentCount = attackMaxCount;
            isJumping = false;
        }


        //Attacking
        if (Input.GetButtonDown("Attack") && isAbleToAttack == true)
        {
            ChangeAnimationState(PlayerState.ATTACKING);
            isAttacking = true;
            isAbleToAttack = false;
        }
        //Attack Recovery
        if (isAttacking == true)
        {
            //Stop any speed and gravity that affected player, and then move within linear time toward target
            rb2d.velocity = Vector3.zero;
            rb2d.gravityScale = 0;
            attackDelay -= Time.fixedDeltaTime;
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
        }

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
                pointerSprite.enabled = false; //Pointer Sprite renderer
                currentTarget = aimTarget.position; //Set current target with aim target coordinate 1 time so it doesn't keep overwrite the value every update
                attackDelay = 5f; //Time before player could attack again

                break;
        }
        animator.Play(animName);
        Debug.Log(animName);
    }

}
