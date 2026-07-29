using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealth;
    [SerializeField] private ParticleSystem robotDestructionVFX;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameManager.AdjustEnemiesLeftText(1);
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth > 0) return;

        DestroyRobot();
    }

    public void DestroyRobot()
    {
        Instantiate(robotDestructionVFX, transform.position, Quaternion.identity);
        _gameManager.AdjustEnemiesLeftText(-1);
        Destroy(gameObject);
    }
}
