using Pathfinding;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public enum MapType { TopDown2D, Isometric2D, Full3D };//Тип карты (Full3D не реализовывается)

public class MG_TileMap : MonoBehaviour
{
    [SerializeField]
    private MapType Maptype = MapType.Isometric2D;//тип карты
    private Dictionary<Vector3Int, MG_Tile> dic_mgTiles = new Dictionary<Vector3Int, MG_Tile>();//Словарик. Необходим для хранения тайлов и его данные в виде MG_Tile.
    private Dictionary<int, MG_FloorData> dic_floorData = new Dictionary<int, MG_FloorData>();//Словарик. Необходим для хранения тайлов и его данные в виде MG_Tile.
    [SerializeField]
    private List<MG_FloorData> floorDataList;
    [SerializeField]
    private int sizeX;  //Используется для размера карты по X
    [SerializeField]
    private int sizeY;  //Используется для размера карты по Y

    [SerializeField]
    private MG_PathConstructor pathConstructor;//[R] создание нодов
    [SerializeField]
    private MG_TileConstuctor tileConstuctor;//[R] конструктор MG_Tile

    private void Awake()
    {
        //dictionary_MG_tiles = new Dictionary<Vector3Int, MG_Tile>();//Создаем справочник
        if (pathConstructor == null)
            Debug.Log("<color=red>MG_TileMap Awake(): MG_PathConstructor не прикреплен!</color>");
        if (tileConstuctor == null)
            Debug.Log("<color=red>MG_TileMap Awake(): MG_TileConstuctor не прикреплен!</color>");

        foreach (var _floorD in floorDataList)
        {
            AddFloor(_floorD.FloorNum, _floorD);
        }

    }

    /// <summary>
    /// Добавить этаж в словарь
    /// </summary>
    /// <param name="_floorNum"></param>
    /// <param name="floorData"></param>
    private void AddFloor(int _floorNum, MG_FloorData floorData)
    {
        dic_floorData.Add(_floorNum, floorData);
    }


    //public void GenerateMap()//ЗАПОЛНЯЕМ ТАЙЛАМИ КАРТУ
    //{
    //    //ClearMap();
    //    //MG_JSON.tileContainer = new MG_TileContainer();
    //    //MG_JSON.tileContainer.mapSize = new Vector2Int(sizeX, sizeY);


    //    //for (int y = 0; y < sizeY; y++)
    //    //{
    //    //    for (int x = 0; x < sizeX; x++)
    //    //    {
    //    //        Vector3Int _ChosenCellByMouse = new Vector3Int(x, -y, 0);
    //    //        floorManager.PlaceGrass(_ChosenCellByMouse, ChosenFloorPropForGrass, true);
    //    //    }
    //    //}

    //    ////-----------SetAllNeigbroursForAllCells(); 
    //    //Camera.main.GetComponent<MG_CameraControl>().ResetCamera();
    //}

    /// <summary>
    /// Получить тип карты
    /// </summary>
    /// <returns></returns>
    public MapType GetMapType()
    {
        return Maptype;
    }


    //public void ClearMap()
    //{
    //    //tileMap_floor.ClearAllTiles();//Удаляем все тайлы
    //    //tileMap_wallH.ClearAllTiles();//Удаляем все тайлы
    //    //tileMap_wallV.ClearAllTiles();//Удаляем все тайлы
    //    //tileMap_Props.ClearAllTiles();
    //    //List<GraphNode> nodeList = new List<GraphNode>();
    //    //AstarPath.active.data.pointGraph.GetNodes(nodeList.Add);
    //    //foreach (var _node in nodeList)
    //    //_node.Destroy();

    //    AstarPath.active.data.pointGraph.Scan();

    //    //-----------------------------------wallMap.ClearAllTiles();//Удаляем все тайлы
    //    if (dic_mgTiles != null)
    //        dic_mgTiles.Clear();//Очищаем словарик

    //    //if (addLabel)
    //    //{
    //    //    //BEGIN Удаляем координатные метки у удаленных клеток
    //    //    var childrenText = new List<GameObject>();
    //    //    foreach (Transform textLabel in labelCanvas.transform)
    //    //    {
    //    //        childrenText.Add(textLabel.gameObject);
    //    //    }
    //    //    childrenText.ForEach(c => DestroyImmediate(c));
    //    //    //END Удаляем координатные метки у удаленных клеток
    //    //}
    //}

    /// <summary>
    /// получить тайловую карту пола указанного этажа
    /// </summary>
    /// <param name="_floorN"></param>
    /// <returns></returns>
    /// 
    public MG_FloorData GetFloorData(int _floorN)
    {
        MG_FloorData _floorData;
        dic_floorData.TryGetValue(_floorN, out _floorData);
        return _floorData;
    }

    /// <summary>
    /// Получить тайловую карту пола
    /// </summary>
    /// <param name="_floorN"></param>
    /// <returns></returns>
    public Tilemap GetFloorMap(int _floorN)
    {
        MG_FloorData _mapFloor;
        _mapFloor = GetFloorData(_floorN);//берем из словаря
        return _mapFloor.FloorTileMap;
    }


    public Tilemap GetWallMap(LineDirection _lineDir, int _floorN)
    {
        MG_FloorData _mapFloor;
        _mapFloor = GetFloorData(_floorN);//берем из словаря
        switch (_lineDir)
        {
            case LineDirection.Horizontal:
                return _mapFloor.WallTileMap_H;
            case LineDirection.Vertical:
                return _mapFloor.WallTileMap_V;
        }
        return null;
    }

    /// <summary>
    /// Метод для получения нужной тайловой карты (особая тайловая карта для стен: H или V).
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_floorN"></param>
    /// <returns></returns>
    public Tilemap GetWallMapByDir(Direction _dir, int _floorN)
    {
        Tilemap _tileMap;
        if (_dir.Equals(Direction.North) || _dir.Equals(Direction.South))//Север или Юг - по горизонтали нужна карта
        {
            _tileMap = GetWallMap(LineDirection.Horizontal, _floorN);//берем Горизонтальную карту стен
            return _tileMap;
        }
        else if (_dir.Equals(Direction.East) || _dir.Equals(Direction.West))//Восток или Запад - по вертикали нужна карта
        {
            _tileMap = GetWallMap(LineDirection.Vertical, _floorN);//берем Вертикальную карту стен
            return _tileMap;
        }
        else
            Debug.Log("MG_WallConstructor GetCorrectTileMap(): Получены неверные значения. " + _dir);
        return null;
    }

    public Tilemap GetPropMap(int _floorN)
    {
        MG_FloorData _mapFloor;
        _mapFloor = GetFloorData(_floorN);//берем из словаря
        return _mapFloor.PropTileMap;
    }




    /// <summary>
    /// Получить MG_Tile
    /// </summary>
    /// <param name="_pos"></param>
    /// <returns></returns>
    public MG_Tile GetMgTile(Vector3Int _pos)
    {
        dic_mgTiles.TryGetValue(_pos, out MG_Tile _mgTile);
        return _mgTile;
    }

    /// <summary>
    /// Удалить MG_Tile
    /// </summary>
    /// <param name="_pos"></param>
    /// <returns></returns>
    public void RemoveMgTile(Vector3Int _pos)
    {
        dic_mgTiles.Remove(_pos);
    }

    /// <summary>
    /// Очистить MG_Tile словарь
    /// </summary>
    public void ClearMGtileDictionary()
    {
        dic_mgTiles.Clear();
    }



    /// <summary>
    /// Есть ли MG_Tile в справочнике
    /// </summary>
    /// <param name="_pos"></param>
    /// <returns></returns>
    public bool HasMgTile(Vector3Int _pos)
    {
        bool _has = dic_mgTiles.TryGetValue(_pos, out MG_Tile _mgTile);
        return _has;
    }

    /// <summary>
    /// Получить или создать MG_Tile
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_color"></param>
    /// <param name="_floorN"></param>
    /// <returns></returns>
    public MG_Tile TryToGetMGtile(Vector3Int _pos, Color _color, int _floorN)//получить MG_Tile c позиции, если не существует, то создать
    {
        MG_Tile _mgTile = GetMgTile(_pos);
        if (_mgTile == null)//Если не существует сам Тайл на карте, то создаем его 
        {
            _mgTile = tileConstuctor.CreateMgTile(_pos, _color, this, _floorN);//Заносим в словарик, при необходимости если не занесен.                
        }
        return _mgTile;
    }

    /// <summary>
    /// Добавить MG_Tile в словарик
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_mgTile"></param>
    public void AddMgTile(Vector3Int _pos, MG_Tile _mgTile)
    {
        dic_mgTiles.Add(_pos, _mgTile);
    }


    /// <summary>
    ///  Существует ли тайл
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_floorN"></param>
    /// <returns></returns>
    public bool HasTile(Vector3Int _pos, int _floorN)
    {

        TileBase _targetTile = GetTile(_pos, _floorN);
        if (_targetTile != null)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Получить тайл (Tile) 
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_floorN"></param>
    /// <returns></returns>
    public Tile GetTile(Vector3Int _pos, int _floorN)
    {
        Tilemap _tilemap = GetFloorMap(_floorN);
        Tile _tile = _tilemap.GetTile<Tile>(_pos);
        return _tile;
    }

    /// <summary>
    /// Получить координату позиции в редакторе
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_floorN"></param>
    /// <returns></returns>
    public Vector3 GetWorldPos(Vector3Int _pos, int _floorN)
    {
        Tilemap _tilemap = GetFloorMap(_floorN);
        Vector3 _worldPos = _tilemap.CellToWorld(_pos);
        return _worldPos;
    }

    /// <summary>
    /// Получить координаты клетки
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_floorN"></param>
    /// <returns></returns>
    public Vector3Int GetCellPos(Vector3 _pos, int _floorN)
    {
        Tilemap _tilemap = GetFloorMap(_floorN);
        return _tilemap.WorldToCell(_pos);
    }

    /// <summary>
    /// Метод для получение правильного значения добавки X и Y для setTile в особой тайловой карты для стен (H,V)
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_dir"></param>
    /// <returns></returns>
    public int[] GetCorrectXY(MapType _type, Direction _dir)
    {
        int _x = 0;
        int _y = 0;

        switch (_type)
        {
            case MapType.TopDown2D:
                {
                    if (_dir.Equals(Direction.South))
                        _y = -1;
                    else if (_dir.Equals(Direction.West))
                        _x = -1;
                    break;
                }
            case MapType.Isometric2D:
                {
                    if (_dir.Equals(Direction.North))
                        _y = 1;
                    else if (_dir.Equals(Direction.East))
                        _x = 1;
                    break;
                }
            default:
                {
                    Debug.Log("MG_TileMap GetCorrectXY(): Тип карты неопределен! " + _type);
                    break;
                }
        }
        return new int[] { _x, _y };
    }

    /// <summary>
    /// Получить лист всех MG_Tiles
    /// </summary>
    /// <returns></returns>
    public List<MG_Tile> GetAllMgTiles()
    {
        List<MG_Tile> _list = dic_mgTiles.Values.ToList();
        if (_list == null)
            _list = new List<MG_Tile>();
        return _list;
    }

    /// <summary>
    /// Очистить карту
    /// </summary>
    public void ClearMap()
    {
        foreach (var _flooData in floorDataList)//каждый слой карты
        {
            _flooData.RemoveAllTiles();//очищаем каждый слой карты
        }

        List<MG_Tile> _list = GetAllMgTiles();//Получаем весь список клеток из словаря
        foreach (MG_Tile _mgTile in _list)//проходимся по каждой клетки
        {
            PointNode _node = _mgTile.NodePro;//берем нод у клетки
            if (_node != null)
            {
                _node.Destroy();//уничтожаем каждый нод
            }
        }
        ClearMGtileDictionary();//очищаем словарик
    }
}

//https://arongranberg.com/astar/docs/old/using-nodes.php
//https://arongranberg.com/astar/docs/old/class_pathfinding_1_1_graph_node.php#abe58701c1937248599945d4d10bad2e0
//https://arongranberg.com/astar/docs_3.8/graph_types.php#point
//https://arongranberg.com/astar/docs/graph-updates.php#pointgraphs
//https://arongranberg.com/astar/docs/pointgraph.html#AddNode
//https://arongranberg.com/astar/docs/graphupdates.html#smaller-updates
//https://arongranberg.com/astar/docs/graphupdates.html#direct
//https://arongranberg.com/astar/docs/graphupdatescene.html
//https://arongranberg.com/astar/docs/pointgraph.html#AddNode
//https://arongranberg.com/astar/docs/astarpath.html#AddWorkItem
//https://arongranberg.com/astar/docs/astarpath.html#PausePathfinding


//public void GetAllTiles()
//{
//    foreach (Vector3Int pos in map.cellBounds.allPositionsWithin)
//    {
//        if (!map.HasTile(pos)) continue;
//        //WIP
//    }
//}

