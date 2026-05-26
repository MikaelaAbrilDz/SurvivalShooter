using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public Vector3 playerPosition;
    
    public Vector3[] enemyPositions;
    public int[] enemyHealths;

    public void Reset()
    {
        playerPosition = Vector3.zero;
        enemyPositions = new Vector3[0];
        enemyHealths = new int[0];
}
}
