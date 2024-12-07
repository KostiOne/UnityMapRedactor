using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIButtonPrefab : MonoBehaviour
{
    [SerializeField] private Button button; 
    [SerializeField] private TextMeshProUGUI buttonText;

    private GameObject prefab;
    private PrefabInstanciate prefabInstanciate;

    public void Initialize(GameObject prefab, PrefabInstanciate prefabInstanciate)
    {
        this.prefab = prefab;
        this.prefabInstanciate = prefabInstanciate;

    
        if (buttonText != null)
        {
            buttonText.text = prefab.name;
        }
        else
        {
            Debug.LogError("ButtonText (TextMeshProUGUI) is missing!");
        }

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
        prefabInstanciate.SetPrefab(prefab);
    }
}
