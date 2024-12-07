using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEditor : MonoBehaviour
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
            mapNameText.text = $"Editing: {levelData.levelName}";

        // Создаем объекты на основе данных
        LoadObjects();
    }

    private void LoadObjects()
    {
        foreach (ObjectData data in levelData.objectDataList)
        {
            // Загружаем префаб из папки Resources/ActionPrefabs
            GameObject prefab = Resources.Load<GameObject>($"ActionPrefabs/{data.prefabName}");

            if (data.prefabName == "Terrarian") 
            {
                Debug.Log($"Skipping prefab '{data.prefabName}'");
                continue; // Переходим к следующему объекту в списке
            }

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

    public void SaveMap()
    {
        // Собираем текущие данные объектов
        levelData.objectDataList.Clear();

        foreach (Transform child in parentTransform)
        {
            // Убираем суффикс "Clone" из имени объекта
            string prefabName = child.name.Replace("(Clone)", "").Trim();

            ObjectData data = new ObjectData
            {
                prefabName = prefabName,  // Сохраняем имя префаба без суффикса
                position = child.position,
                rotation = child.rotation.eulerAngles, // Сохраняем вращение
                scale = child.localScale // Сохраняем масштаб
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
