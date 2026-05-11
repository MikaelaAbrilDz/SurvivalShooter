using UnityEngine;

public class SpeedPickUp : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] float speedMult = 2;
    void Start()
    {
        if (gameData.playerSpeedCollected) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            gameData.playerSpeedCollected = true;
            gameData.playerSpeedMultiplier = speedMult;

            player.agent.speed *= speedMult;
            Destroy(gameObject);
        }
    }
}
