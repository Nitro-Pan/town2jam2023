using UnityEngine;

public class SpiderBossEnemy : BaseEnemy
{
    [field: SerializeField] public GameObject SubtractionProjectile { get; set; }
    [field: SerializeField] public int NumProjectiles { get; set; } = 5;
    [field: SerializeField] public float AngleBetweenProjectiles { get; set; } = 5.0f;
    [field: SerializeField] public float ShootRandomness { get; set; } = 3.0f;
    [field: SerializeField] public int ShotsPerAttack { get; set; } = 3;
    [field: SerializeField] public float BaseTimeBetweenShots { get; set; } = 1.0f;
    [field: SerializeField] public float JumpSpeed { get; set; } = 5.0f;
    [field: SerializeField] public float DropSpeed { get; set; } = 5.0f;
    [field: SerializeField] public float JumpHeight { get; set; } = 5.0f;
    [field: SerializeField] public float BaseSlamRecoveryTime { get; set; } = 3.0f;
    private float CurrentSlamRecoveryTime { get; set; }

    private float CurrentTimeBetweenShots { get; set; } = 0.0f;
    private int CurrentShots { get; set; } = 0;
    private bool ReachedPlayerForSlam { get; set; } = false;
    private Vector3 SlamPosition { get; set; }

    private Collider Collider { get; set; }
    private Animator Animator { get; set; }

    private enum SpiderBossAttackType
    {
        None,
        
        Shoot,
        Slam,

        Count
    }

    public override void Awake()
    {
        base.Awake();
        Collider = GetComponent<Collider>();
        Collider.enabled = false;
        Animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Animator != null)
        {
            Animator.SetBool("IsSlamming", CurrentAttackType == SpiderBossAttackType.Slam);
            Animator.SetBool("IsShooting", CurrentAttackType == SpiderBossAttackType.Shoot);
            Animator.SetBool("IsDead", CurrentHealth <= 0.0f);
        }

    }

    private SpiderBossAttackType CurrentAttackType { get; set; } = SpiderBossAttackType.None;

    SpiderBossAttackType ChooseAttack()
    {
        int attackIndex = Random.Range((int)(SpiderBossAttackType.None) + 1, (int)SpiderBossAttackType.Count);
        return (SpiderBossAttackType)((int)SpiderBossAttackType.None + attackIndex);
    }

    protected override void Attack()
    {
        base.Attack();

        CurrentAttackType = ChooseAttack();
        CurrentShots = 0;
        ReachedPlayerForSlam = false;
        CurrentSlamRecoveryTime = 0.0f;
        CurrentTimeBetweenShots = 0.0f;
        if (Target == null)
        {
            SlamPosition = Origin;
        }
        else
        {
            SlamPosition = Target.transform.position;
        }
    }

    protected override bool UpdateAttackInProgress()
    {
        switch(CurrentAttackType)
        {
            case SpiderBossAttackType.Shoot:
            {
                CurrentTimeBetweenShots -= Time.deltaTime;
                if (CurrentTimeBetweenShots > 0.0f ) { break; }
                
                for (int i = 1; i <= NumProjectiles + 1; ++i)
                {
                    float modifier = i * Mathf.Pow(-1, i);
                    float angle = (transform.rotation.eulerAngles.y + (modifier * AngleBetweenProjectiles)) - 90.0f;

                    float randomness = ShootRandomness - (ShootRandomness / 2.0f);
                    angle += randomness;
                    Instantiate(SubtractionProjectile, transform.position, Quaternion.Euler(0.0f, angle, 0.0f));
                }

                ++CurrentShots;

                CurrentTimeBetweenShots += BaseTimeBetweenShots;
                if (CurrentShots >= ShotsPerAttack)
                {
                    CurrentAttackType = SpiderBossAttackType.None;
                    return true;
                }

                break;
            }

            case SpiderBossAttackType.Slam:
            {
                if (!ReachedPlayerForSlam)
                {
                    Vector3 target = SlamPosition;
                    target += new Vector3(0.0f, JumpHeight);
                    Vector3 movement = Vector3.MoveTowards(transform.position, target, JumpSpeed);
                    if (Vector3.Distance(transform.position, movement) <= JumpSpeed / 2.0f)
                    {
                        ReachedPlayerForSlam = true;
                        Collider.enabled = true;
                    }
                    else
                    {
                        transform.position = movement;
                    }
                }
                else if (!IsRecoveringFromSlam())
                {
                    transform.Translate(new Vector3(0.0f, -DropSpeed));

                    if (transform.position.y <= Origin.y)
                    {
                        transform.position = new Vector3(transform.position.x, Origin.y, transform.position.z);
                        CurrentSlamRecoveryTime = BaseRecoveryTime;
                    }
                }
                else
                {
                    Collider.enabled = false;

                    CurrentSlamRecoveryTime -= Time.deltaTime;

                    if (!IsRecoveringFromSlam())
                    {
                        CurrentAttackType = SpiderBossAttackType.None;
                        return true;
                    }
                }
                break;
            }
        }

        return false;
    }

    bool IsRecoveringFromSlam() => (CurrentSlamRecoveryTime > 0.0f);

    public void OnTriggerEnter(Collider other)
    {
        BasePlayerController player = other.gameObject.GetComponent<BasePlayerController>();
        if (player == null) { return; }

        Collider.enabled = false;

        player.Health = (int)(player.Health / 2.0f);
    }
}
