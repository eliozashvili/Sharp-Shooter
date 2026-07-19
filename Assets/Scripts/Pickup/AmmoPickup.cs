using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] private int ammoAmount;

    private ActiveWeapon _activeWeapon;

    private void Start()
    {
        _activeWeapon = FindAnyObjectByType<ActiveWeapon>();
    }

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        if (_activeWeapon.CurrentAmmo == _activeWeapon.CurrentWeaponSO.MagazineSize) return;

        activeWeapon.HandleAmmo(ammoAmount);

        Destroy(gameObject);
    }
}
