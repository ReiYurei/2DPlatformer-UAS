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
    private string currentState;

    public Transform groundPos;
    public LayerMask ground;

    private float moveSpeed = 5f;
    private float jumpForce = 15f;

    private int attackMaxCount;
    private int attackCurrentCount;
    private int attackDelay;
    private bool isJumping;
    private bool isGrounded;
    private bool isAttacking;
    

    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>(); 

        state = PlayerState.IDLE;
    }
    void ChangeAnimationState(PlayerState newState)
    {
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
                break;
        }
        animator.Play(animName);
    }
    private void FixedUpdate()
    {
        var movement = Input.GetAxis("Horizontal");
        if (movement != 0)
        {
            rb2d.transform.position += new Vector3(movement, 0, 0) * moveSpeed * Time.fixedDeltaTime;
            ChangeAnimationState(PlayerState.MOVING);
        }
        else if (movement == 0 && isGrounded == true)
        {
            ChangeAnimationState(PlayerState.IDLE);
        }
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, 0.325f, ground);
        if (Input.GetButtonDown("Jump") == true && isGrounded == true)
        {
            rb2d.velocity += Vector2.up * jumpForce;
            isJumping = true;
            isGrounded = false;
        }
        if (isJumping == true)
        {
            ChangeAnimationState(PlayerState.JUMPING);
        }
    }
  
}
