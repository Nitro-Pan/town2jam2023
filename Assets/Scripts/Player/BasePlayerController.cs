using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePlayerController : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }

    [field: Header("Camera")]
    [field: SerializeField] private PlayerCameraInterface CameraInterface { get; set; }
    [field: SerializeField] public Transform CameraTarget { get; set; }

    [field: Header("Movement")]
    [field: SerializeField] private Rigidbody MainRigidbody { get; set; }
    [field: SerializeField] private float JumpHeight { get; set; } = 250.0f;
    [field: SerializeField] private float MoveSpeed { get; set; } = 15.0f;
    [field: SerializeField] [field: Range(0, 1)] private float AirSpeedFactor { get; set; } = 0.5f;

    private bool _isGrounded = false;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _lookDelta = Vector2.zero;
    private bool isLockedOn = false;
    private Vector3 _lastFixedUpdatePosition = Vector3.zero;

    #region EVENTS
    public event Action<Vector3, Vector3> OnMovement;
    public event Action<Vector3, Vector2> OnLookDelta;
    public event Action<Transform> OnTargetLocke;
    #endregion


    private void Start()
    {
        CameraInterface.PlayerController = this;
    }

    private void Update()
    {
        // camera stuff
        if (_lookDelta != Vector2.zero)
        {
            OnLookDelta?.Invoke(CameraTarget.position, _lookDelta);
        }
    }

    private void FixedUpdate()
    {
        if (_moveDirection != Vector3.zero)
        {
            transform.forward = Vector3.Normalize(_moveDirection);
        }
        
        MainRigidbody.AddForce((_isGrounded ? 1 : AirSpeedFactor) * MoveSpeed * _moveDirection, ForceMode.Acceleration);

        if (MainRigidbody.velocity != Vector3.zero)
        {
            OnMovement?.Invoke(CameraTarget.position, CameraTarget.position - _lastFixedUpdatePosition);
        }

        _lastFixedUpdatePosition = CameraTarget.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isGrounded && collision.gameObject.CompareTag(CommonDefinitions.Tags.GROUND))
        {
            _isGrounded = true;
        }
    }

    #region PLAYERINPUT
    // player input events need to be public :(
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveDirection2d = context.ReadValue<Vector2>();
        _moveDirection.x = moveDirection2d.x;
        _moveDirection.z = moveDirection2d.y;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        // TODO: determine shooting behaviour
        // abtracted weapons are a must
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        // TODO: determine rolling behaviour 
        // we probably want decoupled frame data & animation context
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        bool isJumping = context.ReadValueAsButton();

        if (isJumping && _isGrounded)
        {
            _isGrounded = false;
            MainRigidbody.AddForce(Vector3.up * JumpHeight, ForceMode.Acceleration);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookDelta = context.ReadValue<Vector2>();
    }

    public void OnTargetLock(InputAction.CallbackContext context)
    {
        if (FindTarget() is not Transform target)
        {
            return;
        }

        isLockedOn = !isLockedOn;

        if (context.ReadValueAsButton())
        {
            OnTargetLocke?.Invoke(isLockedOn ? target : CameraTarget);
        }
    }
    #endregion

    private Transform FindTarget()
    {
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject go in sceneObjects)
        {
            if (go.tag == CommonDefinitions.Tags.ENEMY)
            {
                return go.transform;
            }
        }

        return CameraTarget;
    }
}
