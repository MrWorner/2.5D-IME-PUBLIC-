using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class MG_TileContainer
{
    public Vector2Int mapSize;
    public List<MGJSON_Tile> MG_Tiles = new List<MGJSON_Tile>();
}
