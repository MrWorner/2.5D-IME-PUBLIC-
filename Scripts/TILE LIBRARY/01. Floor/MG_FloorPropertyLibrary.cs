using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_FloorPropertyLibrary : MG_PropertyLibrary
{

    [SerializeField]
    private List<Tile> generatedTileIsoList;//[D] Для просмотра ТАЙЛОВ
    [SerializeField]
    private List<Tile> generatedTile2DList;//[D] Для просмотра ТАЙЛОВ

    /// <summary>
    /// Кастомный метод который запускается после добавления в словарь. В основном используется для заполнения для просмотра тайлов[D]
    /// </summary>
    /// <param name="_tileProp"></param>
    public override void FillCustomList(MG_Property _tileProp)
    {
        MG_FloorProperty _tilePropCasted = (MG_FloorProperty) _tileProp;
        generatedTileIsoList.Add(_tilePropCasted.Tile2DIso);
        generatedTile2DList.Add(_tilePropCasted.Tile2D);
    }
}
