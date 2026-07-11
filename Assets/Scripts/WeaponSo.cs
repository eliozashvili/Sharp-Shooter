using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponsSO")]
public class WeaponSO : ScriptableObject
{
    public int damage;
    public float fireRate;
}
