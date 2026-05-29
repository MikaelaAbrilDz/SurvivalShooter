using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public Vector3 playerPosition;
    public int playerLife;
    
    public Vector3[] enemyPositions;
    public int[] enemyHealths;

    public void Reset()
    {
        playerPosition = Vector3.zero;
        playerLife = 100;
        enemyPositions = new Vector3[0];
        enemyHealths = new int[0];
}
}
