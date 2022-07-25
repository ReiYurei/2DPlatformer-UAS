using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private CircleCollider2D hitboxCollider;
    private PlayerMoveController moveController;
    private PlayerState currentState;
    private void Start()
    {
        moveController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveController>();
        hitboxCollider = this.GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        currentState = moveController.state;
        if (moveController.state == PlayerState.ATTACKING)
        {
            hitboxCollider.enabled = true;
        }
        else
        {
            hitboxCollider.enabled = false;
        }
    }

}
