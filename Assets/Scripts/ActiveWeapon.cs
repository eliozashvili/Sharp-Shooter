using UnityEngine;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    private StarterAssetsInputs _starterAssetsInputs;
    private Camera _camera;
    private Animator _animator;

    private Weapon _currentWeapon;

    private const string ShootAnimationString = "Shoot";

    private float _timeSinceLastShot;

    private void Awake()
    {
        _starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentWeapon = GetComponentInChildren<Weapon>();
        // Initializing cooldown so we can shoot a weapon as soon as we grab it
        _timeSinceLastShot = weaponSO.FireRate;
    }

    private void Update()
    {
        _timeSinceLastShot += Time.deltaTime;

        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!_starterAssetsInputs.shoot || _timeSinceLastShot <= weaponSO.FireRate) return;

        _currentWeapon.Shoot(weaponSO);
        _animator.Play(ShootAnimationString, 0, 0f);

        _timeSinceLastShot = 0f;
        // Set false so ShootInput will not spam infinitely
        _starterAssetsInputs.ShootInput(false);
    }
}
