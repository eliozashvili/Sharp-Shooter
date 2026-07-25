using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealth;
    [SerializeField] private ParticleSystem robotDestructionVFX;

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth > 0) return;

        DestroyRobot();
    }

    public void DestroyRobot()
    {
        Instantiate(robotDestructionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
