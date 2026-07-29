using UnityEngine;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private int playerHealth;
    [SerializeField] private Transform weaponCamera;
    [SerializeField] private Image[] shieldBars;
    [SerializeField] private GameObject gameOverContainer;

    public int PlayerCurrentHealth { get; private set; }


    private void Awake()
    {
        PlayerCurrentHealth = playerHealth;
        AdjustShieldBarsUI();
    }

    public void TakeDamage(int damage)
    {
        PlayerCurrentHealth -= damage;

        AdjustShieldBarsUI();

        if (PlayerCurrentHealth > 0) return;

        DeathCamera();
        GameOverContainer();
        Destroy(gameObject);
    }

    private void AdjustShieldBarsUI()
    {
        // Upon executing Explode() in Explosion.cs player damage
        // is passed down to TakeDamage() then this function is executed,
        // and it starts a loop, if loop iteration is more than player health,
        // rest of the Shield Bars are deactivated
        for (var i = 0; i < shieldBars.Length; i++)
        {
            shieldBars[i].gameObject.SetActive(i < PlayerCurrentHealth);
        }
    }

    private void GameOverContainer()
    {
        gameOverContainer.SetActive(true);
        var starterAssetsInputs = FindAnyObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);
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
