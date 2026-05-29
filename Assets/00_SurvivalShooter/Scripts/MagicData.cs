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
[CreateAssetMenu(fileName = "BowData", menuName = "Scriptable Objects/BowData")]
public class BowData : ScriptableObject
{
    public int damage;
    public float range;
    public float fireRate;
}
[CreateAssetMenu(fileName = "ArrowTrailData", menuName = "Scriptable Objects/ArrowTrailData")]
public class ArrowTrailData : ScriptableObject
{
    public Material material;
    public Gradient gradient;
    public AnimationCurve curve;
    public float duration;
}
