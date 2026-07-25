using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float radius;

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
        // deal damage to player
    }
}
