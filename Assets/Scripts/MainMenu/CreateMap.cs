using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateMap : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField; // Поле для ввода названия карты
    [SerializeField] private string editorSceneName = "Editor"; // Название сцены-редактора

    private void Start()
    {
        // Убедимся, что у нас есть название сцены-редактора
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

        // Создаём путь для сохранения файла карты
        string filePath = Path.Combine(Application.persistentDataPath, mapName + ".json");

        if (File.Exists(filePath))
        {
            Debug.LogError($"Map '{mapName}' already exists!");
            return;
        }

        // Создаём базовые данные карты
        LevelData newLevel = new LevelData
        {
            levelName = mapName,
            objectDataList = new System.Collections.Generic.List<ObjectData>()
        };

        // Сериализуем данные карты в JSON
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

        // Сохраняем имя карты для сцены-редактора
        PlayerPrefs.SetString("CurrentMap", mapName);
        PlayerPrefs.Save();

        // Загружаем сцену-редактор
        SceneManager.LoadScene(editorSceneName);
    }
}
