using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControllerCheck : MonoBehaviour
{
    public GameObject tick0;
    public GameObject tick1;
    public int gamepadId0;
    public int gamepadId1;

    [SerializeField] private SceneSwitch sceneSwitch;

    private void Awake()
    {
        tick0.SetActive(false);
        tick1.SetActive(false);
    }

    void Update()
    {

        if (Gamepad.all.Count < 1) return;
        if (Gamepad.all[gamepadId0].buttonSouth.wasPressedThisFrame)
        {
            tick0.SetActive(true);
        }

        if (Gamepad.all.Count < 2) return;
        if (Gamepad.all[gamepadId1].buttonSouth.wasPressedThisFrame)
        {
            tick1.SetActive(true);
        }

        if (tick0.active && tick1.active)
        {
            sceneSwitch.OpenScene(1);
        }
    }
}
