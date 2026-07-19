// ReSharper disable InconsistentNaming
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponsSO")]
public class WeaponSO : ScriptableObject
{
    public Weapon WeaponPrefab;
    public ParticleSystem HitVFX;

    public int Damage;
    public float FireRate;
    public bool IsAutomatic;
    public bool CanZoom;
    public float ZoomAmount;
    public float ZoomRotationSpeed;
    public int MagazineSize;
}
