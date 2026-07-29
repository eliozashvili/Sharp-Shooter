using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int explosionDamage;

    private void Start()
    {
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    private void Explode()
    {
        // ReSharper disable once Unity.PreferNonAllocApi
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hitColliders)
        {
            if (hit.TryGetComponent(out PlayerHealth playerHealth))
                playerHealth.TakeDamage(explosionDamage);
        }
    }
}
