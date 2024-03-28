using Assets.Scripts.Dto;
using UnityEngine;

public static class SaveManager
{
    public static void Save<T>(string key, T data) where T : SaveData
    {
        string jsonDataString = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static T Load<T>(string key) where T : SaveData, new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string jsonDataString = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(jsonDataString);
        }
        return new T();
    }
}
