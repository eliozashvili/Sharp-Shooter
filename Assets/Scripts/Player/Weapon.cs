using UnityEngine;
using Unity.Cinemachine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlashVFX;
    [SerializeField] private LayerMask layerMask;

    private Camera _camera;
    private CinemachineImpulseSource _impulseSource;
    private Animator _animator;

    private const string ShootAnimationString = "Shoot";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Shoot(WeaponSO weaponSO, bool isZoomed)
    {
        // Checks if Raycast hit the GameObject and fills
        // RaycastHit variable with information about GameObject
        bool isHit = Physics.Raycast
        (
            _camera.transform.position,
            _camera.transform.forward,
            out RaycastHit hit,
            Mathf.Infinity,
            layerMask.value,
            QueryTriggerInteraction.Ignore
        );

        muzzleFlashVFX.Play();
        _impulseSource.GenerateImpulse();
        _animator.Play(ShootAnimationString, 0, 0f);

        // Checks if Raycast hit GameObject and if that
        // GameObject has EnemyHealth component
        if (!isHit) return;

        if (hit.collider.TryGetComponent(out EnemyHealth enemyHealth))
        {
            Instantiate(weaponSO.HitVFX, hit.point, Quaternion.identity, hit.transform);
            enemyHealth.TakeDamage(weaponSO.Damage);
        }
        else
        {
            Instantiate(weaponSO.HitVFX, hit.point, Quaternion.identity);
        }
    }
}
