using UnityEngine;


public enum EditorMode { Floor, Walls, Doors, Windows, Props, None };//Режим редактирования

public class MG_Editor : MonoBehaviour
{
    /// <summary>
    ///ОПИСАНИЕ КЛАССА: данный класс содержит все полученные свойства из источника (окно выбора свойства), чтобы затем конструкторы могли их взять
    /// </summary>
    public static MG_Editor Instance { get; set; }//синглетон 
    
    [SerializeField]
    private MG_TileMap activeMap;//[R] активная карта
    [SerializeField]
    private int floorNum = 0;//этаж
    [SerializeField]
    private EditorMode editorMode = EditorMode.None;//выбранный тип редактирования
    [SerializeField]
    private FloorType floorTypeMode = FloorType.None;//выбранный тип редактирования для пола. (Между Carpet,Grass и None) Так как у нас есть три UI кнопки! Нужен для мыши, когда нужно знать: ластик или обычный пол ложить нужно
    [SerializeField]
    private WallType wallTypeMode = WallType.None;//выбранный тип редактирования для стен.
    [SerializeField]
    private EntryType entryType = EntryType.None;//тип входа
    [SerializeField]
    private Direction direction = Direction.None;//Направление
    [SerializeField]
    private ProjectionType projectionType = ProjectionType.Isometric2D;//проекция
    public ProjectionType ProjectionType { get => projectionType; set => projectionType = value; }
   
    [SerializeField]
    private Color floorColor = Color.white;//цвет комнаты
    [SerializeField]
    private bool roomMode = true;//создать пол со стенами (комнату по цвету)
    [SerializeField]
    private string floorName = "N/A";//Показываем какое свойство пола выбрано [ДЛЯ РАЗРАБОТЧИКА]
    [SerializeField]
    private string grassName = "N/A";//Показываем какое свойство пола выбрано [ДЛЯ РАЗРАБОТЧИКА]
    [SerializeField]
    private string wallName = "N/A";//Показываем какое свойство стены выбрано [ДЛЯ РАЗРАБОТЧИКА]
    [SerializeField]
    private string basicWallName = "N/A";//Показываем какое свойство базовой стены выбрано [ДЛЯ РАЗРАБОТЧИКА]
    [SerializeField]
    private string doorName = "N/A";//Показываем какое свойство двери выбрано [ДЛЯ РАЗРАБОТЧИКА]
    [SerializeField]
    private string windowName = "N/A";//Показываем какое свойство окна выбрано [ДЛЯ РАЗРАБОТЧИКА]

    private MG_FloorProperty floorProperty;//свойство пола
    private MG_FloorProperty grassProperty;//свойство пола для травы. Причина: в редакторе правым кликом сажаем траву, поэтому нужно запомнить
    private MG_WallProperty basicwallProperty;//свойство БАЗОВОЙ стены
    private MG_WallProperty wallProperty;//свойство стены
    private MG_EntryProperty doorProperty;//свойство двери
    private MG_EntryProperty windowProperty;//свойство окна
    private MG_PropProperty propProperty;//свойство окна

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("MG_Editor Awake(): MG_Editor может быть только один компонент на Сцене, другие не нужны.");
        if (activeMap == null)
            Debug.Log("<color=red>MG_Editor Awake(): объект для activeMap не прикреплен!</color>");
    }

    //--------------------------------------------------------

    /// <summary>
    /// Установить тип входа
    /// </summary>
    /// <param name="_type"></param>
    public void SetEntryType(EntryType _type)
    {
        entryType = _type;
    }

    /// <summary>
    /// Получить тип входа
    /// </summary>
    /// <returns></returns>
    public EntryType GetEntryType()
    {
        return entryType;
    }

    //--------------------------------------------------------

    /// <summary>
    /// Установить этаж
    /// </summary>
    /// <param name="_floorN"></param>
    public void SetFloorNumber(int _floorN)
    {
        floorNum = _floorN;
    }

    /// <summary>
    /// Получить этаж
    /// </summary>
    /// <returns></returns>
    public int GetFloorNumber()
    {
        return floorNum;
    }


    //--------------------------------------------------------

    /// <summary>
    /// Задать направление. Необходимо для стен, предметов и тд.
    /// </summary>
    /// <param name="_dir"></param>
    public void SetDirection(Direction _dir)
    {
        direction = _dir;
    }

    /// <summary>
    /// Получить направление. Необходимо для стен, предметов и тд.
    /// </summary>
    /// <returns></returns>
    public Direction GetDirection()
    {
        return direction;
    }

    //--------------------------------------------------------

    /// <summary>
    /// Задать режим редактирования
    /// </summary>
    /// <param name="_mode"></param>
    public void SetEditorMode(EditorMode _mode)
    {
        editorMode = _mode;
    }

    /// <summary>
    /// Получить режим редактирования
    /// </summary>
    /// <returns></returns>
    public EditorMode GetEditorMode()
    {
        return editorMode;
    }

    //--------------------------------------------------------

    /// <summary>
    /// Задать режим вида пола
    /// </summary>
    /// <param name="_mode"></param>
    public void SetFloorTypeMode(FloorType _mode)
    {
        floorTypeMode = _mode;
    }

    /// <summary>
    /// Получить режим вида пола
    /// </summary>
    /// <returns></returns>
    public FloorType GetFloorTypeMode()
    {
        return floorTypeMode;
    }

    //--------------------------------------------------------

    /// <summary>
    /// Задать цвет комнаты. В основном задается через цветовую палитру
    /// </summary>
    /// <param name="_color"></param>
    public void SetFloorColor(Color _color)
    {
        floorColor = _color;
    }

    /// <summary>
    /// Получить цвет комнаты
    /// </summary>
    /// <returns></returns>
    public Color GetFloorColor()
    {
        return floorColor;
    }

    //--------------------------------------------------------

    /// <summary>
    /// Задать свойство стены
    /// </summary>
    /// <param name="_property"></param>
    public void SetBasicWallProperty(MG_WallProperty _property)
    {
        basicwallProperty = _property;
        basicWallName = _property.Name;
    }

    /// <summary>
    /// Получить свойство стены
    /// </summary>
    /// <returns></returns>
    public MG_WallProperty GetBasicWallProperty()
    {
        return basicwallProperty;
    }
    //--------------------------------------------------------

    /// <summary>
    /// Задать свойство стены
    /// </summary>
    /// <param name="_property"></param>
    public void SetWallProperty(MG_WallProperty _property)
    {
        wallProperty = _property;
        wallName = _property.Name;
    }

    /// <summary>
    /// Получить свойство стены
    /// </summary>
    /// <returns></returns>
    public MG_WallProperty GetWallProperty()
    {
        return wallProperty;
    }

    //--------------------------------------------------------

    /// <summary>
    /// Запоминаем выбранное свойство (свойство выбирается в другом окне, напр: окно выбора пола)
    /// </summary>
    /// <param name="_property"></param>
    public void SetFloorProperty(MG_FloorProperty _property)
    {
        FloorType _type = _property.Type;
        switch (_type)
        {
            case FloorType.Carpet:
                {
                    floorProperty = _property;//задаем полученное свойство
                    floorName = _property.Name;//указываем наименование выбранного свойства
                    break;
                }
            case FloorType.Grass:
                {
                    grassProperty = _property;//задаем полученное свойство
                    grassName = _property.Name;//указываем наименование выбранного свойства
                    break;
                }

            default:
                {
                    Debug.Log("MG_Editor SetFloorProperty: Неизвестный тип пола!" + _type);
                    break;
                }
        }
    }

    /// <summary>
    /// Возвращаем выбранное свойство пола
    /// </summary>
    /// <returns></returns>
    public MG_FloorProperty GetFloorProperty(FloorType _type)
    {
        switch (_type)
        {
            case FloorType.Carpet:
                {
                    return floorProperty;//Возвращаем выбранное свойство пола

                }
            case FloorType.Grass:
                {
                    return grassProperty;//Возвращаем выбранное свойство пола
                }

            default:
                {
                    Debug.Log("MG_Editor GetFloorProperty: Неизвестный тип пола! " + _type);
                    return null;
                }
        }
    }

    //--------------------------------------------------------


    /// <summary>
    /// Запоминаем выбранное свойство (свойство выбирается в другом окне, напр: окно выбора пола)
    /// </summary>
    /// <param name="_property"></param>
    public void SetEntryProperty(MG_EntryProperty _property)
    {
        EntryType _type = _property.Type;
        switch (_type)
        {
            case EntryType.Door:
                {
                    doorProperty = _property;//задаем полученное свойство
                    doorName = _property.Name;//указываем наименование выбранного свойства
                    break;
                }
            case EntryType.Window:
                {
                    windowProperty = _property;//задаем полученное свойство
                    windowName = _property.Name;//указываем наименование выбранного свойства
                    break;
                }

            default:
                {
                    Debug.Log("MG_Editor SetEntryProperty: Неизвестный тип! " + _type);
                    break;
                }
        }
    }

    /// <summary>
    /// Возвращаем выбранное свойство пола
    /// </summary>
    /// <returns></returns>
    public MG_EntryProperty GetEntryProperty(EntryType _type)
    {
        switch (_type)
        {
            case EntryType.Door:
                {
                    return doorProperty;//Возвращаем выбранное свойство

                }
            case EntryType.Window:
                {
                    return windowProperty;//Возвращаем выбранное свойство
                }

            default:
                {
                    Debug.Log("MG_Editor GetEntryProperty: Неизвестный тип! " + _type);
                    return null;
                }
        }
    }

    //--------------------------------------------------------

    /// <summary>
    /// Установить свойство Prop
    /// </summary>
    /// <param name="_prop"></param>
    public void SetPropProperty(MG_PropProperty _prop)
    {
        propProperty = _prop;

    }

    /// <summary>
    /// Получить свойство Prop
    /// </summary>
    /// <returns></returns>
    public MG_PropProperty GetPropProperty()
    {
        return propProperty;
    }


    //--------------------------------------------------------

    /// <summary>
    /// Задаем нужно ли при установке пола создавать стены (комнату) в зависимости от цвета пола (зоны)
    /// </summary>
    /// <param name="_value"></param>
    public void SetRoomMode(bool _value)
    {
        roomMode = _value;
    }

    /// <summary>
    /// Получаем значение Режима комнат
    /// </summary>
    /// <returns></returns>
    public bool GetRoomMode()
    {
        return roomMode;
    }

    //--------------------------------------------------------

    /// <summary>
    /// Установить вид редактирования стен
    /// </summary>
    /// <param name="_type"></param>
    public void SetWallTypeMode(WallType _type)
    {
        wallTypeMode = _type;
    }

    /// <summary>
    /// Получить вид редактирования стен
    /// </summary>
    /// <returns></returns>
    public WallType GetWallTypeMode()
    {
        return wallTypeMode;
    }

    //--------------------------------------------------------

    /// <summary>
    /// Получить активную карту
    /// </summary>
    /// <returns></returns>
    public MG_TileMap GetCurrentMap()
    {
        return activeMap;
    }

    /// <summary>
    /// Установить активную карту
    /// </summary>
    /// <param name="_map"></param>
    public void SetCurrentMap(MG_TileMap _map)
    {
        activeMap = _map;
    }

  
}
