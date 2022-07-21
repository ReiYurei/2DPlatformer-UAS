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
    [SerializeField]
    private Rigidbody2D rb2d;

    private float moveSpeed = 5;
    private float jumpForce = 5;

    private int attackMaxCount;
    private int attackCurrentCount;
    private float attackDelay;
    private bool isJumping;
    private bool isGrounded;
    private bool isAttacking;
    

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
