using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager Instance;
    public string playerName = "Player 1";
    public string highScorePlayer;
    public int highScore;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string highScorePlayer;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScorePlayer = highScorePlayer;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayer = data.highScorePlayer;
            highScore = data.highScore;
        }
        else
        {
            highScorePlayer = "";
            highScore = 0;
        }
    }
}
