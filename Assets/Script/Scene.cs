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
    private void Awake()
    {
        SceneManager.LoadScene(SceneType.MainScene.ToString());
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void LateUpdate()
    {
        ChangeScene();
    }
    public void ChangeScene()
    {
        if (transitionScript.IsChangeScene == true && levelIndex <= 0)
        {
            StartCoroutine(LoadNextStage());
            transitionScript.IsChangeScene = false;
        }
        else if (transitionScript.IsChangeScene == true && levelIndex <= 0)
        {

        }

    }
    IEnumerator LoadNextStage()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneType.Stage2.ToString());
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
