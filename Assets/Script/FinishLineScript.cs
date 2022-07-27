using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    private bool isGoalReached = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isGoalReached = true;
        }
    }
    public bool IsGoalReached
    {
        get { return isGoalReached; }
        set { isGoalReached = value; }
    }
}
