using UnityEngine;

public class SwordController : MonoBehaviour, IWeapon
{
    [SerializeField] SwordData swordData;
    [SerializeField] Animator swordAnim;
    public void Shoot(Transform origin, EnemyBehaviour target)
    {
        origin.LookAt(target.transform);
        swordAnim.SetTrigger("Hit");
    }
    public void Reload()
    {

    }
    public float GetRange()
    {
        return swordData.range;
    }
    public int GetDamage()
    {
        return swordData.damage;
    }
    public float GetCooldown()
    {
        return swordData.fireRate;
    }
    public void SwitchWeapon()
    {

    }
}
