using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_WallPropertyLibrary : MG_PropertyLibrary
{
    [SerializeField]
    private List<Tile> generatedTileIsoHList;//[D] Для просмотра
    [SerializeField]
    private List<Tile> generatedTileIsoVList;//[D] Для просмотра
    [SerializeField]
    private List<Tile> generatedTile2DHList;//[D] Для просмотра
    [SerializeField]
    private List<Tile> generatedTile2DVList;//[D] Для просмотра

    /// <summary>
    /// Кастомный метод который запускается после добавления в словарь. В основном используется для заполнения для просмотра тайлов[D]
    /// </summary>
    /// <param name="_tileProp"></param>
    public override void FillCustomList(MG_Property _tileProp)
    {
        MG_WallProperty _tilePropCasted = (MG_WallProperty)_tileProp;
        generatedTileIsoHList.Add(_tilePropCasted.Tile2DIso_H);//добавляем тайл Изометрии в список. ДЛЯ ПРОСМОТРА
        generatedTileIsoVList.Add(_tilePropCasted.Tile2DIso_V);//добавляем тайл Изометрии в список. ДЛЯ ПРОСМОТРА
        generatedTile2DHList.Add(_tilePropCasted.Tile2D_H);//добавляем Тайл 2D в список. ДЛЯ ПРОСМОТРА
        generatedTile2DVList.Add(_tilePropCasted.Tile2D_V);//добавляем Тайл 2D в список. ДЛЯ ПРОСМОТРА
    }
}
