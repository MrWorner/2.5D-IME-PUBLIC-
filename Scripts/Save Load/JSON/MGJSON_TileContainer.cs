using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]//сохраняемость! https://metanit.com/sharp/tutorial/6.1.php
public class MGJSON_TileContainer
{
    //[NonSerialized]//не сохранять mapSize
    //public Vector2Int mapSize;
    public List<MGJSON_Tile> list = new List<MGJSON_Tile>();
 
    /// <summary>
    /// Получить размер
    /// </summary>
    /// <returns></returns>
    public int GetSize()
    {
        if (list != null)
        {
            return list.Count;
        }
        return 0;
    }
}
