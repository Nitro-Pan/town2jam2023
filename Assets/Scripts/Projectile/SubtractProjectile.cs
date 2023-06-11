using UnityEngine;

public class SubtractProjectile : BaseProjectile
{
    [field: SerializeField] public float SubtractionAmount { get; set; }

    protected override void OnHitPlayer(BasePlayerController other)
    {
        Destroy(this);
    }
}
