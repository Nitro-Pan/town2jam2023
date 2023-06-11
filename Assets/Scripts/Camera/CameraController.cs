using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [field: SerializeField] private Camera Camera { get; set; }
    [field: SerializeField] private float PlayerOffset { get; set; }
    [field: SerializeField] private float CameraLookSens { get; set; }

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

        Vector3 targetVector = Camera.transform.position - FollowTarget.position;
        Camera.transform.position = FollowTarget.position + targetVector.normalized * PlayerOffset;
    }

    public void Translate(Vector3 delta)
    {

    }

    public void Orbit(Vector2 delta)
    {
        if (Mathf.Abs(PlayerOffset) < Mathf.Epsilon)
        {
            return;
        }

        Camera.transform.RotateAround(FollowTarget.position, Vector3.up, Mathf.Asin(delta.x / PlayerOffset) * Mathf.Rad2Deg * CameraLookSens);
        Camera.transform.RotateAround(FollowTarget.position, Camera.transform.right, Mathf.Asin(delta.y / PlayerOffset) * Mathf.Rad2Deg * CameraLookSens);
    }
}
