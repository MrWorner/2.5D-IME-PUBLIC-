using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MGUI_DimSwitcher : MonoBehaviour, iDimension
{
    [SerializeField]
    private MG_Editor editor;//[R] редактор
    [SerializeField]
    private MGJSON_TileConstructor json_TileConstructor;//[R]
    [SerializeField]
    private MGSJON_TileDecryptor json_TileDecryptor;//[R]

    [SerializeField]
    private MG_TileMap map2D;//[R] ссылка на карту 2D
    [SerializeField]
    private MG_TileMap map2DISO;//[R] Ссылка на карту ISO
    //[SerializeField]
    private MG_TileMap mapChosen; //выбранная карта у ISO

    [SerializeField]
    private Image buttonImg_2D; //ссылка на кнопку переключения режима 2Д
    [SerializeField]
    private Image buttonImg_ISO; //ссылка на кнопку переключения режима ISO

    [SerializeField]
    private MGUI_FGE UI_FGE;//[R] нужен для Ресета после переключения режима

    [SerializeField]
    private List<MGUI_Tool> UItoolClasses;//лист классов с интерфейсом iDimension
    //private List<iDimension> dimensionClasses;

    [SerializeField]
    private Button Button_2DTD;//[D]
    [SerializeField]
    private Button Button_2DIso;//[D]


    private void Awake()
    {
        if (editor == null)
            Debug.Log("<color=red>MGUI_DimSwitcher Awake(): MG_UIdimSpace не прикреплен!</color>");
        if (json_TileConstructor == null)
            Debug.Log("<color=red>MGUI_DimSwitcher Awake(): MGJSON_TileConstructor не прикреплен!</color>");
        if (json_TileDecryptor == null)
            Debug.Log("<color=red>MGUI_DimSwitcher Awake(): MGSJON_TileDecryptor не прикреплен!</color>");
        if (map2DISO == null)
            Debug.Log("<color=red>MGUI_DimSwitcher Awake(): объект для mapISO не прикреплен!</color>");
        if (map2D == null)
            Debug.Log("<color=red>MGUI_DimSwitcher Awake(): объект для map2D не прикреплен!</color>");
        if (UI_FGE == null)
            Debug.Log("<color=red>MGUI_DimSwitcher Awake(): MGUI_FGE не прикреплен!</color>");
        mapChosen = map2DISO;
    }

    private void Start()
    {
        //Button_2DTD.onClick.Invoke();
        Button_2DIso.onClick.Invoke();
    }

    /// <summary>
    /// Поменять цвет кнопки
    /// </summary>
    /// <param name="_ProjType"></param>
    private void ChangeImageColor(ProjectionType _ProjType)
    {
        switch (_ProjType)
        {
            case ProjectionType.TopDown2D:
                buttonImg_2D.color = Color.yellow;
                buttonImg_ISO.color = Color.white;
                break;
            case ProjectionType.Isometric2D:
                buttonImg_2D.color = Color.white;
                buttonImg_ISO.color = Color.yellow;
                break;
        }
    }

    /// <summary>
    /// После смены карты исправить положение карты
    /// </summary>
    private void FixCameraPos()
    {
        int _floorN = editor.GetFloorNumber();
        Vector3 _pos = Camera.main.transform.position;
        Vector3Int _posLocal = mapChosen.GetCellPos(_pos, _floorN);
        _pos = mapChosen.GetWorldPos(_posLocal, _floorN);
        Camera.main.transform.position = new Vector3(_pos.x, _pos.y, -10f);
    }

    /// <summary>
    /// Изменить видимость карт
    /// </summary>
    /// <param name="_ProjType"></param>
    private void SwitchMapVisibility(ProjectionType _ProjType)
    {
        switch (_ProjType)
        {
            case ProjectionType.TopDown2D:
                map2D.gameObject.SetActive(true);
                map2DISO.gameObject.SetActive(false);
                break;
            case ProjectionType.Isometric2D:
                map2D.gameObject.SetActive(false);
                map2DISO.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Переключить режим карты на 2D TOP DOWN
    /// </summary>
    public void On2DTopDown()
    {

        MGJSON_TileContainer _container = json_TileConstructor.ConstructContainer(mapChosen);//Сконструировать контейнер
        mapChosen.ClearMap();//Очистить карту      
        mapChosen = map2D;
        ProjectionType _type = ProjectionType.TopDown2D;
        ChangeImageColor(_type);//Поменять цвет кнопки
        SwitchMapVisibility(_type);//Изменить видимость карт
        ChangeIconsUI(_type);//ПоменятьUI иконки у классов с интерфейсом iDimension     
        FixCameraPos();//После смены карты исправить положение карты
        SendToEditor(mapChosen, _type);//Передать необходимые настройки редактору
        json_TileDecryptor.GenerateMap(_container, mapChosen);//Сгенерировать карту
    }

    /// <summary>
    /// Переключить режим карты на 2D ISO
    /// </summary>
    public void On2DIsometric()
    {
        MGJSON_TileContainer _container = json_TileConstructor.ConstructContainer(mapChosen);//Сконструировать контейнер
        mapChosen.ClearMap();//Очистить карту 
        mapChosen = map2DISO;
        ProjectionType _type = ProjectionType.Isometric2D;
        ChangeImageColor(_type);//Поменять цвет кнопки
        SwitchMapVisibility(_type);//Изменить видимость карт
        ChangeIconsUI(_type);//ПоменятьUI иконки у классов с интерфейсом iDimension    
        FixCameraPos();//После смены карты исправить положение карты
        SendToEditor(mapChosen, _type);//Передать необходимые настройки редактору
        json_TileDecryptor.GenerateMap(_container, mapChosen);//Сгенерировать карту
    }

    /// <summary>
    /// ПоменятьUI иконки у классов с интерфейсом iDimension
    /// </summary>
    /// <param name="_type"></param>
    private void ChangeIconsUI(ProjectionType _type)
    {
        switch (_type)
        {
            case ProjectionType.TopDown2D:
                foreach (var _class in UItoolClasses)
                {
                    _class.On2DTopDown();
                }
                break;
            case ProjectionType.Isometric2D:
                foreach (var _class in UItoolClasses)
                {
                    _class.On2DIsometric();
                }
                break;
            default:
                Debug.Log("<color=red>MGUI_DimSwitcher ChangeIconsUI(): нереализованный тип! </color>" + _type);
                break;
        }
    }

    /// <summary>
    /// Передать необходимые настройки редактору
    /// </summary>
    /// <param name="_map"></param>
    /// <param name="_projType"></param>
    private void SendToEditor(MG_TileMap _map, ProjectionType _projType)
    {
        editor.SetCurrentMap(_map);
        editor.ProjectionType = _projType;
    }

    /// <summary>
    /// РЕСЕТ ВСЕХ КНОПОК ПОСЛЕ ПЕРЕКЛЮЧЕНИЯ РЕЖИМОВ ПРОЕКЦИИ
    /// </summary>
    //private void ResetAllButtons()
    //{
    //    UI_FGE.Pressed_Floor();//типа кнопка Floor нажата для всего ресета
    //}

}
