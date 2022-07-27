using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    private FinishLineScript finishLine;
    private UnitStatus status;
    private Transform player;
    private Transform spawnPoint;
    private Animator animator;
    private bool isChangeScene = false;
    private void Awake()
    {
        finishLine = GameObject.FindGameObjectWithTag("FinishPoint").GetComponent<FinishLineScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<Transform>();
        animator = this.GetComponent<Animator>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatus>();
    }
    private void LateUpdate()
    {
        TransitionStart();
        NextLevelStart();
    }
    public void Spawn()
    {
        animator.Play("UITransitionEnd");
    }
    public void TransitionStart()
    {
        if (status.IsDie == true)
        {
            status.IsDie = false;
            animator.Play("UITransition");
        }
    }
    public void TransitionEnd()
    {
        player.transform.position = spawnPoint.position;
        status.HealthPoint = status.maxHP;
    }
    public void NextLevelStart()
    {
        if (finishLine.IsGoalReached == true)
        {
            finishLine.IsGoalReached = false;
            animator.Play("UINextLevelTransitionStart");
        }
    }
    public void ChangeSceneCondition()
    {
        isChangeScene = true;
        animator.Play("UITransitionFull");

    }
    public void NextLevelEnd()
    {

        player.transform.position = spawnPoint.position;
        status.HealthPoint = status.maxHP;
        animator.Play("UINextLevelTransitionEnd");
    }
    public void ResultScreen()
    {

    }
    public bool IsChangeScene
    {
        get { return isChangeScene; }
        set { isChangeScene = value; }
    }
}
