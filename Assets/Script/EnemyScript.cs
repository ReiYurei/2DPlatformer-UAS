using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private UnitStatus unitStat;
    private BoxCollider2D hitboxCollider;
    private Rigidbody2D targetRb;
    private PlayerMoveController playerScript;
    private void Start()
    {
        unitStat = this.GetComponent<UnitStatus>();
        targetRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hitbox"))
        {
            playerScript.Bouncing = true;
            targetRb.velocity = Vector2.zero;

            //The Enemy will bounce the player up
            targetRb.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 0.5f);
            targetRb.velocity = new Vector2(0, 20f);
            unitStat.PlayerDealDamage(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatus>().dmgDeal);
        }
         if (collision.gameObject.CompareTag("Hurtbox"))
        {
            playerScript.Bouncing = true;
            //targetRb.velocity = Vector2.zero;

            //The Enemy will bounce the player up
            // targetRb.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 0.5f);
            targetRb.velocity = new Vector2(-targetRb.velocity.x, -targetRb.velocity.y);

            GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatus>().PlayerGetHit(unitStat.dmgDeal);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
}
