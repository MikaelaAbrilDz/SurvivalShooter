using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    SwordController controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<SwordController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) other.GetComponent<EnemyBehaviour>().GetDamaged(controller.GetDamage());
    }
}
