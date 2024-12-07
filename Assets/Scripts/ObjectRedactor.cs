using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectRedactor : MonoBehaviour
{
    [SerializeField] private Slider rotationSlider; // Слайдер для вращения
    [SerializeField] private Slider scaleSlider;    // Слайдер для масштаба
    [SerializeField] private Button deletePrefabButton; // Кнопка для удаления объекта

    private GameObject _object;

    [SerializeField] private float rotationMultiplier = 5f; // Ускорение вращения
    [SerializeField] private float scaleMultiplier = 1f;    // Масштабирование

    private void Start()
    {
        // Привязываем события
        deletePrefabButton.onClick.AddListener(DeletePrefab);
        rotationSlider.onValueChanged.AddListener(RotateObject);
        scaleSlider.onValueChanged.AddListener(ScaleObject);
    }

    public void EneableRedactor(GameObject objectToRedact)
    {
        _object = objectToRedact;

        // Инициализация значений слайдеров
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
            // Сохраняем текущий угол по X и Z, изменяем только угол по Y
            Vector3 currentRotation = _object.transform.eulerAngles;
            currentRotation.y = value * rotationMultiplier;
            _object.transform.eulerAngles = currentRotation;
        }
    }


    private void ScaleObject(float value)
    {
        if (_object != null)
        {
            // Ускоряем масштабирование с использованием множителя
            float scale = value * scaleMultiplier;
            _object.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
