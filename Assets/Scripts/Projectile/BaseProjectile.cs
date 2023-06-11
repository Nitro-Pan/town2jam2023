using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public Collider Collider { get; private set; }
    [field: SerializeField] public float Speed { get; set; }

    public void Awake()
    {
        Collider = GetComponent<Collider>();
    }

    public void FixedUpdate()
    {
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
