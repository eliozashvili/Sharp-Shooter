using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
        activeWeapon.SwitchWeapon(weaponSO);

        Destroy(gameObject);
    }
}
