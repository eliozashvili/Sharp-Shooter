using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSo weaponSo;
    [SerializeField] private ParticleSystem muzzleFlashVFX;
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private Animator animator;

    private StarterAssetsInputs _starterAssetsInputs;
    private Camera _camera;

    private const string ShootAnimation = "Shoot";

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

        muzzleFlashVFX.Play();
        animator.Play(ShootAnimation, 0, 0f);
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
            Instantiate(hitVFX, hit.point, Quaternion.identity, hit.transform);
            enemyHealth.TakeDamage(weaponSo.damage);
        }
        else
        {
            Instantiate(hitVFX, hit.point, Quaternion.identity);
        }
        // Set false so ShootInput will not spam infinitely
        _starterAssetsInputs.ShootInput(false);
    }
}
