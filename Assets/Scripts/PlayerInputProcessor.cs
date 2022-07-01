﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


    public class PlayerInputProcessor : MonoBehaviour
    {
        public Vector2 movementAxis;
        public UnityAction OnJumpAction;  
        public int gamepadId;

        void Update()
        {
            movementAxis = Gamepad.all[gamepadId].leftStick.ReadValue();
            if (Gamepad.all[gamepadId].buttonSouth.wasPressedThisFrame)
            {
                OnJumpAction.Invoke();
            }
        }
    }