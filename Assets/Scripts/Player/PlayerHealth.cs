using UnityEngine;
using Unity.Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform weaponCamera;
    [SerializeField] private int playerHealth;

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            DeathCamera();
            Destroy(gameObject);
        }

        Debug.Log(playerHealth);
    }
    // Freezes camera where player was looking at the moment of death
    private void DeathCamera()
    {
        Camera mainCam = Camera.main;

        if (!mainCam) return;

        if (mainCam.TryGetComponent(out CinemachineBrain brain))
            brain.enabled = false;

        weaponCamera.parent = null;
    }
}
