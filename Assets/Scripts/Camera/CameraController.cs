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

    private Vector3 TargetVector => Camera.transform.position - FollowTarget.position;

    private void Start()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }
        
        // using hack in BasePlayerController instead
        // LookTarget = Camera.transform;
    }

    private void Update()
    {
        Camera.transform.LookAt(LookTarget);

        if (LookTarget != FollowTarget)
        {
            
        }

        Camera.transform.position = FollowTarget.position + TargetVector.normalized * PlayerOffset;
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

        // rotate follow vector 90 degrees to get better rotation (thank fuck it's 90)
        Vector3 rightRotate = new Vector3(TargetVector.z, TargetVector.y, TargetVector.x);
        Camera.transform.RotateAround(FollowTarget.position, rightRotate, Mathf.Asin(delta.y / PlayerOffset) * Mathf.Rad2Deg * CameraLookSens);
    }
}
