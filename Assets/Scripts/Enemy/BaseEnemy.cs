using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [field: SerializeField] public GameObject Target { get; set; }

    [field: SerializeField] public float BaseHealth { get; set; }
    public float CurrentHealth { get; set; }

    [field: SerializeField] public bool UseZForPathfinding { get; set; } = false;

    public float MoveSpeed { get; set; }
    public float RotationSpeed { get; set; }

    public float DistanceToTargetBeforeAttacking { get; set; }
    public bool IsRecoveringFromAttack { get; set; }
    public bool WantsToAttack { get; private set; }
    public bool IsInAttackAnimation { get; set; }

    private Vector3 Origin { get; set; }

    public void Awake()
    {
        Origin = transform.position;
    }

    public void FixedUpdate()
    {
        UpdateMovement();
        UpdateAttack();
    }

    private void UpdateMovement()
    {
        Vector3 moveTarget = ((Target == null) ? Origin : Target.transform.position);

        if (CanMove())
        {
            Vector3 movement = Vector3.MoveTowards(transform.position, moveTarget, MoveSpeed);
            if (!UseZForPathfinding)
            {
                movement.z = transform.position.z;
            }

            transform.Translate(movement);
        }

        if (CanRotate())
        {
            Quaternion rotation = Quaternion.LookRotation(moveTarget - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
        }
    }

    private bool CanMove()
    {
        return !IsInAttackAnimation;
    }

    private bool CanRotate()
    {
        return !IsInAttackAnimation;
    }

    private void UpdateAttack()
    {
        if (Target == null) { return; }

        if (!CanAttack()) { return; }
        if (!ShouldAttack()) { return; }

        BeginAttack();
    }

    private bool CanAttack()
    {
        return (!IsRecoveringFromAttack);
    }

    private bool ShouldAttack()
    {
        if (Target == null) { return false; }

        return (Vector3.Distance(Target.transform.position, transform.position) < DistanceToTargetBeforeAttacking);
    }

    private void BeginAttack()
    {
        WantsToAttack = true;
    }
}
