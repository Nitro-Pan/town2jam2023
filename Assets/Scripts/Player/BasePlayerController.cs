using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePlayerController : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }

    [field: SerializeField] private GameObject _GameController;

    [field: SerializeField] private IWeapon Weapon { get; set; }

    [field: Header("Camera")]
    [field: SerializeField] private PlayerCameraInterface CameraInterface { get; set; }
    [field: SerializeField] public Transform CameraTarget { get; set; }

    [field: Header("Movement")]
    [field: SerializeField] private Rigidbody MainRigidbody { get; set; }
    [field: SerializeField] private float JumpHeight { get; set; } = 250.0f;
    [field: SerializeField] private float MoveSpeed { get; set; } = 15.0f;
    [field: SerializeField] [field: Range(0, 1)] private float AirSpeedFactor { get; set; } = 0.5f;
    [field: SerializeField] private float RollSpeed { get; set; } = 25.0f;

    [field: Header("Animation")]
    [field: SerializeField] private Transform MeshTransform { get; set; }
    [field: SerializeField] private Animator Animator { get; set; }
    [field: SerializeField] private float WalkThreshold { get; set; } = 0.8f;

    private bool _isGrounded = false;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _lookDelta = Vector2.zero;
    private bool isLockedOn = false;
    private Vector3 _lastFixedUpdatePosition = Vector3.zero;
    private Matrix4x4 _lastStationaryCameraMatrix = Matrix4x4.identity;
    private bool _CantSwap = false;

    private bool isInIFrames = false;
    private bool canRoll = true;

    #region EVENTS
    public event Action<Vector3, Vector3> OnMovement;
    public event Action<Vector3, Vector2> OnLookDelta;
    public event Action<Transform> OnTargetLock;
    #endregion

    private void Start()
    {
        CameraInterface.PlayerController = this;
    }

    private void Update()
    {
        // camera stuff
        if (_lookDelta != Vector2.zero && _lookDelta.x != float.NaN)
        {
            OnLookDelta?.Invoke(CameraTarget.position, _lookDelta);
        }
    }

    private void FixedUpdate()
    {
        if (!canRoll)
        {
            MainRigidbody.AddForce(_moveDirection * RollSpeed, ForceMode.Acceleration);
        }
        else if (canRoll)
        {
            MainRigidbody.AddForce((_isGrounded ? 1 : AirSpeedFactor) * MoveSpeed * _moveDirection, ForceMode.Acceleration);

            if (MainRigidbody.velocity != Vector3.zero)
            {
                OnMovement?.Invoke(CameraTarget.position, CameraTarget.position - _lastFixedUpdatePosition);
                if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Anim_player_Roll"))
                {
                    Animator.Play("Base Layer.Anim_Player_NRun");
                }
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + _moveDirection, 0.2f);
        Gizmos.DrawLine(transform.position, transform.position + _moveDirection);

        if (isInIFrames)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }

        if (canRoll)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }

    #region PLAYERINPUT
    // player input events need to be public :(
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveDirection2d = context.ReadValue<Vector2>();
        // if (_moveDirection == Vector3.zero && moveDirection2d != Vector2.zero)
        // {
        _lastStationaryCameraMatrix = CameraInterface.CameraController.transform.localToWorldMatrix;
        // }

        if (canRoll)
        {
            _moveDirection.x = moveDirection2d.x;
            _moveDirection.z = moveDirection2d.y;
            _moveDirection = _lastStationaryCameraMatrix.MultiplyVector(_moveDirection);
            _moveDirection.y = 0;
            _moveDirection.Normalize();
            _moveDirection *= moveDirection2d.magnitude;

            if (_moveDirection != Vector3.zero)
            {
                MeshTransform.forward = new Vector3(_moveDirection.x, 0, _moveDirection.z);
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Anim_player_Roll"))
        {
            Animator.Play("Base Layer.Anim_Player_Attack");
            Weapon.TryAttack();
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (!canRoll)
        {
            return;
        }

        Animator.Play("Base Layer.Anim_player_Roll");
        isInIFrames = true;
        canRoll = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        bool isJumping = context.ReadValueAsButton();

        if (isJumping && _isGrounded)
        {
            _isGrounded = false;
            MainRigidbody.AddForce(Vector3.up * JumpHeight, ForceMode.Acceleration);
            // Animator.Play("jump");
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookDelta = context.ReadValue<Vector2>();
    }

    public void OnTargetLockPressed(InputAction.CallbackContext context)
    {
        if (FindTarget() is not Transform target)
        {
            return;
        }

        isLockedOn = !isLockedOn;

        if (context.ReadValueAsButton())
        {
            OnTargetLock?.Invoke(isLockedOn ? target : CameraTarget);
        }
    }

    public void SwapWeapon(InputAction.CallbackContext context)
    {
        if (_CantSwap) return;
        _GameController.GetComponent<GameController>().SwapWeapon();
    }
    #endregion

    private Transform FindTarget()
    {
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>();

        // TODO: find closest to forward vector
        foreach (GameObject go in sceneObjects)
        {
            if (go.CompareTag(CommonDefinitions.Tags.ENEMY))
            {
                return go.transform;
            }
        }

        return CameraTarget;
    }

    public void OnRollAnimStart()
    {
        canRoll = false;
        isInIFrames = true;
    }

    public void OnRollIFrameEnd()
    {
        isInIFrames = false;
    }

    public void OnRollAnimationComplete()
    {
        canRoll = true;
    }
}