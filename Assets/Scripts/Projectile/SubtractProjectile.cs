using UnityEngine;

public class SubtractProjectile : BaseProjectile
{
    [field: SerializeField] public int SubtractionAmount { get; set; }

    protected override void OnHitPlayer(BasePlayerController other)
    {
        other.Health -= SubtractionAmount;
        Destroy(gameObject);
    }
}
