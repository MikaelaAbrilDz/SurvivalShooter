using UnityEngine;

public class SlowPickUp : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] float speedMult = .5f;
    void Start()
    {
        if (gameData.enemySlowCollected) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            gameData.enemySlowCollected = true;
            gameData.enemySpeedMultiplier = speedMult;


            foreach (var enemy in FindObjectsByType<EnemyBehaviour>(FindObjectsSortMode.None))
            {
                enemy.agent.speed *= speedMult;
            }
            Destroy(gameObject);
        }
    }
}
