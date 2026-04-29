using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WeaponController : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] PlayerBullet bullet;
    [SerializeField] Transform shootPoint;
    public void Shoot(Transform origin, EnemyBehaviour target)
    {
        origin.LookAt(target.transform);
        LTDescr tween = LeanTween.move(bullet.gameObject, target.transform.position + Vector3.up, weaponData.fireRate / 5f);
        bullet.Shoot(tween, weaponData.damage, weaponData.explosionRange);
    }
    public void Reload()
    {

    }
    public float GetRange()
    {
        return weaponData.range;
    }
    public int GetDamage()
    {
        return weaponData.damage;
    }
    public float GetCooldown()
    {
        return weaponData.fireRate;
    }
    public void SwitchWeapon()
    {

    }

}
