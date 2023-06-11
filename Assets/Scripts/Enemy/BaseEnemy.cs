using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [field: SerializeField] public GameObject Target { get; set; }

    [field: SerializeField] public int BaseHealth { get; set; }
    public int CurrentHealth { get; set; }

    [field: SerializeField] public bool UseYForPathfinding { get; set; } = false;

    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float RotationSpeed { get; set; }
    [field: SerializeField] public float MinDistanceFromTarget { get; set; }
    [field: SerializeField] public float DistanceToTargetBeforeAttacking { get; set; }
    [field: SerializeField] public float BaseTimeBetweenAttacks { get; set; }
    private float CurrentTimeBetweenAttacks { get; set; }

    public bool IsAttacking { get; set; }

    protected Vector3 Origin { get; set; }

    [field: SerializeField] public float BaseRecoveryTime { get; set; }
    private float CurrentRecoveryTime { get; set; }

    public virtual void Awake()
    {
        Origin = transform.position;
    }


    public void FixedUpdate()
    {
        if (CurrentHealth <= 0.0f) { return; }

        UpdateMovement();
        UpdateAttack();
    }

    private void UpdateMovement()
    {
        Vector3 moveTarget = ((Target == null) ? Origin : Target.transform.position);

        if (CanMove())
        {
            Vector3 offsetTarget = (transform.position - moveTarget);
            offsetTarget.Normalize();
            offsetTarget = moveTarget + (offsetTarget * MinDistanceFromTarget);

            Vector3 movement = Vector3.MoveTowards(transform.position, offsetTarget, MoveSpeed );
            if (!UseYForPathfinding)
            {
                movement.y = transform.position.y;
            }

            transform.position = movement;
        }

        if (CanRotate())
        {
            Vector3 lookTarget = (moveTarget - transform.position);
            lookTarget.y = 0.0f;
            Quaternion rotation = Quaternion.LookRotation(lookTarget);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
        }
    }

    private bool CanMove()
    {
        return !IsAttacking && !IsRecoveringFromAttack();
    }

    private bool CanRotate()
    {
        return !IsAttacking && !IsRecoveringFromAttack();
    }

    private void UpdateAttack()
    {
        if (Target == null) { return; }

        if (IsAttacking)
        {
            bool finished = UpdateAttackInProgress();
            if (finished)
            {
                IsAttacking = false;
                CurrentRecoveryTime = BaseRecoveryTime;
            }
        }
        else if (IsRecoveringFromAttack())
        {
            UpdateRecovery();
        }
        else
        {
            if (CanAttack())
            {
                IsAttacking = true;
                Attack();
                CurrentTimeBetweenAttacks = BaseTimeBetweenAttacks;
            }

            CurrentTimeBetweenAttacks -= Time.deltaTime;
        }

    }

    public bool IsRecoveringFromAttack()
    {
        return (CurrentRecoveryTime > 0.0f);
    }

    private void UpdateRecovery()
    {
        if (!IsRecoveringFromAttack()) { return; }

        CurrentRecoveryTime -= Time.deltaTime;
    }

    private bool CanAttack()
    {
        return (!IsAttacking && !IsRecoveringFromAttack() && (CurrentTimeBetweenAttacks <= 0.0f));
    }

    private bool ShouldAttack()
    {
        if (Target == null) { return false; }

        return (Vector3.Distance(Target.transform.position, transform.position) < DistanceToTargetBeforeAttacking);
    }

    public void OnTakenDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
    }

    protected virtual void Attack()
    {
    }

    protected virtual bool UpdateAttackInProgress()
    {
        return true;
    }
}
