using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private float projectileSpeed;

    private Rigidbody _rb;

    private int _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.linearVelocity = transform.forward * projectileSpeed;
    }

    public void Init(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerHealth = other.gameObject.GetComponentInParent<PlayerHealth>();

        playerHealth?.TakeDamage(_damage);

        Instantiate(hitVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
