using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    private TextScript timerScript;
    private FinishLineScript finishLine;
    private UnitStatus status;
    private Transform player;
    private Transform spawnPoint;
    private Animator animator;
    private bool isChangeScene = false;
    public bool dying;
    private void Awake()
    {
        timerScript = GameObject.FindGameObjectWithTag("Timer").GetComponent<TextScript>();
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

    public void TransitionStart()
    {
        if (status.IsDie == true)
        {
            animator.Play("UITransition");
            status.IsDie = false;
        }
    }
    public void TransitionEnd()
    {
        animator.Play("UITransitionEnd");
        status.EnabledState();
        status.DeathCounter++;
        player.transform.position = spawnPoint.position;
        status.HealthPoint = status.maxHP;
    }
    public void Restart()
    {
        animator.Play("UINextLevelTransitionStart");
    }
    public void NextLevelStart()
    {
        if (finishLine.IsGoalReached == true)
        {
            timerScript.isResultScreen = true;
            timerScript.Calculate();
            timerScript.ActivateTimer = false;
            if (Input.GetMouseButtonDown(0))
            {
                timerScript.isResultScreen = false;
                timerScript.scoreObject.SetActive(false);
                finishLine.IsGoalReached = false;
                animator.Play("UINextLevelTransitionStart");
            }

        }
       /* if (status.IsDie == true)
        {
            dying = true;
            status.IsDie = false;
            player.transform.position = new Vector3(player.position.x,player.position.y - 4, player.position.z);
            animator.Play("UINextLevelTransitionStart");
        }*/
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
