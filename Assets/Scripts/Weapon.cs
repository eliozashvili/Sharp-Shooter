using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int weaponDamage;

    private StarterAssetsInputs _starterAssetsInputs;
    private Camera _camera;

    private void Awake()
    {
        _starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        HandleRaycast();
    }

    private void HandleRaycast()
    {
        if (!_starterAssetsInputs.shoot) return;
        // Checks if Raycast hit the GameObject and fills
        // RaycastHit variable with information about GameObject
        bool isHit = Physics.Raycast
        (
            _camera.transform.position,
            _camera.transform.forward,
            out RaycastHit hit,
            Mathf.Infinity
        );
        // Checks if Raycast hit GameObject and if that
        // GameObject has EnemyHealth component
        if (isHit && hit.collider.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(weaponDamage);

            Debug.Log("Damage dealt " + weaponDamage + " to " + hit.transform.name);
        }
        // Set false so ShootInput will not spam infinitely
        _starterAssetsInputs.ShootInput(false);
    }
}
