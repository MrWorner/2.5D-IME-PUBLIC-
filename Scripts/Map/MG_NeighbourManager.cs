using Pathfinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MG_NeighbourManager : MonoBehaviour
{
    private static MG_NeighbourManager instance;//в редакторе только один объект должен быть создан
    //[SerializeField]
    //private MG_Editor editor;
    [SerializeField]
    private MG_WallConstructor wallConstructor;//конструктор стен
    [SerializeField]
    private MG_PathConstructor pathConstructor;//конструктор нодов

    private readonly int[][] coord = new int[8][]//массив координат, для нахождения соседей через цикл For
 {
        new int[] { 0, 1},
        new int[] { 1, 1},
        new int[] { 1, 0},
        new int[] { 1, -1},
        new int[] { 0, -1},
        new int[] { -1, -1},
        new int[] { -1, 0},
        new int[] { -1, 1}
 };

    private readonly Direction[] dirs = new Direction[8]//массив направлений, для нахождения соседей через цикл For
    {
            Direction.North,
            Direction.NE,
            Direction.East,
            Direction.SE,
            Direction.South,
            Direction.SW,
            Direction.West,
            Direction.NW
    };

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("MG_NeighbourManager Awake(): MG_NeighbourManager может быть только один компонент на Сцене, другие не нужны.");
        if (wallConstructor == null)
            Debug.Log("<color=red>MG_NeighbourManager Awake(): MG_WallConstructor не прикреплен!</color>");
        if (pathConstructor == null)
            Debug.Log("<color=red>MG_NeighbourManager Awake(): MG_PathConstructor не прикреплен!</color>");
        //if (editor == null)
        //    Debug.Log("<color=red>MG_NeighbourManager Awake(): MG_Editor не прикреплен!</color>");
    }

    /// <summary>
    /// Получить инстанс
    /// </summary>
    /// <returns></returns>
    public static MG_NeighbourManager GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// метод для проверки всех соседей и чтобы все соседи сделали перепроверку
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_starter"></param>
    /// <param name="_map"></param>
    public void CheckSetAllNeigbrours(MG_Tile _mgtile, bool _starter, MG_TileMap _map)//Для определения соседей (клетки)
    {
        //Debug.Log("_mgtile = " + _mgtile.Name + " _starter = " + _starter);     
        int _x = _mgtile.Pos.x;
        int _y = _mgtile.Pos.y;
        for (int i = 0; i < 8; i++)
        {
            //_mgtileN = CheckSetNeigbByPos(_mgtile, coord[i][0], coord[i][1], dirs[i]);
            MG_Tile _mgtileN = _map.GetMgTile(new Vector3Int(_x + coord[i][0], _y + coord[i][1], 0));
            if (_mgtileN != null)
            {
                if (_starter) CopyAllBorderProperties(_mgtile, _mgtileN, dirs[i]);//!EXPERIMENTAL!
                Constructor(_mgtile, _mgtileN, dirs[i], _map);
                if (_starter) CheckSetAllNeigbrours(_mgtileN, false, _map);//Рекурсия, запускаем у соседа этот же метод для полной перепроверки соседей. ИЗ-ЗА ПРОБЛЕМ ПО ДИАГОНАЛИ!
            }
        }
    }

    //private MG_Tile CheckSetNeigbByPos(MG_Tile _mgtile, int _addX, int _addY, Direction _dir)
    //{
    //    int _x = _mgtile.LocalPlace.x;
    //    int _y = _mgtile.LocalPlace.y;

    //    //MG_TileMap _map = MG_TileMap.GetCurrentMap();
    //    MG_Tile _mgtileN = MG_TileMap.GetMgTileFromDic(new Vector3Int(_x + _addX, _y + _addY, 0));
    //    if (_mgtileN != null)
    //    {
    //        CheckSetConnectionByDirection(_mgtile, _mgtileN, _dir);
    //        //----------------CheckSetConnectionByDirection(_cellN, _cell, ReverseDirection(_dir));
    //        return _mgtileN;//возвращаем соседа
    //    }
    //    return null;
    //}

    /// <summary>
    /// Добавляем соседа в список соседей. Также устанавливаем связь между нодами. ОДНОСТОРОННЯЯ СВЯЗЬ!
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_mgtileN"></param>
    private void AddNeigbour(MG_Tile _mgtile, MG_Tile _mgtileN)
    {
        List<MG_Tile> _list = _mgtile.CellNeighbours;//ссылка на лист
        if (!_list.Contains(_mgtileN))//проверяем что не содержится сосед в списке
        {
            _list.Add(_mgtileN);//добавляем в список соседей
            PointNode _node = _mgtile.NodePro;//вытаскиваем нод
            PointNode _nodeN = _mgtileN.NodePro;//вытаскиваем нод у соседа
            pathConstructor.LinkNode(_node, _nodeN);//соединяем ноды ОДНОСТОРОННЯЯ СВЯЗЬ
        }
    }

    /// <summary>
    /// Удаляем соседа из списка соседей. Также удаляем связь между нодами. ОДНОСТОРОННЯЯ СВЯЗЬ!
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_mgtileN"></param>
    private void RemoveNeigbour(MG_Tile _mgtile, MG_Tile _mgtileN)
    {
        List<MG_Tile> _list = _mgtile.CellNeighbours;//ссылка на лист
        if (_list.Contains(_mgtileN))//проверяем что содержится сосед в списке
        {
            _list.Remove(_mgtileN);//удаляем из списка соседей
            PointNode _node = _mgtile.NodePro;//вытаскиваем нод
            PointNode _nodeN = _mgtileN.NodePro;//вытаскиваем нод у соседа
            pathConstructor.UnLinkNode(_node, _nodeN);//разрываем связь нодов ОДНОСТОРОННЯЯ СВЯЗЬ
        }
    }

    /// <summary>
    /// Метод для проверки препятствий диагоналей по направлению
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <returns></returns>
    private bool IsDiagonalBlocked(MG_Tile _mgtile, Direction _dir, MG_TileMap _map)//НУЖЕН ТОЛЬКО ДЛЯ НАХОЖДЕНИЯ ПУТИ
    {
        bool _isBlocked = false;

        //------ПОЛУЧАЕМ НАПРАВЛЕНИЕ ПО КОТОРЫМ НУЖНО ПРОВЕРИТЬ СОСЕДЕЙ!
        Direction[] _dirArray;
        switch (_dir)
        {

            case Direction.NE:
                _dirArray = new Direction[2] { Direction.North, Direction.East };//Направления соседей для текущей клетки, которые могут блокировать диагональ (УГОЛ)
                break;
            case Direction.SE:
                _dirArray = new Direction[2] { Direction.South, Direction.East };//Направления соседей для текущей клетки, которые могут блокировать диагональ (УГОЛ)
                break;
            case Direction.NW:
                _dirArray = new Direction[2] { Direction.North, Direction.West };//Направления соседей для текущей клетки, которые могут блокировать диагональ (УГОЛ)
                break;
            case Direction.SW:
                _dirArray = new Direction[2] { Direction.South, Direction.West };//Направления соседей для текущей клетки, которые могут блокировать диагональ (УГОЛ)
                break;
            default:
                Debug.Log("<color=red>MG_NeighbourManager IsDiagonalBlocked(): недоступный тип направления!</color> " + _dir);
                return false;
        }

        Vector3Int _pos = _mgtile.Pos;
        foreach (var _dir2 in _dirArray)
        {
            MG_Tile _mgtileN = GetNeigbourByDir(_pos, _dir2, _map);//получаем соседа
            if (_mgtileN != null)//если сосед присутствует
            {
                bool _connection = CheckConnectionByDirection(_mgtile, _mgtileN, _dir2, _map, true);//проверяем что ничего нет
                if (!_connection)
                {
                    _isBlocked = true;
                    break;
                }
            }
            else//Если его нет, то там пусто по диагонали, ходить явно нельзя!
            {
                _isBlocked = true;
                break;
            }
        }

        return _isBlocked;
    }

    /// <summary>
    /// Проверить есть ли вход через который можно проходить
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_dir"></param>
    /// <returns></returns>
    public bool HasWalkableEntry(MG_Tile _mgtile, Direction _dir)
    {
        EntryType _type = _mgtile.GetEntryType(_dir);

        //Debug.Log(" _type = " + _type);
        if (_type.Equals(EntryType.Door))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Метод для проверки и создания связи между соседними клетками. Связь создается односторонняя! В данном методе находяться все необходимые проверки и условия
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_mgtileN"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    private void Constructor(MG_Tile _mgtile, MG_Tile _mgtileN, Direction _dir, MG_TileMap _map)
    {
        bool _createLink = CheckConnectionByDirection(_mgtile, _mgtileN, _dir, _map, false);//нужно ли создавать связь или ее нужно разорвать

        if (_createLink)
            AddNeigbour(_mgtile, _mgtileN);//добавляем соседа ОДНОСТОРОННЯЯ СВЯЗЬ
        else
            RemoveNeigbour(_mgtile, _mgtileN);//удаляем соседа ОДНОСТОРОННЯЯ СВЯЗЬ
    }

    /// <summary>
    /// Проверяем все преграды между клетками
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_mgtileN"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <param name="_forDiagonal"></param>
    /// <returns></returns>
    private bool CheckConnectionByDirection(MG_Tile _mgtile, MG_Tile _mgtileN, Direction _dir, MG_TileMap _map, bool _forDiagonal)
    {
        bool _connection = false;//нужно ли создавать связь или ее нужно разорвать
        //CheckSetDiagonalWalkable(_mgtile, _map);//Нужно обязательно проверить по диагонали

        bool _hasProp = _mgtile.HasProp();//есть ли какой нибудь объект на данный клетке
        bool _hasPropN = _mgtileN.HasProp();//есть ли какой нибудь объект на соседней клетке

        if (!_hasProp && !_hasPropN)//у обоих нет Prop объекта
        {
            if (MG_Direction.Diagonal.Contains(_dir))//если сосед - на диагонали, то нужна специальная проверка
            {
                bool _blocked = IsDiagonalBlocked(_mgtile, _dir, _map);//проверяем есть ли помехи у диагонали
                if (!_blocked)//если по диагонали ничего не мешает
                {
                    bool _blockedN = IsDiagonalBlocked(_mgtileN, MG_Direction.ReverseDir(_dir), _map);//проверяем есть ли помехи у диагонали СОСЕДА
                    if (!_blockedN)//если у соседа по диагонали ничего не мешает
                    {
                        _connection = true;
                    }
                }
            }
            else//если сосед - на НЕ диагонали, то проверим есть ли стена, а затем вход если есть стена
            {
                bool _hasWall = _mgtile.HasWall(_dir);
                if (_hasWall)//если есть стена
                {
                    if (!_forDiagonal)//Не для диагонали будем проверять вход. Если эту проверку убрать, то стена с дверями будет проходима по диагонали (БАГ)
                    {
                        bool _walkableEntry = HasWalkableEntry(_mgtile, _dir);//проверяем есть ли вход                                                                              //Debug.Log(" _walkableEntry = " + _walkableEntry);
                        if (_walkableEntry)//если есть вход через который можно прйти
                        {
                            _connection = true;
                        }
                    }
                }
                else
                {
                    _connection = true;
                }
            }
            //Debug.Log(" _blocked = " + _blocked );
        }
        //Debug.Log("_linkIt = " + _linkIt);

        return _connection;
    }

    /// <summary>
    /// Удаляем всех соседей
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_map"></param>
    public void RemoveAllNeigbrours(MG_Tile _mgtile, MG_TileMap _map)//Для удаления всех соседей (клетки)
    {
        int _x = _mgtile.Pos.x;
        int _y = _mgtile.Pos.y;
        for (int i = 0; i < 8; i++)
        {
            MG_Tile _mgtileN = _map.GetMgTile(new Vector3Int(_x + coord[i][0], _y + coord[i][1], 0));
            if (_mgtileN != null)
            {
                RemoveNeigbour(_mgtile, _mgtileN);//убираем соседа
                RemoveNeigbour(_mgtileN, _mgtile);//теперь сосед также убирает выбранную клетку
            }
        }
    }

    /// <summary>
    /// Получить соседа
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <returns></returns>
    public MG_Tile GetNeigbourByDir(Vector3Int _pos, Direction _dir, MG_TileMap _map)
    {
        int _x = _pos.x;
        int _y = _pos.y;
        switch (_dir)
        {
            case Direction.North:
                {
                    return _map.GetMgTile(new Vector3Int(_x, _y + 1, 0));
                }
            case Direction.NE:
                {
                    return _map.GetMgTile(new Vector3Int(_x + 1, _y + 1, 0));
                }
            case Direction.NW:
                {
                    return _map.GetMgTile(new Vector3Int(_x - 1, _y + 1, 0));
                }
            case Direction.South:
                {
                    return _map.GetMgTile(new Vector3Int(_x, _y - 1, 0));
                }
            case Direction.SE:
                {
                    return _map.GetMgTile(new Vector3Int(_x + 1, _y - 1, 0));
                }
            case Direction.SW:
                {
                    return _map.GetMgTile(new Vector3Int(_x - 1, _y - 1, 0));
                }
            case Direction.East:
                {
                    return _map.GetMgTile(new Vector3Int(_x + 1, _y, 0));
                }
            case Direction.West:
                {
                    return _map.GetMgTile(new Vector3Int(_x - 1, _y, 0));
                }
            default: return null;
        }
    }

    /// <summary>
    /// После созданного нового пола, чтобы взять переменные соседских стен, дверей и тд., которые окружают (на границе). Новый пол не знает о них и должен о них знать. Иначе по этому полу можно будет пройти сквозь препятствие к соседям!
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_mgtile"></param>
    private void CopyAllBorderProperties(MG_Tile _mgtile, MG_Tile _mgtileN, Direction _dir)
    {
        switch (_dir)
        {
            case Direction.North:
                _mgtile.PropWall_N = _mgtileN.PropWall_S;
                _mgtile.PropEntry_N = _mgtileN.PropEntry_S;
                break;
            case Direction.East:
                _mgtile.PropWall_E = _mgtileN.PropWall_W;
                _mgtile.PropEntry_E = _mgtileN.PropEntry_W;
                break;
            case Direction.South:
                _mgtile.PropWall_S = _mgtileN.PropWall_N;
                _mgtile.PropEntry_S = _mgtileN.PropEntry_N;
                break;
            case Direction.West:
                _mgtile.PropWall_W = _mgtileN.PropWall_E;
                _mgtile.PropEntry_W = _mgtileN.PropEntry_E;
                break;
        }
    }
}
