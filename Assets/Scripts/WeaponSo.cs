// ReSharper disable InconsistentNaming
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponsSO")]
public class WeaponSO : ScriptableObject
{
    public ParticleSystem HitVFX;

    public int Damage;
    public float FireRate;
    public bool IsAutomatic;
}
