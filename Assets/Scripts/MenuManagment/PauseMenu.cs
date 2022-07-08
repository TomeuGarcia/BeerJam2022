using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public int gamepadId0 = 0;
    public int gamepadId1 = 1;

    private bool isPaused = false;

    public delegate void PauseMenuAction();
    public static event PauseMenuAction OnGoToMainMenu;


    private void Awake()
    {
        pauseMenu.SetActive(false);
    }


    private void Update()
    {
        if ((Gamepad.all.Count > 0 && (Gamepad.all[gamepadId0].startButton.wasPressedThisFrame || Gamepad.all[gamepadId1].startButton.wasPressedThisFrame)) ||
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void Home()
    {
        if (OnGoToMainMenu != null) OnGoToMainMenu();

        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
