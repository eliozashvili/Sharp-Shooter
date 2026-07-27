using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretTower;
    [SerializeField] private Transform playerPosition;

    private void Update()
    {
        turretTower.LookAt(playerPosition);
    }
}
