using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ThePrototype.Scripts.InputHandle
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Edulog/InputReader")]
    public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction EnableMouseControlCamera = delegate { };
        public event UnityAction DisableMouseControlCamera = delegate { };
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction<bool> Interact = delegate { };
        public event UnityAction<bool> AlternateInteract = delegate { };
        public event UnityAction<bool> Inventory = delegate { };


        private PlayerInputActions _inputActions;

        public Vector3 Direction => _inputActions.Player.Move.ReadValue<Vector2>();

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new();
                _inputActions.Player.SetCallbacks(this);
            }

            _inputActions.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look?.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }

        private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

        public void OnFire(InputAction.CallbackContext context)
        {
            //noop
        }

        public void OnMouseControlCamera(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    EnableMouseControlCamera?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    DisableMouseControlCamera?.Invoke();
                    break;
            }
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            //noop
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Jump?.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Jump?.Invoke(false);
                    break;
            }
            
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Interact?.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Interact?.Invoke(false);
                    break;
            }
        }
        public void OnAlternateInteract(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    AlternateInteract?.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    AlternateInteract?.Invoke(false);
                    break;
            }
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Inventory?.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Inventory?.Invoke(false);
                    break;
            }
        }
    }
}