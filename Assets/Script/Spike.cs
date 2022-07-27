using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Collider2D collider;
    private UnitStatus status;
    void Awake()
    {
        collider = this.GetComponent<Collider2D>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatus>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        status.PlayerGetHit(status.maxHP);
    }


}
