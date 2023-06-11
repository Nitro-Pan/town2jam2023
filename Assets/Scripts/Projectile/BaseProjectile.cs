using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public Collider Collider { get; private set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float LifeSpan { get; set; } = 10.0f;

    public void Awake()
    {
        Collider = GetComponent<Collider>();
    }

    public void FixedUpdate()
    {
        LifeSpan -= Time.deltaTime;
        if (LifeSpan <= 0.0f)
        {
            Destroy(gameObject);
        }

        transform.Translate(new Vector3(Speed, 0.0f, 0.0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        BasePlayerController other = collision.gameObject.GetComponent<BasePlayerController>();

        OnHitPlayer(other);
    }

    protected virtual void OnHitPlayer(BasePlayerController other)
    {
    }
}
