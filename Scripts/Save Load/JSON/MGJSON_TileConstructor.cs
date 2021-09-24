using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGJSON_TileConstructor : MonoBehaviour
{
    private static MGJSON_TileConstructor instance;//в редакторе только один объект должен быть создан

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MGJSON_TileConstructor Awake(): MGJSON_TileConstructor может быть только один компонент на Сцене, другие не нужны.</color>");
    }

    /// <summary>
    /// Получить контейнер наполненный MGJSON_Tile
    /// </summary>
    /// <param name="_map"></param>
    /// <returns></returns>
    public MGJSON_TileContainer ConstructContainer(MG_TileMap _map)
    {
        List<MGJSON_Tile> _jsonTileList = new List<MGJSON_Tile>();//листь объектов MGJSON_Tile
        List<MG_Tile> _list = _map.GetAllMgTiles();//получаем все MG_Tile


        foreach (MG_Tile _tile in _list)//берем каждый MG_Tile
        {
            MGJSON_Tile _jsonTile = GenerateJsonTile(_tile);//Обрабатываем его информацию в MGJSON_Tile
            _jsonTileList.Add(_jsonTile);//добавляем в локальный лист
        }

        MGJSON_TileContainer _tileContainer = new MGJSON_TileContainer();//подготавливаем контейнер   
        _tileContainer.list = _jsonTileList;
        return _tileContainer;
    }

    /// <summary>
    /// Сгенерировать MGJSON_Tile
    /// </summary>
    /// <param name="_mgTile"></param>
    /// <returns></returns>
    public MGJSON_Tile GenerateJsonTile(MG_Tile _mgTile)
    {
        string _wall_N = null;
        string _wall_E = null;
        string _wall_S = null;
        string _wall_W = null;
        WallType _wallType_N = WallType.None;
        WallType _wallType_E = WallType.None;
        WallType _wallType_S = WallType.None;
        WallType _wallType_W = WallType.None;

        bool _hasWall_N = _mgTile.HasWall(Direction.North);
        bool _hasWall_E = _mgTile.HasWall(Direction.East);
        bool _hasWall_S = _mgTile.HasWall(Direction.South);
        bool _hasWall_W = _mgTile.HasWall(Direction.West);

        if (_hasWall_N)
        {
            _wall_N = _mgTile.PropWall_N.ID;
            _wallType_N = _mgTile.PropWall_N.Type;
        }
        if (_hasWall_E)
        {
            _wall_E = _mgTile.PropWall_E.ID;
            _wallType_E = _mgTile.PropWall_E.Type;
        }
        if (_hasWall_S)
        {
            _wall_S = _mgTile.PropWall_S.ID;
            _wallType_S = _mgTile.PropWall_S.Type;
        }
        if (_hasWall_W)
        {
            _wall_W = _mgTile.PropWall_W.ID;
            _wallType_W = _mgTile.PropWall_W.Type;
        }

        string _entry_N = null;
        string _entry_E = null;
        string _entry_S = null;
        string _entry_W = null;
        EntryType _entryType_N = EntryType.None;
        EntryType _entryType_E = EntryType.None;
        EntryType _entryType_S = EntryType.None;
        EntryType _entryType_W = EntryType.None;
        if (_mgTile.HasEntry(Direction.North))
        {
            _entry_N = _mgTile.PropEntry_N.ID;
            _entryType_N = _mgTile.PropEntry_N.Type;
        }
        if (_mgTile.HasEntry(Direction.East))
        {
            _entry_E = _mgTile.PropEntry_E.ID;
            _entryType_E = _mgTile.PropEntry_E.Type;
        }
        if (_mgTile.HasEntry(Direction.South))
        {
            _entry_S = _mgTile.PropEntry_S.ID;
            _entryType_S = _mgTile.PropEntry_S.Type;
        }
        if (_mgTile.HasEntry(Direction.West))
        {
            _entry_W = _mgTile.PropEntry_W.ID;
            _entryType_W = _mgTile.PropEntry_W.Type;
        }

        string _prop = null;
        Direction _propDir = Direction.None;
        PropSize _propSize = PropSize.None;
        if (_mgTile.HasProp())
        {
            _prop = _mgTile.PropProperty.ID;
            _propDir = _mgTile.PropDir;
            _propSize = _mgTile.PropSize;
        }

        MGJSON_Tile _jsonTile = new MGJSON_Tile
        {
            pos = _mgTile.Pos,
            color = _mgTile.Color,
            floor = _mgTile.FloorProp.ID,
            floorType = _mgTile.FloorProp.Type,

            wallN = _wall_N,//id стены
            wallE = _wall_E,//id стены
            wallS = _wall_S,//id стены
            wallW = _wall_W,//id стены
            wallTypeN = _wallType_N,
            wallTypeE = _wallType_E,
            wallTypeS = _wallType_S,
            wallTypeW = _wallType_W,

            entryN = _entry_N,//id входа
            entryE = _entry_E,//id входа
            entryS = _entry_S,//id входа
            entryW = _entry_W,//id входа
            entryTypeN = _entryType_N,//тип входа (нужен для определения окно или дверь, если вдруг нет пользовательского контента)
            entryTypeE = _entryType_E,//тип входа (нужен для определения окно или дверь, если вдруг нет пользовательского контента)
            entryTypeS = _entryType_S,//тип входа (нужен для определения окно или дверь, если вдруг нет пользовательского контента)
            entryTypeW = _entryType_W,//тип входа (нужен для определения окно или дверь, если вдруг нет пользовательского контента)

            prop = _prop,
            propDir = _propDir,
            propSize = _propSize
        };

        return _jsonTile;
    }

    

}
