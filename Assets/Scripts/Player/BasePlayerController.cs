using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePlayerController : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }

    [Header("Camera")]
    [SerializeField] private Camera _mainCamera;

    [Header("Movement")]
    [SerializeField] private Rigidbody _mainRigidbody;
    [SerializeField] [Range(0, 1000)] private float _jumpHeight;
    [SerializeField] [Range(0, 1000)] private float _moveSpeed;

    private bool _isGrounded = false;
    private Vector3 _moveDirection = Vector3.zero;


    private void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    private void FixedUpdate()
    {
        _mainRigidbody.AddForce(_moveDirection * _moveSpeed, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isGrounded)
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
            _mainRigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Acceleration);
        }
    }
    #endregion
}
