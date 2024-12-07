using System.Collections.Generic;
using UnityEngine;

// Класс для данных уровня
[System.Serializable]
public class LevelData
{
    public string levelName;                  // Имя уровня
    public List<ObjectData> objectDataList = new List<ObjectData>(); // Список данных об объектах
}
