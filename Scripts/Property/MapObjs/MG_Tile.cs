
using Pathfinding;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

//USED SAMPLE: https://medium.com/@allencoded/unity-tilemaps-and-storing-individual-tile-data-8b95d87e9f32
public class MG_Tile
{
    //public string ID { get; set; }
    public string Name { get; set; }
    public Vector3Int Pos { get; set; }
    public Vector3 WorldLocation { get; set; }
    public MG_TileMap Map { get; set; }
    public List<MG_Tile> CellNeighbours { get; set; } = new List<MG_Tile>(); //Массив компонентов соседних клеток 
    public TextMeshPro Label { get; set; }
    //public int Cost { get; set; } //Цена за движение по данной клетке
    //public bool Walkable { get; set; } //Можно ли ходить по данной клетке
    public PointNode NodePro { get; set; }//https://arongranberg.com/astar/docs/old/class_pathfinding_1_1_graph_node.php#abe58701c1937248599945d4d10bad2e0

    public MG_WallProperty PropWall_N { get; set; } = null;
    public MG_WallProperty PropWall_E { get; set; } = null;
    public MG_WallProperty PropWall_S { get; set; } = null;
    public MG_WallProperty PropWall_W { get; set; } = null;

    public MG_EntryProperty PropEntry_N { get; set; } = null;
    public MG_EntryProperty PropEntry_E { get; set; } = null;
    public MG_EntryProperty PropEntry_S { get; set; } = null;
    public MG_EntryProperty PropEntry_W { get; set; } = null;
    public MG_PropProperty PropProperty { get; set; } = null;

    public Direction PropDir { get; set; } = Direction.None;
    public PropSize PropSize { get; set; } = PropSize.None;

    public Color Color { get; set; } = Color.white;
    public MG_FloorProperty FloorProp { get; set; }

    /// <summary>
    /// Получить тип входа
    /// </summary>
    /// <param name="_dir"></param>
    /// <returns></returns>
    public EntryType GetEntryType(Direction _dir)
    {
        EntryType _type = EntryType.None;
        switch (_dir)
        {
            case Direction.North:
                if (PropEntry_N != null)
                {
                    _type = this.PropEntry_N.Type;
                }
                break;
            case Direction.East:
                if (PropEntry_E != null)
                {
                    _type = this.PropEntry_E.Type;
                }
                break;
            case Direction.South:
                if (PropEntry_S != null)
                {
                    _type = this.PropEntry_S.Type;
                }
                break;
            case Direction.West:
                if (PropEntry_W != null)
                {
                    _type = this.PropEntry_W.Type;
                }
                break;
                //default:
                //Debug.Log("MG_Tile GetEntryType(): Получены неверные значения. " + _dir);
                //break;
        }
        return _type;
    }

    //Есть ли какой нибудь вход в данном направлении
    public bool HasEntry(Direction _dir)
    {
        bool _hasEntry = false;
        switch (_dir)
        {
            case Direction.North:
                if (PropEntry_N != null)
                {
                    _hasEntry = true;
                }
                break;
            case Direction.East:
                if (PropEntry_E != null)
                {
                    _hasEntry = true;
                }
                break;
            case Direction.South:
                if (PropEntry_S != null)
                {
                    _hasEntry = true;
                }
                break;
            case Direction.West:
                if (PropEntry_W != null)
                {
                    _hasEntry = true;
                }
                break;
                default:
                Debug.Log("MG_Tile GetEntryType(): Получены неверные значения. " + _dir);
                break;
        }
        return _hasEntry;
    }


    /// <summary>
    /// Есть ли стена в данном направлении
    /// </summary>
    /// <param name="_dir"></param>
    /// <returns></returns>
    public bool HasWall(Direction _dir)
    {
        bool _hasWall = false;
        switch (_dir)
        {
            case Direction.None:
                break;
            case Direction.North:
                if (PropWall_N != null)
                    _hasWall = true;
                break;
            case Direction.East:
                if (PropWall_E != null)
                    _hasWall = true;
                break;
            case Direction.South:
                if (PropWall_S != null)
                    _hasWall = true;
                break;
            case Direction.West:
                if (PropWall_W != null)
                    _hasWall = true;
                break;
            default:
                Debug.Log("<color=red> MG_Tile HasWall(): нереализованное направление! </color>" + _dir);
                break;
        }

        return _hasWall;
    }

    /// <summary>
    /// Есть ли объект
    /// </summary>
    /// <returns></returns>
    public bool HasProp()
    {
        if (PropProperty == null)
            return false;
        else
            return true;
    }
}

[Serializable]
public class MGJSON_Tile
{
    public Vector3Int pos;
    public Color color;
    public string floor;
    public FloorType floorType;

    public string wallN;//id стены
    public string wallE;//id стены
    public string wallS;//id стены
    public string wallW;//id стены
    public WallType wallTypeN;//тип стены
    public WallType wallTypeE;//тип стены
    public WallType wallTypeS;//тип стены
    public WallType wallTypeW;//тип стены

    public string entryN;//id входа
    public string entryE;//id входа
    public string entryS;//id входа
    public string entryW;//id входа
    public EntryType entryTypeN;//тип входа
    public EntryType entryTypeE;//тип входа
    public EntryType entryTypeS;//тип входа
    public EntryType entryTypeW;//тип входа

    public Direction propDir;
    public string prop; //MG_FloorProperty.ID
    public PropSize propSize; //размер
}