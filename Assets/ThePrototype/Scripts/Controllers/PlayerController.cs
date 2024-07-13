using System;
using ThePrototype.Scripts.Base.Interactable;
using ThePrototype.Scripts.InputHandle;
using ThePrototype.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ThePrototype.Scripts.Controller
{
    [Serializable]
    public struct PlayerSetting
    {
        public float moveSpeed;
        public float rotationSpeed;
        public float smoothTime;
        public float jumpHeight;
        public float visualAngleLimit;
        public float rayDistance;
        public LayerMask rayMask;
    }

    public class PlayerController : MonoBehaviour
    {
        public event UnityAction<string> OnPromptTextUpdate;

        [field: Header("References")]
        [field: SerializeField]
        CharacterController CharacterController { get; set; }

        [field: SerializeField] Animator Animator { get; set; }

        [field: SerializeField] InputReader Input { get; set; }

        [field: Header("Settings")]
        [field: SerializeField]
        PlayerSetting PlayerSetting { get; set; }

        #region CashedData

        private Transform _transform;
        private Camera _mainCamera;
        private float _zeroF;
        private float _gravity;

        #endregion

        private float _currentSpeed;
        private float _velocity;
        private bool _isGrounded;
        private Vector3 _playerVelocity;
        private float _xAxisRotation;

        private IInteractable _currentInteractableEntity;

        private void Awake()
        {
            _zeroF = Constant.ZeroF;
            _gravity = Constant.Gravity;
            _transform = transform;
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            Input.Jump += Jump;
            Input.Look += LookHandle;
            Input.Interact += TakeAction;
        }


        private void OnDisable()
        {
            Input.Jump -= Jump;
            Input.Look -= LookHandle;
            Input.Interact -= TakeAction;
        }

        private void Update()
        {
            _isGrounded = CharacterController.isGrounded;

            Interaction();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            var movementDirection = new Vector3(Input.Direction.x, 0, Input.Direction.y).normalized;


            if (movementDirection.magnitude > _zeroF)
            {
                HandleCharacterController(movementDirection);
            }

            _playerVelocity.y += _gravity * Time.deltaTime;
            if (_isGrounded && _playerVelocity.y < _zeroF)
            {
                _playerVelocity.y = _zeroF;
            }

            CharacterController.Move(_playerVelocity * Time.deltaTime);
        }

        private void HandleCharacterController(Vector3 adjustedDirection)
        {
            Vector3 adjustedMovement = adjustedDirection * (PlayerSetting.moveSpeed * Time.deltaTime);
            CharacterController.Move(_transform.TransformDirection(adjustedMovement));
        }

        private void LookHandle(Vector2 input, bool isDeviceMouse)
        {
            _xAxisRotation -= (input.y * Time.deltaTime) * PlayerSetting.rotationSpeed;
            _xAxisRotation = Mathf.Clamp(_xAxisRotation, -PlayerSetting.visualAngleLimit,
                PlayerSetting.visualAngleLimit);
            _mainCamera.transform.localRotation = Quaternion.Euler(_xAxisRotation, 0, 0);
            _transform.Rotate(Vector3.up * (input.x * Time.deltaTime * PlayerSetting.rotationSpeed));
        }

        private void SmoothSpeed(float value)
        {
            _currentSpeed = Mathf.SmoothDamp(_currentSpeed, value, ref _velocity, PlayerSetting.smoothTime);
        }

        private void Jump(bool isPressed)
        {
            if (isPressed && _isGrounded)
            {
                _playerVelocity.y = Mathf.Sqrt(PlayerSetting.jumpHeight * -_gravity);
            }
        }

        private void Interaction()
        {
            Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, PlayerSetting.rayDistance, PlayerSetting.rayMask))
            {
                if (hitInfo.collider.TryGetComponent(out _currentInteractableEntity))
                {
                    OnPromptTextUpdate?.Invoke(_currentInteractableEntity.PromptMessage);
                }
            }
            else
            {
                OnPromptTextUpdate?.Invoke(string.Empty);
                _currentInteractableEntity = null;
            }
        }

        private void TakeAction(bool isPressed)
        {
            if (isPressed && _currentInteractableEntity != null)
            {
                _currentInteractableEntity.Interact();
            }
        }
    }
}