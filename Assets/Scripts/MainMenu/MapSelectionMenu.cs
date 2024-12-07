using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapSelectionMenu : MonoBehaviour
{
    [SerializeField] private Transform contentPanel;       // Панель для размещения кнопок
    [SerializeField] private GameObject buttonPrefab;      // Префаб кнопки для карты
    [SerializeField] private string editorSceneName = "Editor"; // Название сцены-редактора

    private string mapsFolderPath;

    private void Start()
    {
        // Путь к папке с сохранёнными картами
        mapsFolderPath = Application.persistentDataPath;
        LoadMapList();
    }

    private void LoadMapList()
    {
        // Очищаем старые кнопки (если они есть)
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Получаем все файлы с расширением .json в папке
        string[] mapFiles = Directory.GetFiles(mapsFolderPath, "*.json");

        foreach (string filePath in mapFiles)
        {
            string mapName = Path.GetFileNameWithoutExtension(filePath);
            CreateMapButton(mapName);
        }
    }

    private void CreateMapButton(string mapName)
    {
        // Создаём кнопку из префаба
        GameObject buttonObj = Instantiate(buttonPrefab, contentPanel);
        buttonObj.GetComponentInChildren<TMP_Text>().text = mapName;

        // Добавляем обработчик клика для кнопки
        buttonObj.GetComponent<Button>().onClick.AddListener(() => OnMapSelected(mapName));
    }

    private void OnMapSelected(string mapName)
    {
        // Сохраняем выбранное имя карты для редактора
        PlayerPrefs.SetString("CurrentMap", mapName);
        PlayerPrefs.Save();

        // Загружаем сцену-редактор
        SceneManager.LoadScene(editorSceneName);
    }
}
