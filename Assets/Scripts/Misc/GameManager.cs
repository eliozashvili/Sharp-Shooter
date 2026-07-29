using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Cinemachine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject winLoseTextContainer;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private TMP_Text winLoseText;
    [SerializeField] private TMP_Text enemiesLeftText;
    [SerializeField] private Transform weaponCamera;
    [SerializeField] private GameObject player;

    private Camera _mainCam;

    private const string YouWinString = "YOU WIN!";
    private const string GameOverString = "GAME OVER";
    private const string EnemiesLeftString = "ENEMIES LEFT: ";

    private int _enemiesLeft;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    public void AdjustEnemiesLeftText(int amount)
    {
        _enemiesLeft += amount;
        enemiesLeftText.text = EnemiesLeftString + _enemiesLeft;

        GameOverContainer();
    }

    public void GameOverContainer()
    {
        bool isWin = _enemiesLeft <= 0;
        bool isLose = playerHealth.PlayerCurrentHealth <= 0;

        if (!isWin && !isLose) return;

        winLoseText.text = isWin ? YouWinString : GameOverString;

        winLoseTextContainer.SetActive(true);
        winLoseText.gameObject.SetActive(true);
        starterAssetsInputs.SetCursorState(false);

        DeathCamera();
        Time.timeScale = 0.1f;
    }

    // Freezes camera where player was looking at the moment of death
    public void DeathCamera()
    {
        if (!_mainCam) return;

        if (_mainCam.TryGetComponent(out CinemachineBrain brain))
            brain.enabled = false;

        weaponCamera.parent = null;

        player.SetActive(false);
    }

    public void RestartLevelButton()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
    }

    public void QuitGameButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
