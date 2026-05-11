using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    static string path = Application.persistentDataPath + "/save.json";

    [System.Serializable] class SaveData
    {
        public Vector3 playerPosition;
        public float playerSpeedMultiplier;

        public float enemySpeedMultiplier;


        public bool playerSpeedCollected;
        public bool enemySlowCollected;
    }

    public static void Save(GameData datasave)
    {
        SaveData data = new SaveData();

        data.playerPosition = datasave.playerPosition;
        data.playerSpeedMultiplier = datasave.playerSpeedMultiplier;

        data.enemySpeedMultiplier = datasave.enemySpeedMultiplier;

        data.playerSpeedCollected = datasave.playerSpeedCollected;
        data.enemySlowCollected = datasave.enemySlowCollected;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);

        print("PARTIDA GUARDADA");
    }
    public static bool Load(GameData datasave, GameObject player)
    {
        if (!File.Exists(path)) return false;

        string json = File.ReadAllText(path);

        SaveData data = JsonUtility.FromJson<SaveData>(json);

        datasave.playerPosition = data.playerPosition;
        datasave.playerSpeedMultiplier = data.playerSpeedMultiplier;

        datasave.enemySpeedMultiplier = data.enemySpeedMultiplier;

        datasave.playerSpeedCollected = data.playerSpeedCollected;
        datasave.enemySlowCollected = data.enemySlowCollected;

        print("PARTIDA CARGADA");

        return true;

    }
    public static void DeleteSave()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            print("PARTIDA BORRADA");
        }

    }
}
