using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptFlyer : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Hitbox"))
        {
           
            //The Enemy will bounce the player up
            targetRb.position = new Vector2(this.transform.position.x, this.transform.position.y);
            playerScript.CurrentAttackCount = 2;
            unitStat.PlayerDealDamage(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatus>().dmgDeal);
        }
        if (collision.gameObject.CompareTag("Hurtbox"))
        {
            playerScript.Bouncing = true;

            //The Enemy will bounce the player up
            targetRb.velocity = new Vector3(-targetRb.velocity.x, -targetRb.velocity.y);

            GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatus>().PlayerGetHit(unitStat.dmgDeal);
        }
    }
}
