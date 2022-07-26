using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    public string unitName;
    public int dmgDeal;
    public float recoveryTime;

    public int maxHP;
    private int currentHP;
    public float respawnTime;

    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        collider = this.GetComponent<Collider2D>();
        currentHP = maxHP;
    }
    public void PlayerDealDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            DisabledState();
            Invoke("Respawn", respawnTime);
        }      
    }
    public void PlayerGetHit(int dmg)
    {
        currentHP -= dmg;
        DisabledState();
        Invoke("EnabledState", recoveryTime);
    }
    public void EnabledState()
    {
        collider.enabled = true;
        spriteRenderer.color = Color.white;
    }

    public void DisabledState()
    {
        collider.enabled = false;
        spriteRenderer.color = Color.black;

    }
    public void Respawn()
    {
        currentHP = maxHP;
        collider.enabled = true;
        spriteRenderer.color = Color.red;
    }
    public void PlayerDies()
    {

    }
    public int HealthPoint
    {
        get { return currentHP; }
        set { currentHP = value; }
    }
}
