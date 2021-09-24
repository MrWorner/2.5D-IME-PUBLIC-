using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_EntryConstructor : MonoBehaviour
{
    private static MG_EntryConstructor instance;//в редакторе только один объект должен быть создан
    [SerializeField]
    private string prevId;//для запоминания предыдущего ID свойства.
    [SerializeField]
    private Vector3Int prevPos;//Предыдущая выбранная позиция клетки
    [SerializeField]
    private Direction prevDir;//Предыдущая выбранное направление объекта
    [SerializeField]
    private EditorMode prevEdMode;//Предыдущая выбранное режим редактирования
    [SerializeField]
    private EntryType prevEntryType;//предыдущий тип entry (нужен только для удаления)
    [SerializeField]
    private MG_TileMap prevMap;//Предыдущая активная карта
    [SerializeField]
    private int prevFloorN;//предыдущий этаж
    [SerializeField]
    private EntryType prevEntyType;//предыдущий тип входа

    [SerializeField]
    private MG_NeighbourManager neighbourManager;//[R] менеджер соседей
    [SerializeField]
    private MG_WallConstructor wallConstructor;//[R] конструктор стен
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MG_EntryConstructor Awake(): MG_EntryConstructor может быть только один компонент на Сцене, другие не нужны.</color>");
        if (neighbourManager == null)
            Debug.Log("<color=red>MG_EntryConstructor Awake(): MG_NeighbourManager не прикреплен!</color>");
        if (wallConstructor == null)
            Debug.Log("<color=red>MG_EntryConstructor Awake(): MG_WallConstructor не прикреплен!</color>");
        if (editor == null)
            Debug.Log("<color=red>MG_EntryConstructor Awake(): MG_Editor не прикреплен!</color>");
    }

    /// <summary>
    /// Метод вызывается после клика мыши по карте, там где будет создан или заменена стена. Но перед этим идет проверка чтобы не выполнять действие на одной и той же клетке более 1 раза
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_tileProp"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void SafeConstruct(Vector3Int _pos, MG_EntryProperty _tileProp, Direction _dir, MG_TileMap _map, int _floorN)
    {
        string _id = _tileProp.ID;//вытаскиваем ID стены
        EditorMode _edMode = editor.GetEditorMode();//получаем текущий режим редактирования

        //ПРОВЕРКА. Чтобы не выполнять действие на одной и той же клетке более 1 раза
        if
           (
            !_pos.Equals(prevPos)//проверяем что не тоже самое свойство
            || !_dir.Equals(prevDir)//проверяем что направление объекта не тоже самое
            || !_id.Equals(prevId)//проверяем что свойство ID не предыдущее
            || !_edMode.Equals(prevEdMode)//проверяем что свойство ID не предыдущее
            || !_map.Equals(prevMap)//проверяем что не предыдущая карта
            || !_floorN.Equals(prevFloorN)//проверяем что не тот же этаж
            )
        {
            //BEGIN ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ
            prevId = _id;
            prevPos = _pos;
            prevDir = _dir;
            prevEdMode = _edMode;
            prevMap = _map;
            prevFloorN = _floorN;
            //END ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ
            Construct(_pos, _tileProp, _dir, _map, _floorN);//Выполняем сам метод создания/смена свойств и тектсуры
        }
    }

    /// <summary>
    /// Создание двери/окна (Entry)
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_tileProp"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void Construct(Vector3Int _pos, MG_EntryProperty _tileProp, Direction _dir, MG_TileMap _map, int _floorN)
    {
        bool _hasTile = _map.HasTile(_pos, _floorN);//Теперь нужно узнать есть ли тайл на карте
        if (_hasTile)
        {
            MG_Tile _mgTile = _map.GetMgTile(_pos);//Теперь нужно получить MG_Tile по координатам

            MapType _type = _map.GetMapType();//берем тип карты
            Tilemap _tileMap = _map.GetWallMapByDir(_dir, _floorN);//получаем нужную тайловую карту ДЛЯ СТЕН  

            int[] _XY = _map.GetCorrectXY(_type, _dir);//Получаем нужные X и Y для координат. Иначе стена будет не на той клетки
            Vector3Int _correctedPos = new Vector3Int(_mgTile.Pos.x + _XY[0], _mgTile.Pos.y + _XY[1], 0);
            Tile _tile = GetCorrectTile(_type, _dir, _tileProp);//подготавливаем сам визуальный тайл для карты в зависимости от вида карты
            SetPropertyForMgTile(_dir, _tileProp, _mgTile);//Установить свойство к правильному полю MG_Tile в зависимости от направления
            //SetLogic(_dir, _mgTile, true);//Настраиваем логические свойства тайла для стен

            MG_Tile _mgTileN = neighbourManager.GetNeigbourByDir(_pos, _dir, _map);//получаем соседа по направлению
            if (_mgTileN != null)//если сосед есть
            {
                SetPropertyForMgTile(MG_Direction.ReverseDir(_dir), _tileProp, _mgTileN);//Установить свойство к правильному полю MG_Tile в зависимости от направления соседу
                //SetLogic(MG_Direction.ReverseDir(_dir), _mgTileN, true);//задаем соседу логические свойства
            }
             
            _tileMap.SetTile(_correctedPos, _tile);//устанавливаем визуальный тайл
            neighbourManager.CheckSetAllNeigbrours(_mgTile, true, _map);//запускаем метод по настройки соседей и нодов 
        }
        else
        {
            //Если кликнуто на пустое, то создаем только у соседа
            MG_Tile _mgTileN = neighbourManager.GetNeigbourByDir(_pos, _dir, _map);//получаем соседа по направлению
            if (_mgTileN != null)//если сосед есть
            {
                Vector3Int _posN = _mgTileN.Pos;//получить позицию соседа
                Construct(_posN, _tileProp, MG_Direction.ReverseDir(_dir), _map, _floorN);//РЕКУРСИЯ, перезапускаем метод, назначая соседа - _mgTile 
            }
        }
    }

    /// <summary>
    /// Получить нужный тайл в зависимости от типа карты и направления
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_dir"></param>
    /// <param name="_tileProp"></param>
    /// <returns></returns>
    private Tile GetCorrectTile(MapType _type, Direction _dir, MG_EntryProperty _tileProp)
    {
        Tile[] _array2D = new Tile[] { _tileProp.Tile2D_H, _tileProp.Tile2D_V };
        Tile[] _arrayIso2D = new Tile[] { _tileProp.Tile2DIso_H, _tileProp.Tile2DIso_V };
        Tile[] _arrayChosen;
        Tile _chosenTile;

        switch (_type)
        {
            case MapType.TopDown2D:
                {
                    _arrayChosen = _array2D;
                    break;
                }
            case MapType.Isometric2D:
                {
                    _arrayChosen = _arrayIso2D;
                    break;
                }
            default:
                {
                    Debug.Log("<color=red>MG_EntryConstructor GetCorrectTile(): Тип карты нереализован!</color> " + _type);
                    return null;
                }
        }

        switch (_dir)
        {
            case Direction.North:
                {
                    return _chosenTile = _arrayChosen[0];
                }
            case Direction.South:
                {
                    return _chosenTile = _arrayChosen[0];
                }
            case Direction.East:
                {
                    return _chosenTile = _arrayChosen[1];
                }
            case Direction.West:
                {
                    return _chosenTile = _arrayChosen[1];
                }
            default:
                {
                    Debug.Log("<color=red>MG_EntryConstructor GetCorrectTile(): недоступный тип направления!</color> " + _dir);
                    return null;
                }
        }
    }


    /// <summary>
    /// Установить свойство к правильному полю MG_Tile в зависимости от направления
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_tileProp"></param>
    /// <param name="_mgTile"></param>
    private void SetPropertyForMgTile(Direction _dir, MG_EntryProperty _tileProp, MG_Tile _mgTile)
    {
        switch (_dir)
        {
            case Direction.North:
                {
                    _mgTile.PropEntry_N = _tileProp;
                    break;
                }
            case Direction.South:
                {
                    _mgTile.PropEntry_S = _tileProp;
                    break;
                }
            case Direction.East:
                {
                    _mgTile.PropEntry_E = _tileProp;
                    break;
                }
            case Direction.West:
                {
                    _mgTile.PropEntry_W = _tileProp;
                    break;
                }
            default:
                {
                    Debug.Log("<color=red>MG_EntryConstructor SetCorrectlyPropForMgTile(): недоступный тип направления!</color> " + _dir);
                    break;
                }
        }
    }

    /// <summary>
    /// Настраиваем логические свойства тайла для стен
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_mgTile"></param>
    /// <param name="_entry"></param>
    //private void SetLogic(Direction _dir, MG_Tile _mgTile, bool _entry)
    //{
    //    //Debug.Log("SetGhostWalls _mgTile = " + _mgTile.Name + " _direction" + _direction.ToString() + " result = " + result + " createWalls = " + createWalls);
    //    switch (_dir)
    //    {
    //        case Direction.North:
    //            {
    //                //_mgTile.HasWall_N = true;
    //                _mgTile.HasEntry_N = _entry;
    //                //_mgTile.Barrier_N = true;
    //                break;
    //            }
    //        case Direction.South:
    //            {
    //                //_mgTile.HasWall_S = true;
    //                _mgTile.HasEntry_S = _entry;
    //                //_mgTile.Barrier_S = true;
    //                break;
    //            }
    //        case Direction.East:
    //            {
    //                //_mgTile.HasWall_E = true;
    //                _mgTile.HasEntry_E = _entry;
    //                //_mgTile.Barrier_E = true;
    //                break;
    //            }
    //        case Direction.West:
    //            {
    //                //_mgTile.HasWall_W = true;
    //                _mgTile.HasEntry_W = _entry;
    //                //_mgTile.Barrier_W = true;
    //                break;
    //            }

    //        default: break;
    //    }
    //}

    /// <summary>
    /// Безопасное удаление Entry для клика мыши с проверками
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void SafeRemove(Vector3Int _pos, Direction _dir, MG_TileMap _map, int _floorN, EntryType _type)
    {
        string _id = "-1";//делаем -1 ID свойства (удаление)
        EditorMode _edMode = editor.GetEditorMode();//получаем текущий режим редактирования

        //ПРОВЕРКА. Чтобы не выполнять действие на одной и той же клетке более 1 раза
        if
           (
            !_pos.Equals(prevPos)//проверяем что не тоже самое свойство
            || !_dir.Equals(prevDir)//проверяем что направление объекта не тоже самое
            || !_id.Equals(prevId)//проверяем что свойство ID не предыдущее
            || !_edMode.Equals(prevEdMode)//проверяем что режим не предыдущий
            || !_map.Equals(prevMap)//проверяем что не предыдущая активная карта
            || !_type.Equals(prevEntyType)//проверяем что не предыдущая активная карта
            )
        {
            //BEGIN ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ
            prevId = _id;
            prevPos = _pos;
            prevDir = _dir;
            prevEdMode = _edMode;
            prevMap = _map;
            prevEntyType = _type;
            //END ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ

            Remove(_pos, _dir, _map, _floorN, _type);//Выполняем сам метод создания/смена свойств и тектсуры пола
        }
    }

    /// <summary>
    /// Удаление Entry
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void Remove(Vector3Int _pos, Direction _dir, MG_TileMap _map, int _floorN, EntryType _type)
    {

        bool _hasTile = _map.HasTile(_pos, _floorN);//Теперь нужно узнать есть ли тайл на карте, иначе ничего не делать
        if (_hasTile)
        {
            MG_Tile _mgTile = _map.GetMgTile(_pos);//Теперь нужно получить свойство самого тайла
            bool _hasEntry = _mgTile.HasEntry(_dir);//проверяем есть ли Entry в заданном направлении
            if (_hasEntry)//есть Entry
            {
                EntryType _typeS = GetTypeByDir(_dir, _mgTile);
                if (_type.Equals(_typeS))
                {
                    MG_WallProperty _property = editor.GetBasicWallProperty();//получаем свойство стены                  
                    wallConstructor.Construct(_pos, _property, _dir, _map, _floorN);//создаем стену    

                    SetPropertyForMgTile(_dir, null, _mgTile);//Установить свойство к правильному полю MG_Tile в зависимости от направления
                }
            }
        }
        else
        {
            //Если кликнуто на пустое, то удаляем только у соседа
            MG_Tile _mgTileN = neighbourManager.GetNeigbourByDir(_pos, _dir, _map);//получаем соседа по направлению
            if (_mgTileN != null)//если сосед есть
            {
                Direction _dirN = MG_Direction.ReverseDir(_dir);//перенаправляем направление
                bool _hasEntry = _mgTileN.HasEntry(_dirN);//проверяем есть ли Entry в заданном направлении
                if (_hasEntry)//есть Entry
                {
                    EntryType _typeS = GetTypeByDir(_dirN, _mgTileN);
                    if (_type.Equals(_typeS))
                    {
                        Vector3Int _posN = _mgTileN.Pos;//получаем координаты соседа
                        MG_WallProperty _property = editor.GetBasicWallProperty();//получаем свойство стены                  
                        wallConstructor.Construct(_posN, _property, _dirN, _map, _floorN);//создаем стену  

                        SetPropertyForMgTile(_dirN, null, _mgTileN);//Установить свойство к правильному полю MG_Tile в зависимости от направления
                    }
                }
            }
        }
    }

    /// <summary>
    /// Безопасное удаление всех Entry для клика мыши
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void SafeRemoveAll(Vector3Int _pos, MG_TileMap _map, int _floorN, EntryType _type)
    {
        string _id = "-1";//делаем -1 ID пола (удаление)
        EditorMode _edMode = editor.GetEditorMode();//получаем текущий режим редактирования

        //ПРОВЕРКА. Чтобы не выполнять действие на одной и той же клетке более 1 раза
        if
           (
            !_pos.Equals(prevPos)//проверяем что не тоже самое свойство
            || !_id.Equals(prevId)//проверяем что свойство ID не предыдущее
            || !_edMode.Equals(prevEdMode)//проверяем что режим не предыдущий
            || !_map.Equals(prevMap)//проверяем что не предыдущая активная карта
            || !_type.Equals(prevEntryType)//проверяем что не предыдущий тип Entry
            )
        {
            //BEGIN ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ
            prevId = _id;
            prevPos = _pos;
            prevEdMode = _edMode;
            prevMap = _map;
            prevEntryType = _type;
            //END ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ

            foreach (var _dir in MG_Direction.Basic)//берем каждое направление
            {
                Remove(_pos, _dir, _map, _floorN, _type);//Выполняем удаление стены
            }
            prevDir = Direction.None;//Чтобы работало строительство после удаления на той же клетки
        }
    }

    /// <summary>
    /// Проверить есть ли entry в заданном направлении
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_mgTile"></param>
    /// <returns></returns>
    //public bool HasEntryByDir(Direction _dir, MG_Tile _mgTile)
    //{
    //    switch (_dir)
    //    {
    //        case Direction.North:
    //            {
    //                return _mgTile.HasEntry_N;
    //            }
    //        case Direction.South:
    //            {
    //                return _mgTile.HasEntry_S;
    //            }
    //        case Direction.East:
    //            {
    //                return _mgTile.HasEntry_E;
    //            }
    //        case Direction.West:
    //            {
    //                return _mgTile.HasEntry_W;
    //            }
    //    }
    //    //Debug.Log("MG_WallConstructor HasWallByDir(): Ошибка");
    //    return false;
    //}

    /// <summary>
    /// Получить тип entry по заданному направлению 
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_mgTile"></param>
    /// <returns></returns>
    private EntryType GetTypeByDir(Direction _dir, MG_Tile _mgTile)
    {
        switch (_dir)
        {
            case Direction.North:
                {
                    return _mgTile.PropEntry_N.Type;
                }
            case Direction.South:
                {
                    return _mgTile.PropEntry_S.Type;
                }
            case Direction.East:
                {
                    return _mgTile.PropEntry_E.Type;
                }
            case Direction.West:
                {
                    return _mgTile.PropEntry_W.Type;
                }
        }
        Debug.Log("<color=red>MG_WallConstructor GetTypeByDir(): Ошибка</color>");
        return EntryType.None;
    }

    /// <summary>
    /// Получить свойство стены заданного направления у клетки 
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_mgTile"></param>
    /// <returns></returns>
    //public MG_WallProperty GetPropByDir(Direction _dir, MG_Tile _mgTile)
    //{
    //    switch (_dir)
    //    {
    //        case Direction.North:
    //            {
    //                return _mgTile.propWall_N;
    //            }
    //        case Direction.South:
    //            {
    //                return _mgTile.propWall_S;
    //            }
    //        case Direction.East:
    //            {
    //                return _mgTile.propWall_E;
    //            }
    //        case Direction.West:
    //            {
    //                return _mgTile.propWall_W;
    //            }
    //    }
    //    Debug.Log("MG_WallConstructor GetPropByDir(): Ошибка");
    //    return null;
    //}
}