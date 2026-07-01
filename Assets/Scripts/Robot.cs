using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Robot : MonoBehaviour
{
    private FirstPersonController _player;
    private NavMeshAgent _agent;

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
        _agent.SetDestination(_player.transform.position);
    }
}
