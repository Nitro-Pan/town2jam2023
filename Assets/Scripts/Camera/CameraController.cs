using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [field: SerializeField] private Camera Camera { get; set; }
    [field: SerializeField] private float PlayerOffset { get; set; }

    public Transform LookTarget { get; set; }
    public Transform FollowTarget { get; set; }

    private void Start()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }

        LookTarget = Camera.transform;
    }

    private void Update()
    {
        Camera.transform.LookAt(LookTarget);
    }

    public void Translate(Vector3 delta)
    {
        Camera.transform.Translate(delta);
    }

    public void Orbit(Vector2 delta)
    {
        Camera.transform.RotateAround(FollowTarget.position, Vector3.up, Mathf.Asin(delta.x / PlayerOffset) * Mathf.Rad2Deg);
        Camera.transform.RotateAround(FollowTarget.position, Camera.transform.right, Mathf.Asin(delta.y / PlayerOffset) * Mathf.Rad2Deg);
    }
}
