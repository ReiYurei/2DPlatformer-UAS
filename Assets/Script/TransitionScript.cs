using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    private UnitStatus status;
    private Transform player;
    private Transform spawnPoint;
    private Animator animator;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<Transform>();
        animator = this.GetComponent<Animator>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatus>();
    }
    private void Update()
    {
        TransitionStart();
    }
    public void TransitionStart()
    {
        if (status.IsDie == true)
        {
            status.IsDie = false;
            animator.Play("UITransition");
        }
    }
    public void Spawn()
    {
        animator.Play("UITransitionEnd");
    }
    public void TransitionEnd()
    {
        player.transform.position = spawnPoint.position;
        status.HealthPoint = status.maxHP;
    }
}
