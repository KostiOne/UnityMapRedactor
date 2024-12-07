using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionPanel : MonoBehaviour
{
    [SerializeField] private string prefabFolderPath = "ActionPrefabs";
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private GameObject buttonPrefab;

    private PrefabInstanciate prefabInstanciate; 

    private void Start()
    {
        // Находим PrefabInstanciate в сцене
        prefabInstanciate = FindObjectOfType<PrefabInstanciate>();
        if (prefabInstanciate == null)
        {
            Debug.LogError("PrefabInstanciate not found in the scene!");
            return;
        }

        LoadAndDisplayPrefabs();
    }

    private void LoadAndDisplayPrefabs()
    {
        // Загружаем все префабы из папки
        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>(prefabFolderPath);
        if (loadedPrefabs.Length == 0)
        {
            Debug.LogError("No prefabs found in folder: " + prefabFolderPath);
            return;
        }

        foreach (GameObject prefab in loadedPrefabs)
        {
            // Создаём кнопку для каждого префаба
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            // Настраиваем кнопку через её скрипт
            UIButtonPrefab buttonScript = newButton.GetComponent<UIButtonPrefab>();
            if (buttonScript != null)
            {
                buttonScript.Initialize(prefab, prefabInstanciate);
            }
            else
            {
                Debug.LogError("Button prefab is missing a UIButtonPrefab component!");
            }
        }
    }
}
