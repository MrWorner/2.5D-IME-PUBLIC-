using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_WalkableArea : MonoBehaviour
{
    //RGB Colors as Float Values Converter
    //https://answers.unity.com/questions/1083182/rgb-colors-as-float-values-converter.html

    private List<MG_Tile> shownArea;
    //private List<MG_Tile> availableArea;
    private HashSet<MG_Tile> availableArea; //HashSet
    private Tile tile;
    private Tilemap visArea;
    private Color chosenColor = Color.green;
    private Color orange = Color.yellow;//new Color(1f, 0.5f, 0.25f, 1f)
    private Color green = Color.green;
    private Color red = Color.red;

    //public void ShowWalkableArea()
    //{
    //    MG_Tile _mgtile = GetComponent<MG_Unit>().MGtile;
    //    int _tu = GetComponent<MG_Unit>().TU;
    //    visArea = _mgtile.map.tileMap_VisibleArea;
    //    visArea.ClearAllTiles();
    //    tile = _mgtile.map.walkableTile;

    //    shownArea = new List<MG_Tile>();
    //    availableArea = new HashSet<MG_Tile>();

    //    AddAvArea(_mgtile);
    //    shownArea.Add(_mgtile);

    //    //while (availableArea.Count > 0 && _tu > 0)
    //    while (_tu > 0)
    //    {
    //        CheckColor(_tu);
    //        AddAvAreaFromN();
    //        ShowOnMap();
    //        _tu -= 1;
    //        availableArea.Clear();
    //    }
    //}

    //private void AddAvArea(MG_Tile _mgtile)
    //{
    //    foreach (MG_Tile _mgtileN in _mgtile.CellNeighbours)
    //    {
    //        availableArea.Add(_mgtileN);
    //    }
    //}

    //private void ShowOnMap()
    //{
    //    foreach (MG_Tile _mgtileN in availableArea)
    //    {
    //        if (!shownArea.Contains(_mgtileN))
    //        {
    //            Vector3Int _pos = _mgtileN.LocalPlace;
    //            visArea.SetTile(_pos, tile);
    //            visArea.SetTileFlags(_pos, TileFlags.None);
    //            visArea.SetColor(_pos, chosenColor);
    //            shownArea.Add(_mgtileN);
    //        }

    //    }
    //}

    //private void AddAvAreaFromN()
    //{
    //    foreach (MG_Tile _mgtileN in shownArea)
    //    {
    //        AddAvArea(_mgtileN);
    //    }
    //}

    //private void CheckColor(int _tu)
    //{
    //    if (_tu > 8)
    //    {
    //        if (!chosenColor.Equals(Color.green))
    //            chosenColor = Color.green;
    //        //Debug.Log("_tu = " + _tu + " Green");
    //    }
    //    else if (_tu > 4)
    //    {
    //        if (!chosenColor.Equals(orange))
    //            chosenColor = orange;
    //        //Debug.Log("_tu = " + _tu + " Orange");
    //    }
    //    else
    //    {
    //        if (!chosenColor.Equals(Color.red))
    //            chosenColor = Color.red;
    //        //Debug.Log("_tu = " + _tu + " Red");
    //    }
    //}


}
