using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraInterface : MonoBehaviour
{
    private BasePlayerController _playerController;

    public CameraController CameraController { get; set; }
    public BasePlayerController PlayerController 
    {
        get => _playerController;
        set 
        {
            RemovePlayerEvents();
            _playerController = value;
            AddPlayerEvents();
        }
    }

    public Vector3 CameraForward => CameraController.transform.forward;

    private void Start()
    {
        CameraController = Camera.main.GetComponent<CameraController>(); // find it???
    }

    private void AddPlayerEvents()
    {
        _playerController.OnLookDelta += OnPlayerLookDelta;
        _playerController.OnMovement += OnPlayerMovement;
        _playerController.OnTargetLock += OnPlayerTargetLock;

        CameraController.LookTarget = PlayerController.CameraTarget;
        CameraController.FollowTarget = PlayerController.CameraTarget;
    }

    private void RemovePlayerEvents()
    {
        if (_playerController == null)
        {
            return;
        }

        _playerController.OnLookDelta -= OnPlayerLookDelta;
        _playerController.OnMovement -= OnPlayerMovement;
        _playerController.OnTargetLock -= OnPlayerTargetLock;
    }

    private void OnPlayerLookDelta(Vector3 playerPos, Vector2 lookDelta)
    {
        CameraController.Orbit(lookDelta);
    }

    private void OnPlayerMovement(Vector3 playerPos, Vector3 playerVelocity)
    {
        CameraController.Translate(playerVelocity);
    }

    private void OnPlayerTargetLock(Transform target)
    {
        CameraController.LookTarget = target;
    }
}

