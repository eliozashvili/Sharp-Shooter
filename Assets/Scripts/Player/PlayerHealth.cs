using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private int playerHealth;
    [SerializeField] private Image[] shieldBars;
    [SerializeField] private GameManager gameManager;

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

        gameManager.DeathCamera();
        gameManager.GameOverContainer();
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
}
