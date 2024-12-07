using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrefabInstanciate : MonoBehaviour
{
    private GameObject prefabToInstantiate; // Текущий выбранный префаб
    [SerializeField] private Transform environmentParent; // Родительский объект (Environment)
    [SerializeField] private LayerMask mousePlaneLayerMask; // Слой, на котором может быть объект, с которым мы хотим взаимодействовать
    [SerializeField] private LayerMask objectLayer; // Слой объектов для выбора

    private ObjectRedactor objectRedactor;
    private GameObject selectedObject; // Текущий выбранный объект

    private void Start()
    {
        objectRedactor = FindObjectOfType<ObjectRedactor>();
        objectRedactor.Hide();
    }

    void Update()
    {
        // Игнорируем клики по UI
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Проверяем нажатие левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            if (prefabToInstantiate != null)
            {
                TryInstantiatePrefab();
            }
           TrySelectObject();
        }

        if (Input.GetMouseButton(0) && selectedObject != null)
        {
            MoveSelectedObject();
        }
        // Если зажата левая кнопка мыши и выбран объект, перемещаем его
    }

    private void TryInstantiatePrefab()
    {
        Vector3 worldPosition = MouseWorld.GetPosition();

        // Проверяем, есть ли объект на месте клика и имеет ли он правильный слой
        if (IsValidTerrainLayer())
        {
            // Если луч попал в нужный слой, создаём новый префаб в позиции клика и делаем его дочерним для Environment
            Instantiate(prefabToInstantiate, worldPosition, Quaternion.identity, environmentParent);
        }
    }

    private void TrySelectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, objectLayer))
        {
            selectedObject = hit.collider.gameObject;
            objectRedactor.EneableRedactor(selectedObject);
        }
    }

    private void MoveSelectedObject()
    {
        Vector3 worldPosition = MouseWorld.GetPosition();

        // Проверяем, что позиция находится на слое Terrain
        if (IsValidTerrainLayer())
        {
            // Обновляем только позицию объекта, оставляя его текущее вращение нетронутым
            selectedObject.transform.position = new Vector3(worldPosition.x, selectedObject.transform.position.y, worldPosition.z);
        }
    }

    private bool IsValidTerrainLayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
            // Проверяем, что объект находится на правильном слое
            return ((1 << hit.collider.gameObject.layer) & mousePlaneLayerMask) != 0;
        }
        return false;
    }

    public void SetPrefab(GameObject prefab)
    {
        prefabToInstantiate = prefab; // Устанавливаем выбранный префаб
    }
}
