using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    private float timer;
    private bool timeActive = true;
    public bool isResultScreen;
    private bool isCalculate;
    public Text timertText;
    public Text deathText;
    public Text scoreText;
    public GameObject scoreObject;
    private UnitStatus status;
    private float score;
    private void Start()
    {
        scoreObject.SetActive(false);
        timertText.text = timer.ToString("F2");
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatus>();
    }
    void Update()
    {
        if (timeActive == true)
        {
            timertText.enabled = true;
            deathText.enabled = true;
            timer += Time.deltaTime;
            timertText.text = timer.ToString("F2");
        }
        else if (timeActive == false && isResultScreen == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position
                = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            timertText.enabled = false;
            deathText.enabled = false;

            scoreObject.SetActive(true);
            scoreText.text = score.ToString();
        }

        deathText.text = status.DeathCounter.ToString();
    }
    public void Calculate()
    {

        score = Mathf.RoundToInt(timer);
        score = score * 10000;
        score = 999990 - score;
        

    }
    public bool IsCalculate
    {
        get { return isCalculate; }
        set { isCalculate = value; }
    }
    public bool ActivateTimer
    {
        get { return timeActive; }
        set { timeActive = value; }
    }
    public float TimerCount
    {
        get { return timer; }
        set { timer = value; }
    }
}
