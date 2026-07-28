using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretTower;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileSpawnInterval;
    [SerializeField] private int damage;

    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = FindAnyObjectByType<PlayerHealth>();

        StartCoroutine(FireProjectile());
    }

    private void Update()
    {
        turretTower.LookAt(playerPosition);
    }

    private IEnumerator FireProjectile()
    {
        var wait = new WaitForSeconds(projectileSpawnInterval);

        while (_playerHealth && _playerHealth.PlayerCurrentHealth > 0)
        {
            Projectile newProjectile =
                Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            newProjectile.transform.LookAt(playerPosition);
            newProjectile.Init(damage);

            yield return wait;
        }
    }
}
