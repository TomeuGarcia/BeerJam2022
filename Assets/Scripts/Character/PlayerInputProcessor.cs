using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerInputProcessor : MonoBehaviour
{
    public Vector2 movementAxis;
    public UnityAction OnJumpAction;
    public UnityAction OnAbilityInvoke;
    public UnityAction OnCanonEnterExitAction;
    public UnityAction OnCanonShootAction;
    public int gamepadId;

    [Flags] private enum InputConfig { Controller = 0x1,
                                       WASD = 0x2, 
                                       Arrows = 0x4 };
    [SerializeField] InputConfig inputConfig = InputConfig.Controller | InputConfig.WASD;



    void Update()
    {
        movementAxis = Vector2.zero;

        if ((inputConfig & InputConfig.Controller) == InputConfig.Controller)
        {
            UpdateControllersConfigInput();
        }
        if ((inputConfig & InputConfig.WASD) == InputConfig.WASD)
        {
            UpdateWASDConfigInput();
        }
        if ((inputConfig & InputConfig.Arrows) == InputConfig.Arrows)
        {
            UpdateArrowsConfigInput();
        }

        movementAxis.x = Mathf.Clamp(movementAxis.x, -1f, 1f);
    }

    
    public static bool IsGamepadInvalid(int gamepadId)
    {
        return Gamepad.all.Count <= gamepadId;
    }


    private void UpdateControllersConfigInput()
    {
        if (IsGamepadInvalid(gamepadId)) return;

        movementAxis += Gamepad.all[gamepadId].leftStick.ReadValue();
        if (Gamepad.all[gamepadId].buttonSouth.wasPressedThisFrame)
        {
            if (OnJumpAction != null) OnJumpAction.Invoke();
            if (OnCanonShootAction != null) OnCanonShootAction.Invoke();
        }
        if (Gamepad.all[gamepadId].buttonWest.wasPressedThisFrame)
        {
            if(OnAbilityInvoke != null) OnAbilityInvoke.Invoke();
        }
        if (Gamepad.all[gamepadId].buttonNorth.wasPressedThisFrame)
        {
            if (OnCanonEnterExitAction != null) OnCanonEnterExitAction.Invoke();
        }
    }


    private void UpdateWASDConfigInput()
    {
        movementAxis.x += -Keyboard.current.aKey.ReadValue() + Keyboard.current.dKey.ReadValue();
        //movementAxis.y = -Keyboard.current.sKey.ReadValue() + Keyboard.current.wKey.ReadValue();

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            if (OnJumpAction != null) OnJumpAction.Invoke();
            if (OnCanonEnterExitAction != null) OnCanonEnterExitAction.Invoke();
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            if (OnAbilityInvoke != null) OnAbilityInvoke.Invoke();
            if (OnCanonShootAction != null) OnCanonShootAction.Invoke();
        }

    }

    private void UpdateArrowsConfigInput()
    {
        movementAxis.x += -Keyboard.current.leftArrowKey.ReadValue() + Keyboard.current.rightArrowKey.ReadValue();
        //movementAxis.y = -Keyboard.current.downArrowKey.ReadValue() + Keyboard.current.upArrowKey.ReadValue();

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (OnJumpAction != null) OnJumpAction.Invoke();
            if (OnCanonEnterExitAction != null) OnCanonEnterExitAction.Invoke();
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            if (OnAbilityInvoke != null) OnAbilityInvoke.Invoke();
            if (OnCanonShootAction != null) OnCanonShootAction.Invoke();
        }

    }

}
