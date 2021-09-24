using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_FloorConstructor : MonoBehaviour
{
    private static MG_FloorConstructor instance;//в редакторе только один объект должен быть создан
    [SerializeField]
    private Color prevColor;//для запоминания предыдущего цвета. 
    [SerializeField]
    private string prevId;//для запоминания предыдущего ID свойства пола.
    [SerializeField]
    private Vector3Int prevPos;//Предыдущая выбранная позиция клетки
    [SerializeField]
    private EditorMode prevEdMode;//Предыдущая выбранное режим редактирования
    [SerializeField]
    private MG_TileMap prevMap;//Предыдущая активная карта
    [SerializeField]
    private int prevFloorN;//предыдущий этаж

    [SerializeField]
    private MG_TileConstuctor tileConstuctor;//[R] конструктор MG_Lile
    [SerializeField]
    private MG_NeighbourManager neighbourManager;//[R] менеджер соседей
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MG_FloorConstructor Awake(): MG_FloorConstructor может быть только один компонент на Сцене, другие не нужны.</color>");
        if (tileConstuctor == null)
            Debug.Log("<color=red>MG_TileConstuctor Awake(): MG_TileConstuctor не прикреплен!</color>");
        if (neighbourManager == null)
            Debug.Log("<color=red>MG_FloorConstructor Awake(): MG_NeighbourManager не прикреплен!</color>");
        if (editor == null)
            Debug.Log("<color=red>MG_FloorConstructor Awake(): MG_Editor не прикреплен!</color>");
    }

    /// <summary>
    /// Метод вызывается после клика мыши по карте, там где будет создан или заменен пол. Но перед этим идет проверка чтобы не выполнять действие на одной и той же клетке более 1 раза
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_tileProp"></param>
    /// <param name="_color"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    /// 
    public bool SafeConstruct(Vector3Int _pos, MG_FloorProperty _tileProp, Color _color, MG_TileMap _map, int _floorN)
    {
        string _id = _tileProp.ID;//вытаскиваем ID пола
        EditorMode _edMode = editor.GetEditorMode();//получаем текущий режим редактирования

        //ПРОВЕРКА. Чтобы не выполнять действие на одной и той же клетке более 1 раза
        if
           (
            !_pos.Equals(prevPos)//проверяем что не тоже самое свойство пола
            || !_color.Equals(prevColor)//проверяем что это не тот же цвет комнаты
            || !_id.Equals(prevId)//проверяем что свойство ID не предыдущее
            || !_edMode.Equals(prevEdMode)//проверяем что режим не предыдущий
            || !_map.Equals(prevMap)//проверяем что режим не предыдущий
            || !_floorN.Equals(prevFloorN)//проверяем что не тот же этаж
            )
        {
            //BEGIN ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ
            prevId = _id;
            prevColor = _color;
            prevPos = _pos;
            prevMap = _map;
            prevFloorN = _floorN;
            //END ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ

            Construct(_pos, _tileProp, _color, _map, _floorN);//Выполняем сам метод создания/смена свойств и тектсуры пола
            return true;//Возвращаем True, пол создан или изменен
        }
        else
            return false;//Возвращаем False, ничего не создано
    }

    /// <summary>
    /// Метод для создания пола на карте
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_tileProp"></param>
    /// <param name="_color"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void Construct(Vector3Int _pos, MG_FloorProperty _tileProp, Color _color, MG_TileMap _map, int _floorN)
    {
        Tilemap _tilemap = _map.GetFloorMap(_floorN);//тайловая карта пола
        MapType _type = _map.GetMapType();//берем тип карты
        Tile _tile = GetTileForCurTypeOfMap(_type, _tileProp);//подготавливаем сам визуальный тайл для карты в зависимости от вида карты

        _tilemap.SetTile(_pos, _tile);//устанавливаем тайл
        _tilemap.SetTileFlags(_pos, TileFlags.None);//убираем флаги(ограничения), чтобы можно было закрасить цветом
        _tilemap.SetColor(_pos, _color);//Перекрашиваем тайл

        MG_Tile _mgTile = _map.GetMgTile(_pos);//Теперь нужно получить свойство самого тайла
        if (_mgTile == null)//если пустой mg_tile, то создаем новый
            _mgTile = tileConstuctor.CreateMgTile(_pos, _color, _map, _floorN);
        _mgTile.FloorProp = _tileProp;//задаем новое свойство
        _mgTile.Color = _color;//задаем новый цвет      

        neighbourManager.CheckSetAllNeigbrours(_mgTile, true, _map);//запускаем метод по настройки соседей и нодов
    }

    /// <summary>
    /// Метод для взятия правильного тайла в зависимости от типа карты (Top Down или Isometric)
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_tileProp"></param>
    /// <returns></returns>
    private Tile GetTileForCurTypeOfMap(MapType _type, MG_FloorProperty _tileProp)
    {
        switch (_type)
        {
            case MapType.TopDown2D:
                {
                    return _tileProp.Tile2D;
                }
            case MapType.Isometric2D:
                {
                    return _tileProp.Tile2DIso;
                }
            //case MapType.None:
            //    {
            //        Debug.Log("MG_FloorConstructor GetTileForCurTypeOfMap(): Тип карты не задан!");
            //        return null;
            //    }
            default:
                {
                    Debug.Log("MG_FloorConstructor GetCorrectTile(): Тип карты неопределен!");
                    return null;
                }
        }
    }

    /// <summary>
    /// Метод для очистки пола Безопасный способ с проверками для курсора.
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public bool SafeRemove(Vector3Int _pos, MG_TileMap _map, int _floorN)
    {

        string _id = "-1";//делаем (-1) ID пола, удаление
        EditorMode _edMode = editor.GetEditorMode();//получаем текущий режим редактирования
        
        //ПРОВЕРКА. Чтобы не выполнять действие на одной и той же клетке более 1 раза
        if
           (
            !_pos.Equals(prevPos)//проверяем что не тоже самое свойство пола
            || !_id.Equals(prevId)//проверяем что свойство ID не предыдущее
            || !_edMode.Equals(prevEdMode)//проверяем что режим не предыдущий
            || !_map.Equals(prevMap)//проверяем карта не предыдущая
            || !_floorN.Equals(prevFloorN)//проверяем что не тот же этаж
            )
        {
            //BEGIN ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ
            prevId = _id;
            prevEdMode = _edMode;
            prevPos = _pos;
            prevMap = _map;
            prevFloorN = _floorN;
            //END ЗАОПМИНАЕМ ПОЛУЧЕННЫЕ ЗНАЧЕНИЯ КАК ПРЕДЫДУЩИЕ

            Remove(_pos, _map, _floorN);//Выполняем сам метод удаление пола
            return true;
        }
        return false;
    }

    /// <summary>
    /// Метод для очистки пола
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    public void Remove(Vector3Int _pos, MG_TileMap _map, int _floorN)
    {      
        Tilemap _tileMap = _map.GetFloorMap(_floorN);//тайловая карта основной карты
        Tile _tile = _map.GetTile(_pos, _floorN);//получаем сам тайл по позиции
        if (_tile)//если существует, то приступаем к удалению
        {
            MG_Tile _mgTile = _map.GetMgTile(_pos);//берем свойство тайла со справочника с помощью ключа Позиция
            //TextMeshPro _label = _mgTile.Label;//вытаскиваем метку(текст) тайла (В ОСНОВНОМ ЭТО КООРДИНАТЫ ДЛЯ РАЗРАБОТЧИКА)
            //if (_label)//если есть, то делаем текст пустым
            //{
                //----Destroy(_label.gameObject);//уничтожаем текст клетки
            //    _label.text = "";
            //};
            _tileMap.SetTile(_pos, null);//убираем тайл на карте
            neighbourManager.RemoveAllNeigbrours(_mgTile, _map);//запускаем метод по настройки соседей и нодов   
            _map.RemoveMgTile(_pos);//удаляем из словаря
        }
    }
}
