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

    private Weapon _currentWeapon;
    private FirstPersonController _firstPersonController;

    private const string ShootAnimationString = "Shoot";

    private float _timeSinceLastShot;
    private float _defaultFOV;
    private float _defaultRotationSpeed;

    public WeaponSO CurrentWeaponSO { get; private set; }
    public int CurrentAmmo { get; private set; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        _firstPersonController = GetComponentInParent<FirstPersonController>();
    }

    private void Start()
    {
        SwitchWeapon(startingWeaponSO);
        HandleAmmo(CurrentWeaponSO.MagazineSize);

        // Initializing cooldown so we can shoot a weapon as soon as we grab it
        _timeSinceLastShot = CurrentWeaponSO.FireRate;
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
        CurrentAmmo += amount;

        if (CurrentAmmo > CurrentWeaponSO.MagazineSize)
            CurrentAmmo = CurrentWeaponSO.MagazineSize;

        ammoText.text = CurrentAmmo.ToString("D2");
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
        CurrentWeaponSO = weapon;

        HandleAmmo(CurrentWeaponSO.MagazineSize);
    }

    private void HandleShoot()
    {
        _timeSinceLastShot += Time.deltaTime;

        if (!_starterAssetsInputs.shoot) return;

        if (_timeSinceLastShot >= CurrentWeaponSO.FireRate && CurrentAmmo > 0)
        {
            _currentWeapon.Shoot(CurrentWeaponSO);
            _animator.Play(ShootAnimationString, 0, 0f);
            _timeSinceLastShot = 0f;
            HandleAmmo(-1);
        }
        // Set false so ShootInput will not spam infinitely
        if (!CurrentWeaponSO.IsAutomatic)
            _starterAssetsInputs.ShootInput(false);
    }

    private void HandleZoom()
    {
        if (!CurrentWeaponSO.CanZoom) return;

        if (_starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = CurrentWeaponSO.ZoomAmount;
            zoomVignette.gameObject.SetActive(true);
            _firstPersonController.ChangeRotationSpeed(CurrentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = _defaultFOV;
            zoomVignette.gameObject.SetActive(false);
            _firstPersonController.ChangeRotationSpeed(_defaultRotationSpeed);
        }
    }
}
