using UnityEngine;
using System.Collections;

public class SpawnerGate : MonoBehaviour
{
    [SerializeField] private Transform enemiesParentGOTransform;
    [SerializeField] private Robot robotPrefab;
    [SerializeField] private float spawnInterval;

    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = FindAnyObjectByType<PlayerHealth>();

        StartCoroutine(SpawnRobots());
    }

    private IEnumerator SpawnRobots()
    {
        var wait = new WaitForSeconds(spawnInterval);

        while (_playerHealth && _playerHealth.PlayerCurrentHealth > 0)
        {
            Instantiate(robotPrefab, transform.position, transform.rotation, enemiesParentGOTransform);

            yield return wait;
        }
    }
}
