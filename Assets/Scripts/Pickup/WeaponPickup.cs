using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] protected WeaponSO weaponSO;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
