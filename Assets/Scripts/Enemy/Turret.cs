using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretTower;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float spawnInterval;

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
        var wait = new WaitForSeconds(spawnInterval);

        while (_playerHealth && _playerHealth.PlayerCurrentHealth > 0)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, turretTower.rotation);

            yield return wait;
        }
    }
}
