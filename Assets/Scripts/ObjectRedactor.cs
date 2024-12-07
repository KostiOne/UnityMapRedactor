using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectRedactor : MonoBehaviour
{
    [SerializeField] private Slider rotationSlider;
    [SerializeField] private Slider scaleSlider;  
    [SerializeField] private Button deletePrefabButton; 

    private GameObject _object;

    [SerializeField] private float rotationMultiplier = 5f;
    [SerializeField] private float scaleMultiplier = 1f;  

    private void Start()
    {

        deletePrefabButton.onClick.AddListener(DeletePrefab);
        rotationSlider.onValueChanged.AddListener(RotateObject);
        scaleSlider.onValueChanged.AddListener(ScaleObject);
    }

    public void EneableRedactor(GameObject objectToRedact)
    {
        _object = objectToRedact;


        rotationSlider.value = _object.transform.eulerAngles.y;
        scaleSlider.value = _object.transform.localScale.x;

        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void DeletePrefab()
    {
        if (_object != null)
        {
            Destroy(_object);
            _object = null;
            Hide();
        }
    }

    private void RotateObject(float value)
    {
        if (_object != null)
        {

            Vector3 currentRotation = _object.transform.eulerAngles;
            currentRotation.y = value * rotationMultiplier;
            _object.transform.eulerAngles = currentRotation;
        }
    }


    private void ScaleObject(float value)
    {
        if (_object != null)
        {

            float scale = value * scaleMultiplier;
            _object.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
