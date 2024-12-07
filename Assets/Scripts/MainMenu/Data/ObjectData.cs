using System.Collections.Generic;
using UnityEngine;

// Класс для данных объекта уровня
[System.Serializable]
public class ObjectData
{
    public string prefabName;      // Имя префаба
    public Vector3 position;      // Позиция объекта
    public Vector3 rotation;      // Вращение объекта
    public Vector3 scale;         // Масштаб объекта
}