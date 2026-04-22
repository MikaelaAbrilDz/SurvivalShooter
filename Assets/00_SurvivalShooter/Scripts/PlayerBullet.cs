using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] LayerMask enemyMask;
    int damage;
    Vector3 ogPos;
    public void Shoot(LTDescr tween, int damage, Vector3 ogPos)
    {
        GetComponent<TrailRenderer>().enabled = true;
        tween.setOnComplete(Impact);
        this.damage = damage;
        this.ogPos = ogPos;
    }
    public void Impact()
    {
        foreach (Collider enemy in Physics.OverlapSphere(transform.position, 0.5f, enemyMask))
        {
            enemy.GetComponent<EnemyBehaviour>().GetDamaged(damage);
        }
        GetComponent<TrailRenderer>().enabled = false;
        transform.position = ogPos;
    }
}
