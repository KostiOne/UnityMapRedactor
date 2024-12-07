using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIButtonPrefab : MonoBehaviour
{
    [SerializeField] private Button button; // Ссылка на кнопку
    [SerializeField] private TextMeshProUGUI buttonText; // Текст кнопки

    private GameObject prefab; // Префаб, связанный с этой кнопкой
    private PrefabInstanciate prefabInstanciate; // Ссылка на PrefabInstanciate

    public void Initialize(GameObject prefab, PrefabInstanciate prefabInstanciate)
    {
        this.prefab = prefab;
        this.prefabInstanciate = prefabInstanciate;

        // Устанавливаем текст кнопки
        if (buttonText != null)
        {
            buttonText.text = prefab.name;
        }
        else
        {
            Debug.LogError("ButtonText (TextMeshProUGUI) is missing!");
        }

        // Подключаем событие нажатия кнопки
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component is missing!");
        }
    }

    public void OnButtonClick()
    {
        Debug.Log("Selected prefab: " + prefab.name);
        prefabInstanciate.SetPrefab(prefab); // Устанавливаем выбранный префаб
    }
}
