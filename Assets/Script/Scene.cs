using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene : MonoBehaviour
{
    public enum SceneType
    {
        MainScene,Stage2
    }
    public TransitionScript transitionScript;
    private int levelIndex;
    private void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void LateUpdate()
    {
        ChangeScene();
    }
    public void ChangeScene()
    {
        if (transitionScript.IsChangeScene == true && levelIndex == 0 && transitionScript.dying == false)
        {
            StartCoroutine(LoadNextStage(1));
            transitionScript.IsChangeScene = false;
        }
        else if (transitionScript.IsChangeScene == true && levelIndex < 0 && transitionScript.dying == false)
        {

            StartCoroutine(LoadNextStage(0));
            transitionScript.IsChangeScene = false;
        }
        else if (transitionScript.IsChangeScene == true && transitionScript.dying == true)
        {
            StartCoroutine(LoadNextStage(levelIndex));
            transitionScript.IsChangeScene = false;
            transitionScript.dying = false;
        }
    }
    IEnumerator LoadNextStage(int level)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
