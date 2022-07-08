using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public delegate void SceneSwitchAction();
    public static event SceneSwitchAction OnGoToGameScene;

    public void OpenScene(int index)
    {
        if (index == 2)
        {
            if (OnGoToGameScene != null) OnGoToGameScene();
        }

        SceneManager.LoadScene(index);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
