using UnityEngine;

public interface IWeapon
{
    public void Shoot(Transform origin, EnemyBehaviour target)
    {

    }
    public void Reload()
    {

    }
    public float GetRange()
    {
        return 10f;
    }
    public int GetDamage()
    {
        return 10;
    }
    public void SwitchWeapon()
    {

    }

}
