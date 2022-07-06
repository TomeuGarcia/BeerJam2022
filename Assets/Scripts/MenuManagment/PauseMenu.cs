using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public int gamepadId0;
    public int gamepadId1;

    private bool isPaused = false;


    private void Update()
    {
        if (Gamepad.all.Count > 0 && (Gamepad.all[gamepadId0].startButton.wasPressedThisFrame || Gamepad.all[gamepadId1].startButton.wasPressedThisFrame))
        {
            if (isPaused)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
