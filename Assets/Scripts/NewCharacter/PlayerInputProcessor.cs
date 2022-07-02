using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


    public class PlayerInputProcessor : MonoBehaviour
    {
        public Vector2 movementAxis;
        public UnityAction OnJumpAction;
        public UnityAction OnAbilityInvoke;
        public int gamepadId;

        void Update()
        {
            movementAxis = Gamepad.all[gamepadId].leftStick.ReadValue();
            if (Gamepad.all[gamepadId].buttonSouth.wasPressedThisFrame)
            {
                if (OnJumpAction == null) return;
                OnJumpAction.Invoke();
            }
            if (Gamepad.all[gamepadId].buttonWest.wasPressedThisFrame)
            {
                OnAbilityInvoke.Invoke();
            }
        }
    }
