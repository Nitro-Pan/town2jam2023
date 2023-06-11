using UnityEngine;

public class SpiderBossEnemy : BaseEnemy
{
    [field: SerializeField] public GameObject SubtractionProjectile { get; set; }
    [field: SerializeField] public int NumProjectiles { get; set; } = 5;
    [field: SerializeField] public float AngleBetweenProjectiles { get; set; } = 5.0f;

    protected override void Attack()
    {
        base.Attack();

        for (int i = 1; i <= NumProjectiles; ++i)
        {
            float modifier = i * (-1 * (i % 2));
            float angle = (transform.rotation.eulerAngles.y + (modifier * AngleBetweenProjectiles)) - 90.0f;
            Instantiate(SubtractionProjectile, transform.position, Quaternion.Euler(0.0f, angle, 0.0f));
        }
    }
}
