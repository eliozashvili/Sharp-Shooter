using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealth;

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth > 0) return;

        Destroy(gameObject);
    }
}
