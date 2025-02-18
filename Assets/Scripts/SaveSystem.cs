using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    public int kills = 0;
    public int deaths = 0;
    public int width = 1920;
    public int height = 1080;
    public bool fullScreen = true;

    private bool isDataLoaded = false;

    private void Awake()
    {
        // Singleton: гарантируем, что есть только один экземпляр SaveSystem
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        GameData data = new GameData
        {
            kills = kills,
            deaths = deaths,
            width = width,
            height = height,
            fullscreen = fullScreen
        };
        Save(data);
    }

    public void LoadGameOnce()
    {
        if (!isDataLoaded)
        {
            GameData data = Load();
            if (data != null)
            {
                kills = data.kills;
                deaths = data.deaths;
                width = data.width;
                height = data.height;
                fullScreen = data.fullscreen;
            }
            isDataLoaded = true;
        }
    }

    private void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/statistics.json", json);
        
        //Debug.Log("Game saved to: " + Application.persistentDataPath + "/statistics.json");
    }

    private GameData Load()
    {
        string path = Application.persistentDataPath + "/statistics.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            //Debug.Log("Game loaded from: " + path);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }
}
