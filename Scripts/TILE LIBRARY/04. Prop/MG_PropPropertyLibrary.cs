using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_PropPropertyLibrary : MG_PropertyLibrary
{
    [SerializeField]
    private List<Tile> generatedTileIsoList_N;//[D] Для просмотра ТАЙЛОВ
    [SerializeField]
    private List<Tile> generatedTileIsoList_E;//[D] Для просмотра ТАЙЛОВ
    [SerializeField]
    private List<Tile> generatedTileIsoList_S;//[D] Для просмотра ТАЙЛОВ
    [SerializeField]
    private List<Tile> generatedTileIsoList_W;//[D] Для просмотра ТАЙЛОВ
    [SerializeField]
    private List<Tile> generatedTile2DList_N;//[D] Для просмотра ТАЙЛОВ
    [SerializeField]
    private List<Tile> generatedTile2DList_E;//[D] Для просмотра ТАЙЛОВ
    [SerializeField]
    private List<Tile> generatedTile2DList_S;//[D] Для просмотра ТАЙЛОВ
    [SerializeField]
    private List<Tile> generatedTile2DList_W;//[D] Для просмотра ТАЙЛОВ

    /// <summary>
    /// Кастомный метод который запускается после добавления в словарь. В основном используется для заполнения для просмотра тайлов[D]
    /// </summary>
    /// <param name="_tileProp"></param>
    public override void FillCustomList(MG_Property _tileProp)
    {
        MG_PropProperty _tilePropCasted = (MG_PropProperty) _tileProp;
        generatedTileIsoList_N.Add(_tilePropCasted.Tile2DIso_N);
        generatedTileIsoList_E.Add(_tilePropCasted.Tile2DIso_E);
        generatedTileIsoList_S.Add(_tilePropCasted.Tile2DIso_S);
        generatedTileIsoList_W.Add(_tilePropCasted.Tile2DIso_W);
        generatedTile2DList_N.Add(_tilePropCasted.Tile2D_N);
        generatedTile2DList_E.Add(_tilePropCasted.Tile2D_E);
        generatedTile2DList_S.Add(_tilePropCasted.Tile2D_S);
        generatedTile2DList_W.Add(_tilePropCasted.Tile2D_W);
    }
}
