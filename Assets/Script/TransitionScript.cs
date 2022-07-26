using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public UnitStatus status;
    public Transform player;
    public Transform spawnPoint;
    public Animator animator;
    private void start()
    {
        player = GetComponent<Transform>();
        spawnPoint = GetComponent<Transform>();
        status = GetComponent<UnitStatus>();
    }
    private void LateUpdate()
    {
        if (status.HealthPoint <= 0)
        {
            animator.Play("UITransition");
 
        }
    }
    public void Spawn()
    {
        animator.Play("UITransitioneND");
    }
    public void TransitionEnd()
    {
        player.transform.position = spawnPoint.position;
        status.HealthPoint = status.maxHP;
        Time.timeScale = 1.0f;
    }
}
