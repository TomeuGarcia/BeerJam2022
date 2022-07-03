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
        if (Gamepad.all[gamepadId0].startButton.wasPressedThisFrame || Gamepad.all[gamepadId1].startButton.wasPressedThisFrame && isPaused == false)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        if (Gamepad.all[gamepadId0].startButton.wasPressedThisFrame || Gamepad.all[gamepadId1].startButton.wasPressedThisFrame && isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        if (Gamepad.all[gamepadId0].selectButton.wasPressedThisFrame || Gamepad.all[gamepadId1].selectButton.wasPressedThisFrame && isPaused)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }
    }
}
