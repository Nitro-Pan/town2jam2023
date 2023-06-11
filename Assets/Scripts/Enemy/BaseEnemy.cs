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

    public AudioSource[] spiderHitsSFX;

    public bool IsRecoveringFromAttack { get; set; }
    public bool WantsToAttack { get; private set; }
    public bool IsInAttackAnimation { get; set; }

    private Vector3 Origin { get; set; }

    public void Awake()
    {
        Origin = transform.position;
    }

    private float curTime = 0.0f;

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

        curTime += Time.deltaTime;
        if (curTime > 5.0f)
        {
            Attack();
            curTime = 0.0f;
        }
    }
    
    public void OnTakenDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        CycleThroughSFX.playSFX(spiderHitsSFX);
    }

    protected virtual void Attack()
    {
    }
}
