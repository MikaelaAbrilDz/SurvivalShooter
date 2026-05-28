using UnityEngine;

[CreateAssetMenu(fileName = "MagicData", menuName = "Scriptable Objects/MagicData")]
public class MagicData : ScriptableObject
{
    public int damage;
    public float range;
    public float explosionRange;
    public float reloadTime;
    public float fireRate;
    public int maxAmmo;
}
[CreateAssetMenu(fileName = "SwordData", menuName = "Scriptable Objects/SwordData")]
public class SwordData : ScriptableObject
{
    public int damage;
    public float range;
    public float fireRate;
}
