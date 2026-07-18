using UnityEngine;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine.UI;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
#pragma warning disable CS0618 // Type or member is obsolete
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
#pragma warning restore CS0618 // Type or member is obsolete
    [SerializeField] private Image zoomVignette;

    private StarterAssetsInputs _starterAssetsInputs;
    private Camera _camera;
    private Animator _animator;

    private Weapon _currentWeapon;
    private FirstPersonController _firstPersonController;

    private const string ShootAnimationString = "Shoot";

    private float _timeSinceLastShot;
    private float _defaultFOV;
    private float _defaultRotationSpeed;

    private void Awake()
    {
        _currentWeapon = GetComponentInChildren<Weapon>();
        _animator = GetComponentInChildren<Animator>();
        _starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        _firstPersonController = GetComponentInParent<FirstPersonController>();
    }

    private void Start()
    {
        // Initializing cooldown so we can shoot a weapon as soon as we grab it
        _timeSinceLastShot = weaponSO.FireRate;
        _defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        _defaultRotationSpeed = _firstPersonController.RotationSpeed;
    }

    private void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void SwitchWeapon(WeaponSO weapon)
    {
        // Destroy weapon player holds currently in hands
        if (_currentWeapon)
            Destroy(_currentWeapon.gameObject);
        // When player triggers pickup, instantiate picked up weapon
        // and child it to Active Weapon Game Object
        Weapon newWeapon = Instantiate(weapon.WeaponPrefab, transform);
        // Update animator for new weapon
        _animator = newWeapon.GetComponentInChildren<Animator>();
        // Update current weapon
        _currentWeapon = newWeapon;
        // Update weaponSO so we always have correct SO for current weapon
        weaponSO = weapon;
    }

    private void HandleShoot()
    {
        _timeSinceLastShot += Time.deltaTime;

        if (!_starterAssetsInputs.shoot || _timeSinceLastShot <= weaponSO.FireRate) return;

        _currentWeapon.Shoot(weaponSO);
        _animator.Play(ShootAnimationString, 0, 0f);

        _timeSinceLastShot = 0f;

        // Set false so ShootInput will not spam infinitely
        if (!weaponSO.IsAutomatic)
            _starterAssetsInputs.ShootInput(false);
    }

    private void HandleZoom()
    {
        if (!weaponSO.CanZoom) return;

        if (_starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            zoomVignette.gameObject.SetActive(true);
            _firstPersonController.ChangeRotationSpeed(weaponSO.ZoomRotationSpeed);
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = _defaultFOV;
            zoomVignette.gameObject.SetActive(false);
            _firstPersonController.ChangeRotationSpeed(_defaultRotationSpeed);
        }
    }
}
