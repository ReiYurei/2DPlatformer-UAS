using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private BoxCollider2D hitboxCollider;
    private Rigidbody2D targetRb;
    private PlayerMoveController playerScript;
    private void Start()
    {
        targetRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveController>();
        hitboxCollider = this.GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            playerScript.Bouncing = true;
            targetRb.velocity = Vector2.zero;

            //The Enemy will bounce the player up
            targetRb.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+0.5f);
            targetRb.velocity = new Vector2(0, 20f); 
            Debug.Log(collision);

        }
    }
}
