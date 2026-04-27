using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] LayerMask enemyMask;
    int damage;
    float explosionRange;

    bool exploded;
    Vector3 explosionPos;
    public void Shoot(LTDescr tween, int damage, float explosionRange)
    {
        GetComponent<TrailRenderer>().enabled = true;
        tween.setOnComplete(Impact);
        this.damage = damage;
        this.explosionRange = explosionRange;
        explosionPos = tween.to;
    }
    public void Impact()
    {
        exploded = true;
        foreach (Collider enemy in Physics.OverlapSphere(transform.position, explosionRange, enemyMask))
        {
            enemy.GetComponent<EnemyBehaviour>().GetDamaged(damage);
        }
        GetComponent<TrailRenderer>().enabled = false;
        Invoke(nameof(ResetBullet), 0.01f);
    }
    void ResetBullet()
    {
        transform.localPosition = Vector3.zero;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red * 0.5f;
        if (exploded)
        {
            Gizmos.DrawSphere(explosionPos, explosionRange);
        }
    }
}
