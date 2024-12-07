using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateMap : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string editorSceneName = "Editor"; 

    private void Start()
    {
     
        if (string.IsNullOrEmpty(editorSceneName))
        {
            Debug.LogError("Editor scene name is not set!");
        }
    }

    public void CreateNewMap()
    {
        string mapName = inputField.text.Trim();

        if (string.IsNullOrEmpty(mapName))
        {
            Debug.LogWarning("Map name cannot be empty!");
            return;
        }

 
        string filePath = Path.Combine(Application.persistentDataPath, mapName + ".json");

        if (File.Exists(filePath))
        {
            Debug.LogError($"Map '{mapName}' already exists!");
            return;
        }

    
        LevelData newLevel = new LevelData
        {
            levelName = mapName,
            objectDataList = new System.Collections.Generic.List<ObjectData>()
        };

        string json = JsonUtility.ToJson(newLevel, true);
        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log($"Map '{mapName}' created and saved to {filePath}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to save map '{mapName}': {e.Message}");
            return;
        }

        PlayerPrefs.SetString("CurrentMap", mapName);
        PlayerPrefs.Save();

        SceneManager.LoadScene(editorSceneName);
    }
}
