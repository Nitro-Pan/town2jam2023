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

    private void Start()
    {
        CameraController = Camera.main.GetComponent<CameraController>(); // find it???
    }

    private void AddPlayerEvents()
    {
        _playerController.OnLookDelta += OnPlayerLookDelta;
        _playerController.OnMovement += OnPlayerMovement;
        _playerController.OnTargetLocke += OnPlayerTargetLock;

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
        _playerController.OnTargetLocke -= OnPlayerTargetLock;
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

