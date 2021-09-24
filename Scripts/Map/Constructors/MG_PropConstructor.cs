using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_PropConstructor : MonoBehaviour
{
    private static MG_PropConstructor instance;//в редакторе только один объект должен быть создан
    [SerializeField]
    private string prevId;//для запоминания предыдущего ID свойства.
    [SerializeField]
    private Vector3Int prevPos;//Предыдущая выбранная позиция клетки
    [SerializeField]
    private Direction prevDir;//Предыдущая выбранное направление объекта
    [SerializeField]
    private EditorMode prevEdMode;//Предыдущая выбранное режим редактирования
    [SerializeField]
    private MG_TileMap prevMap;//Предыдущая активная карта
    [SerializeField]
    private int prevFloorN;//предыдущий этаж

    [SerializeField]
    private MG_NeighbourManager neighbourManager;//[R] менеджер соседей
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MG_PropConstructor Awake(): MG_WallConstructor может быть только один компонент на Сцене, другие не нужны.</color>");
        if (neighbourManager == null)
            Debug.Log("<color=red>MG_PropConstructor Awake(): MG_NeighbourManager не прикреплен!</color>");
        if (editor == null)
            Debug.Log("<color=red>MG_PropConstructor Awake(): MG_Editor не прикреплен!</color>");
    }

    /// <summary>
    /// Метод вызывается после клика мыши по карте, там где будет создан или заменен Prop объект. Но перед этим идет проверка чтобы не выполнять действие на одной и той же клетке более 1 раза
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_tileProp"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public bool SafeConstruct(Vector3Int _pos, MG_PropProperty _tileProp, Direction _dir, MG_TileMap _map, int _floorN)
    {
        string _id = _tileProp.ID;//вытаскиваем ID свойства
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
            return true;
        }
        return false;
    }

    /// <summary>
    /// Создание стены
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_tileProp"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void Construct(Vector3Int _pos, MG_PropProperty _tileProp, Direction _dir, MG_TileMap _map, int _floorN)
    {
        bool _hasTile = _map.HasTile(_pos, _floorN);//Теперь нужно узнать есть ли тайл на карте
        if (_hasTile)
        {
            MG_Tile _mgTile = _map.GetMgTile(_pos);//Теперь нужно получить MG_Tile по координатам

            MapType _type = _map.GetMapType();//берем тип карты
            Tilemap _tileMap = _map.GetPropMap(_floorN);//получаем нужную тайловую карту ДЛЯ СТЕН  

            Tile _tile = GetCorrectTile(_type, _dir, _tileProp);//подготавливаем сам визуальный тайл для карты в зависимости от вида карты
            SetPropertyForMgTile(_tileProp, _mgTile);//Установить свойство к правильному полю MG_Tile
            SetLogic(_mgTile, true, _dir);//Настраиваем логические свойства тайла для стен
               
            _tileMap.SetTile(_pos, _tile);//устанавливаем визуальный тайл
            neighbourManager.CheckSetAllNeigbrours(_mgTile, true, _map);//запускаем метод по настройки соседей и нодов
        }
    }

    /// <summary>
    /// Получить нужный тайл в зависимости от типа карты и направления
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_dir"></param>
    /// <param name="_tileProp"></param>
    /// <returns></returns>
    private Tile GetCorrectTile(MapType _type, Direction _dir, MG_PropProperty _tileProp)
    {
        Tile[] _arrayIso2D = new Tile[] { _tileProp.Tile2DIso_N, _tileProp.Tile2DIso_E, _tileProp.Tile2DIso_S, _tileProp.Tile2DIso_W };
        Tile[] _array2D = new Tile[] { _tileProp.Tile2D_N, _tileProp.Tile2D_E, _tileProp.Tile2D_S, _tileProp.Tile2D_W };
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
                    Debug.Log("MG_PropConstructor GetCorrectTile(): Тип карты нереализован! " + _type);
                    return null;
                }
        }

        switch (_dir)
        {
            case Direction.North:
                {
                    return _chosenTile = _arrayChosen[0];
                }
            case Direction.East:
                {
                    return _chosenTile = _arrayChosen[1];
                }
            case Direction.South:
                {
                    return _chosenTile = _arrayChosen[2];
                }
            case Direction.West:
                {
                    return _chosenTile = _arrayChosen[3];
                }
            default:
                {
                    Debug.Log("MG_PropConstructor GetCorrectTile(): недоступный тип направления! " + _dir);
                    return null;
                }
        }
    }

    /// <summary>
    /// Установить свойство к правильному полю MG_Tile в зависимости от направления
    /// </summary>
    /// <param name="_tileProp"></param>
    /// <param name="_mgTile"></param>
    private void SetPropertyForMgTile(MG_PropProperty _tileProp, MG_Tile _mgTile)
    {
        _mgTile.PropProperty = _tileProp;
    }

    /// <summary>
    /// Настраиваем логические свойства тайла
    /// </summary>
    /// <param name="_mgTile"></param>
    /// <param name="_hasProp"></param>
    /// <param name="_dir"></param>
    private void SetLogic(MG_Tile _mgTile, bool _hasProp, Direction _dir)
    {
        //Debug.Log("SetGhostWalls _mgTile = " + _mgTile.Name + " _direction" + _direction.ToString() + " result = " + result + " createWalls = " + createWalls);
        _mgTile.PropDir = _dir;
    }

    /// <summary>
    /// Безопасное удаление для клика мыши с проверками
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_dir"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void SafeRemove(Vector3Int _pos, MG_TileMap _map, int _floorN)
    {
        string _id = "-1";//делаем -1 ID свойства (удаление)
        EditorMode _edMode = editor.GetEditorMode();//получаем текущий режим редактирования

        //ПРОВЕРКА. Чтобы не выполнять действие на одной и той же клетке более 1 раза
        if
           (
            !_pos.Equals(prevPos)//проверяем что не тоже самое свойство
            || !_id.Equals(prevId)//проверяем что свойство ID не предыдущее
            || !_edMode.Equals(prevEdMode)//проверяем что режим не предыдущий
            || !_map.Equals(prevMap)//проверяем что не предыдущая активная карта
            )
        {
            //BEGIN ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ
            prevId = _id;
            prevPos = _pos;
            prevEdMode = _edMode;
            prevMap = _map;
            //END ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ

            Remove(_pos, _map, _floorN);//Выполняем сам метод создания/смена свойств и тектсуры пола
        }
    }

    /// <summary>
    /// Удаление
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void Remove(Vector3Int _pos, MG_TileMap _map, int _floorN)
    {
        bool _hasTile = _map.HasTile(_pos, _floorN);//Теперь нужно узнать есть ли тайл на карте, иначе ничего не делать
        if (_hasTile)
        {
            MG_Tile _mgTile = _map.GetMgTile(_pos);//Теперь нужно получить свойство самого тайла
            Tilemap _tileMap = _map.GetPropMap(_floorN);//получаем нужную тайловую карту                      
            SetLogic(_mgTile, false, Direction.None);//Настраиваем логические свойства тайла для стен
         
            _tileMap.SetTile(_pos, null);//устанавливаем тайл
            SetPropertyForMgTile(null, _mgTile);//Установить свойство к правильному полю MG_Tile
            neighbourManager.CheckSetAllNeigbrours(_mgTile, true, _map);//запускаем метод по настройки соседей и нодов
        }
    }

}
