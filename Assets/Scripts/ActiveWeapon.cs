using UnityEngine;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    private StarterAssetsInputs _starterAssetsInputs;
    private Camera _camera;
    private Animator _animator;

    private Weapon _currentWeapon;

    private const string ShootAnimation = "Shoot";

    private void Awake()
    {
        _starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!_starterAssetsInputs.shoot) return;

        _currentWeapon.Shoot(weaponSO);
        _animator.Play(ShootAnimation, 0, 0f);

        // Set false so ShootInput will not spam infinitely
        _starterAssetsInputs.ShootInput(false);
    }
}
