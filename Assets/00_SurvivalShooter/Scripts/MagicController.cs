using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MagicController : MonoBehaviour, IWeapon
{
    [SerializeField] MagicData magicData;
    [SerializeField] PlayerBullet bullet;
    [SerializeField] Transform shootPoint;
    public void Shoot(Transform origin, EnemyBehaviour target)
    {
        origin.LookAt(target.transform);
        LTDescr tween = LeanTween.move(bullet.gameObject, target.transform.position + Vector3.up, magicData.fireRate / 5f);
        bullet.Shoot(tween, magicData.damage, magicData.explosionRange);
    }
    public void Reload()
    {

    }
    public float GetRange()
    {
        return magicData.range;
    }
    public int GetDamage()
    {
        return magicData.damage;
    }
    public float GetCooldown()
    {
        return magicData.fireRate;
    }
    public void SwitchWeapon()
    {

    }

}
