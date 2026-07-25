using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Robot : MonoBehaviour
{
    private FirstPersonController _player;
    private NavMeshAgent _agent;

    private const string PlayerString = "Player";

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _player = FindAnyObjectByType<FirstPersonController>();
    }

    private void Update()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        _agent.SetDestination(_player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerString)) return;

        var enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.DestroyRobot();
    }
}
