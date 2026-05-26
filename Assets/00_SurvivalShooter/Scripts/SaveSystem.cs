using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    static string path = Application.persistentDataPath + "/save.json";

    [System.Serializable] class SaveData
    {
        public Vector3 playerPosition;

        public Vector3[] enemyPositions;
        public int[] enemyHealths;
    }

    public static void Save(GameData datasave)
    {
        SaveData data = new SaveData();

        data.playerPosition = datasave.playerPosition;

        data.enemyPositions = datasave.enemyPositions;
        data.enemyHealths = datasave.enemyHealths;

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

        for (int i = 0; i < datasave.enemyPositions.Length; i++)
        {
            datasave.enemyPositions[i] = data.enemyPositions[i];
            datasave.enemyHealths[i] = data.enemyHealths[i];
        }

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
