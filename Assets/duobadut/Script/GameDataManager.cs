using System.IO;
using UnityEngine;

public static class GameDataManager
{

    private static readonly string SAVE_FILE_NAME = "game_object_data.json";

    public static void SaveGameObject(string json)
    {
        string filePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        File.WriteAllText(filePath, json);
    }

    public static GameObject LoadGameObject()
    {
        string filePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameObject>(json);
        }
        else
        {
            return null;
        }
    }
}
