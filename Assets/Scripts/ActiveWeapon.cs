using UnityEngine;
using StarterAssets;
using TMPro;
using Unity.Cinemachine;
using UnityEngine.UI;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO startingWeaponSO;
#pragma warning disable CS0618 // Type or member is obsolete
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
#pragma warning restore CS0618 // Type or member is obsolete
    [SerializeField] private Image zoomVignette;
    [SerializeField] private TMP_Text ammoText;

    private StarterAssetsInputs _starterAssetsInputs;
    private Camera _camera;
    private Animator _animator;

    private WeaponSO _currentWeaponSO;
    private Weapon _currentWeapon;
    private FirstPersonController _firstPersonController;

    private const string ShootAnimationString = "Shoot";

    private float _timeSinceLastShot;
    private float _defaultFOV;
    private float _defaultRotationSpeed;
    private int _currentAmmo;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        _firstPersonController = GetComponentInParent<FirstPersonController>();
    }

    private void Start()
    {
        SwitchWeapon(startingWeaponSO);
        HandleAmmo(_currentWeaponSO.MagazineSize);

        // Initializing cooldown so we can shoot a weapon as soon as we grab it
        _timeSinceLastShot = _currentWeaponSO.FireRate;
        _defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        _defaultRotationSpeed = _firstPersonController.RotationSpeed;
    }

    private void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void HandleAmmo(int amount)
    {
        _currentAmmo += amount;

        if (_currentAmmo > _currentWeaponSO.MagazineSize)
            _currentAmmo = _currentWeaponSO.MagazineSize;

        ammoText.text = _currentAmmo.ToString("D2");
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
        // Update _currentWeaponSO so we always have correct SO for current weapon
        _currentWeaponSO = weapon;

        HandleAmmo(_currentWeaponSO.MagazineSize);
    }

    private void HandleShoot()
    {
        _timeSinceLastShot += Time.deltaTime;

        if (!_starterAssetsInputs.shoot) return;

        if (_timeSinceLastShot >= _currentWeaponSO.FireRate && _currentAmmo > 0)
        {
            _currentWeapon.Shoot(_currentWeaponSO);
            _animator.Play(ShootAnimationString, 0, 0f);
            _timeSinceLastShot = 0f;
            HandleAmmo(-1);
        }
        // Set false so ShootInput will not spam infinitely
        if (!_currentWeaponSO.IsAutomatic)
            _starterAssetsInputs.ShootInput(false);
    }

    private void HandleZoom()
    {
        if (!_currentWeaponSO.CanZoom) return;

        if (_starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = _currentWeaponSO.ZoomAmount;
            zoomVignette.gameObject.SetActive(true);
            _firstPersonController.ChangeRotationSpeed(_currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = _defaultFOV;
            zoomVignette.gameObject.SetActive(false);
            _firstPersonController.ChangeRotationSpeed(_defaultRotationSpeed);
        }
    }
}
