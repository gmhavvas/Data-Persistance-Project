using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string PlayerName;
    }

    public static int GameHighScore;
    public static string GamePlayerName;

    public InputField PlayerName;
    public Text HighScore;

    // Update is called once per frame
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            GameHighScore = data.HighScore;
            GamePlayerName = data.PlayerName;
            PlayerName.text = GamePlayerName;
            HighScore.text = "Best Score: " + GameManager.GameHighScore.ToString() + " Player Name: " + GameManager.GamePlayerName;
        }
    }

    public static void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.PlayerName = GamePlayerName;
        data.HighScore = GameHighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void StartGame()
    {
        if (GamePlayerName != PlayerName.text)
        {
            GamePlayerName = PlayerName.text;
        }

        SceneManager.LoadScene(1);
    }

    public static void Quit()
    {
            SaveHighScore();
    #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
    #else
                    Application.Quit(); // original code to quit Unity player
    #endif
    }

}
