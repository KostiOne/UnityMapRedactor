using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapSelectionMenu : MonoBehaviour
{
    [SerializeField] private Transform contentPanel;      
    [SerializeField] private GameObject buttonPrefab;    
    [SerializeField] private string editorSceneName = "Editor";

    private string mapsFolderPath;

    private void Start()
    {

        mapsFolderPath = Application.persistentDataPath;
        LoadMapList();
    }

    private void LoadMapList()
    {

        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        string[] mapFiles = Directory.GetFiles(mapsFolderPath, "*.json");

        foreach (string filePath in mapFiles)
        {
            string mapName = Path.GetFileNameWithoutExtension(filePath);
            CreateMapButton(mapName);
        }
    }

    private void CreateMapButton(string mapName)
    {
   
        GameObject buttonObj = Instantiate(buttonPrefab, contentPanel);
        buttonObj.GetComponentInChildren<TMP_Text>().text = mapName;

    
        buttonObj.GetComponent<Button>().onClick.AddListener(() => OnMapSelected(mapName));
    }

    private void OnMapSelected(string mapName)
    {
      
        PlayerPrefs.SetString("CurrentMap", mapName);
        PlayerPrefs.Save();

       
        SceneManager.LoadScene(editorSceneName);
    }
}
