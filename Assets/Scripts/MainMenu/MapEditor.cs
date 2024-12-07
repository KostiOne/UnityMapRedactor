using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEditor : MonoBehaviour
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
            mapNameText.text = $"Editing: {levelData.levelName}";


        LoadObjects();
    }

    private void LoadObjects()
    {
        foreach (ObjectData data in levelData.objectDataList)
        {
            GameObject prefab = Resources.Load<GameObject>($"ActionPrefabs/{data.prefabName}");

            if (data.prefabName == "Terrarian") 
            {
                Debug.Log($"Skipping prefab '{data.prefabName}'");
                continue; 
            }

            if (prefab != null)
            {
                // Если объект найден, создаём его
                GameObject obj = Instantiate(prefab, data.position, Quaternion.Euler(data.rotation), parentTransform);
                obj.transform.localScale = data.scale; 
            }
            else
            {
                Debug.LogWarning($"Prefab '{data.prefabName}' not found in Resources/ActionPrefabs!");
            }
        }
    }

    public void SaveMap()
    {
        levelData.objectDataList.Clear();

        foreach (Transform child in parentTransform)
        {
      
            string prefabName = child.name.Replace("(Clone)", "").Trim();

            ObjectData data = new ObjectData
            {
                prefabName = prefabName, 
                position = child.position,
                rotation = child.rotation.eulerAngles,
                scale = child.localScale 
            };

            levelData.objectDataList.Add(data);
        }

        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(filePath, json);

        Debug.Log($"Map '{currentMapName}' saved successfully at {filePath}!");
    }

    public void Exit()
    {
        SaveMap();
        SceneManager.LoadScene("MainMenu");
    }
}
