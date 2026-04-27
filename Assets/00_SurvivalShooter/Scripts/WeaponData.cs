using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public int damage;
    public float range;
    public float explosionRange;
    public float reloadTime;
    public float fireRate;
    public int maxAmmo;
}
