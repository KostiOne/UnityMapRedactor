using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private Transform parentTransform; // Родительский объект для созданных объектов
    [SerializeField] private TMP_Text mapNameText;      // Текстовое поле для отображения имени карты

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

        // Загружаем данные карты
        string json = File.ReadAllText(filePath);
        levelData = JsonUtility.FromJson<LevelData>(json);

        // Отображаем имя карты на UI
        if (mapNameText != null)
            mapNameText.text = $"Plaing: {levelData.levelName}";

        // Создаем объекты на основе данных
        LoadObjects();
    }

    private void LoadObjects()
    {
        foreach (ObjectData data in levelData.objectDataList)
        {
            // Загружаем префаб из папки Resources/ActionPrefabs
            GameObject prefab = Resources.Load<GameObject>($"ActionPrefabs/{data.prefabName}");

            if (prefab != null)
            {
                // Если объект найден, создаём его
                GameObject obj = Instantiate(prefab, data.position, Quaternion.Euler(data.rotation), parentTransform);
                obj.transform.localScale = data.scale; // Восстанавливаем масштаб
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
