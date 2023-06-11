using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [field: SerializeField] private Camera Camera { get; set; }
    [field: SerializeField] private float PlayerFreeOffset { get; set; } = 80.0f;
    [field: SerializeField] private float PlayerLockOffset { get; set; } = 120.0f;
    [field: SerializeField] private float CameraLookSens { get; set; } = 10.0f;
    public Transform LookTarget { get; set; }
    public Transform FollowTarget { get; set; }

    private Vector3 CameraTargetVector => Camera.transform.position - FollowTarget.position;
    private Vector3 LookFollowVector => FollowTarget.position - LookTarget.position; // look -> follow (points toward character)

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

        Vector3 followOffsetVector = LookTarget == FollowTarget ? 
                                        CameraTargetVector.normalized * PlayerFreeOffset :
                                        LookFollowVector.normalized * PlayerLockOffset;

        Camera.transform.position = FollowTarget.position + followOffsetVector;
    }

    public void Translate(Vector3 delta)
    {

    }

    public void Orbit(Vector2 delta)
    {
        if (Mathf.Abs(PlayerFreeOffset) < Mathf.Epsilon)
        {
            return;
        }

        // rotate follow vector 90 degrees to get better rotation (thank fuck it's 90)
        // Vector3 rightRotate = LookTarget != FollowTarget ? new Vector3(TargetVector.z, TargetVector.y, TargetVector.x) : -Camera.transform.right;
        if (LookTarget == FollowTarget)
        {
            Camera.transform.RotateAround(FollowTarget.position, Vector3.up, Mathf.Asin(delta.x / PlayerFreeOffset) * Mathf.Rad2Deg * CameraLookSens);
            Camera.transform.RotateAround(FollowTarget.position, -Camera.transform.right, Mathf.Asin(delta.y / PlayerFreeOffset) * Mathf.Rad2Deg * CameraLookSens);
        }
    }
}
