using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private Transform parentTransform; 
    [SerializeField] private TMP_Text mapNameText;    

    private string currentMapName;
    private string filePath;
    private LevelData levelData;

    private void Start()
    {
        currentMapName = PlayerPrefs.GetString("CurrentMap", null);

        if (string.IsNullOrEmpty(currentMapName))
        {
            Debug.LogError("No map selected for editing!");
            return;
        }

        filePath = Path.Combine(Application.persistentDataPath, currentMapName + ".json");

        if (!File.Exists(filePath))
        {
            Debug.LogError($"Map file '{currentMapName}' not found at {filePath}!");
            return;
        }


        string json = File.ReadAllText(filePath);
        levelData = JsonUtility.FromJson<LevelData>(json);

 
        if (mapNameText != null)
            mapNameText.text = $"Plaing: {levelData.levelName}";


        LoadObjects();
    }

    private void LoadObjects()
    {
        foreach (ObjectData data in levelData.objectDataList)
        {
    
            GameObject prefab = Resources.Load<GameObject>($"ActionPrefabs/{data.prefabName}");

            if (prefab != null)
            {
               
                GameObject obj = Instantiate(prefab, data.position, Quaternion.Euler(data.rotation), parentTransform);
                obj.transform.localScale = data.scale;
            }
            else
            {
                Debug.LogWarning($"Prefab '{data.prefabName}' not found in Resources/ActionPrefabs!");
            }
        }
    }
    public void ExitTheGameToMainMenu()
    {
        Debug.Log("1");
        SceneManager.LoadScene("MainMenu");
    }
}
