using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UIElements;

public class Data : MonoBehaviour
{
    public static Data Instance;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI playerNameText;
    public string playerName;
    public int newHighScore;
    public string newHighScoreName;
 
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadNewBestScore();
    }
    private void Start()
    {
        if (newHighScoreName!= null) {
            bestScore.text = "Best Score : " + newHighScoreName + " : " + newHighScore;
        } 
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif

    }
    public void StartGame()
    {
        playerName = playerNameText.text;
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int newHighScore;
        public string newHighScoreName;
    }

    public void SaveNewbBestScore()
    {
        SaveData data = new SaveData();
        data.newHighScore = newHighScore;
        data.newHighScoreName = newHighScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadNewBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            newHighScore = data.newHighScore;
            newHighScoreName = data.newHighScoreName;
        }
    }
}
