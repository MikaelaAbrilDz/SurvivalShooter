using UnityEngine;
using System.Collections;

public class BowController : MonoBehaviour, IWeapon
{
    [SerializeField] BowData bowData;
    [SerializeField] ArrowTrailData trailData;
    [SerializeField] Transform shootPoint;
    [SerializeField] LayerMask enemyMask;
    public void Shoot(Transform origin, EnemyBehaviour target)
    {
        origin.LookAt(target.transform);
        GameObject arrow = new GameObject("arrow");
        arrow.transform.position = shootPoint.position;
        TrailRenderer trail = arrow.AddComponent<TrailRenderer>();

        trail.material = trailData.material;
        trail.colorGradient = trailData.gradient;
        trail.widthCurve = trailData.curve;
        trail.time = trailData.duration;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        LeanTween.move(trail.gameObject, target.transform.position, (shootPoint.position - target.transform.position).magnitude / 5);
        StartCoroutine(DestroyArrow(arrow, (shootPoint.position - target.transform.position).magnitude / 5));
        StartCoroutine(CheckEnemies(arrow));
    }
    private IEnumerator DestroyArrow(GameObject arrow, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (arrow) Destroy(arrow);
    }
    private IEnumerator CheckEnemies(GameObject arrow)
    {
        while (arrow)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            foreach (var enemy in Physics.OverlapSphere(arrow.transform.position, 0.01f, enemyMask))
            {
                enemy.GetComponent<EnemyBehaviour>().GetDamaged(GetDamage());
                Destroy(arrow);
            }
        }
    }
    public void Reload()
    {

    }
    public float GetRange()
    {
        return bowData.range;
    }
    public int GetDamage()
    {
        return bowData.damage;
    }
    public float GetCooldown()
    {
        return bowData.fireRate;
    }
    public void SwitchWeapon()
    {

    }
}
