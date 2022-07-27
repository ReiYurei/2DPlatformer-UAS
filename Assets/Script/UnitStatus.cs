using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    public string unitName;
    public int dmgDeal;
    public float recoveryTime;
    private bool isDie = false;
    private int deathCounter = 1;

    public int maxHP;
    private int currentHP;
    public float respawnTime;

    private Collider2D spriteCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteCollider = this.GetComponent<Collider2D>();
        currentHP = maxHP;
    }
    public void PlayerDealDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {

            Object.Destroy(this.gameObject);
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
        spriteCollider.enabled = true;
        spriteRenderer.color = Color.white;
    }

    public void DisabledState()
    {
        spriteCollider.enabled = false;
        spriteRenderer.color = new Color(1,0,0,0.5f);

    }
    public void Respawn()
    {
      
    }
    public void PlayerDie()
    {
        isDie = true;
        deathCounter--;

    }
    public int DeathCounter
    {
        get { return deathCounter; }
        set { deathCounter = value; }
    }
    public bool IsDie
    {
        get { return isDie; }
        set { isDie = value; }
    }
    public int HealthPoint
    {
        get { return currentHP; }
        set { currentHP = value; }
    }
}
