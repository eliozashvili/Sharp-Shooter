using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlashVFX;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Shoot(WeaponSO weaponSO)
    {
        // Checks if Raycast hit the GameObject and fills
        // RaycastHit variable with information about GameObject
        bool isHit = Physics.Raycast
        (
            _camera.transform.position,
            _camera.transform.forward,
            out RaycastHit hit,
            Mathf.Infinity
        );
        muzzleFlashVFX.Play();
        // Checks if Raycast hit GameObject and if that
        // GameObject has EnemyHealth component
        if (isHit && hit.collider.TryGetComponent(out EnemyHealth enemyHealth))
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
