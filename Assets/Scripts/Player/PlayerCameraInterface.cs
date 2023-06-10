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

    //public void Set(Transform CameraTarget, BasePlayerController playerController)
    //{
    //    playerController.OnMovement += OnPlayerMovement;
    //    playerController.OnLookDelta += OnPlayerLook;
    //}

    //private void OnPlayerMovement(Vector3 pos, Vector3 velocity)
    //{
    //    MainCamera.transform.position = MainCamera.transform.forward * DistanceFromPlayer - pos;

    //    TrackPlayer(pos);

    //    // TODO: smooth out velocity
    //}

    //private void OnPlayerLook(Vector3 pos, Vector2 lookDelta)
    //{
    //    MainCamera.transform.RotateAround(pos, Vector3.up, Mathf.Asin(lookDelta.x / DistanceFromPlayer) * Mathf.Rad2Deg);
    //    MainCamera.transform.RotateAround(pos, MainCamera.transform.right, Mathf.Asin(lookDelta.y / DistanceFromPlayer) * Mathf.Rad2Deg);

    //    TrackPlayer(pos);
    //}

    //private void TrackPlayer(Vector3 targetPos)
    //{
    //    MainCamera.transform.LookAt(targetPos);
    //}
}

