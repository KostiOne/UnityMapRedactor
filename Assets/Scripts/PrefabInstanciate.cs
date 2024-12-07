using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrefabInstanciate : MonoBehaviour
{
    private GameObject prefabToInstantiate;
    [SerializeField] private Transform environmentParent; 
    [SerializeField] private LayerMask mousePlaneLayerMask; 
    [SerializeField] private LayerMask objectLayer;

    private ObjectRedactor objectRedactor;
    private GameObject selectedObject;

    private void Start()
    {
        objectRedactor = FindObjectOfType<ObjectRedactor>();
        objectRedactor.Hide();
    }

    void Update()
    {
    
        if (EventSystem.current.IsPointerOverGameObject()) return;

    
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
   
    }

    private void TryInstantiatePrefab()
    {
        Vector3 worldPosition = MouseWorld.GetPosition();

  
        if (IsValidTerrainLayer())
        {
            
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

    
        if (IsValidTerrainLayer())
        {
          
            selectedObject.transform.position = new Vector3(worldPosition.x, selectedObject.transform.position.y, worldPosition.z);
        }
    }

    private bool IsValidTerrainLayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
          
            return ((1 << hit.collider.gameObject.layer) & mousePlaneLayerMask) != 0;
        }
        return false;
    }

    public void SetPrefab(GameObject prefab)
    {
        prefabToInstantiate = prefab; 
    }
}
