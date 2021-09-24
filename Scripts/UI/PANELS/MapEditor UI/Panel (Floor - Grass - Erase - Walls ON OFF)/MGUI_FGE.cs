using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MGUI_FGE : MonoBehaviour
{
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора
    [SerializeField]
    private MG_Cursor cursor;//[R] курсор

    //----------СПРАЙТЫ ДЛЯ КУРСОРА
    [SerializeField]
    private Sprite cursorS_Floor_2D;//[R] спрайт пола для курсора
    [SerializeField]
    private Sprite cursorS_FloorEraser_2D;//[R] спрайт ластика пола для курсора
    [SerializeField]
    private Sprite cursorS_Floor_ISO;//[R] спрайт пола для курсора
    [SerializeField]
    private Sprite cursorS_FloorEraser_ISO;//[R] спрайт ластика пола для курсора

    //----------ИЗОБРАЖЕНИЕ КНОПОК
    [SerializeField]
    private Image sr_Floor;//[R] изображение кнопки "Floor"
    [SerializeField]
    private Image sr_Grass;//[R] изображение кнопки "Grass"
    [SerializeField]
    private Image sr_Eraser;//[R] изображение кнопки "Eraser"

    [SerializeField]
    private Image sr_WallsON;//[R] изображение кнопки "Walls ON"
    [SerializeField]
    private Image sr_WallsOFF;//[R] изображение кнопки "Walls OFF"

    private Image curButtonImage;//нажатое изображение кнопки (ДЛЯ ПЕРЕКРАСКИ)
    private readonly EditorMode edMode = EditorMode.Floor;//режим редактирования

    private void Awake()
    {
        if (editor == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): MG_Editor не прикреплен!</color>");
        if (cursor == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): MG_Cursor не прикреплен!</color>");
        //----------СПРАЙТЫ ДЛЯ КУРСОРА
        if (cursorS_Floor_2D == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для cursorS_Floor_2D не прикреплен!</color>");
        if (cursorS_FloorEraser_2D == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для cursorS_FloorEraser_2D не прикреплен!</color>");
        //----------СПРАЙТЫ ДЛЯ КУРСОРА
        if (cursorS_Floor_ISO == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для cursorS_Floor_ISO не прикреплен!</color>");
        if (cursorS_FloorEraser_ISO == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для cursorS_FloorEraser_ISO не прикреплен!</color>");
        //----------ИЗОБРАЖЕНИЕ КНОПОК
        if (sr_Floor == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для img_Floor не прикреплен!</color>");
        if (sr_Grass == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для img_Grass не прикреплен!</color>");
        if (sr_Eraser == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для img_Eraser не прикреплен!</color>");
        if (sr_WallsON == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для sr_WallsON не прикреплен!</color>");
        if (sr_WallsOFF == null)
            Debug.Log("<color=red>MGUI_FGE Awake(): объект для sr_WallsOFF не прикреплен!</color>");

        curButtonImage = sr_Floor;
        sr_WallsOFF.color = Color.grey;
    }

    //------------------------------------------------------------

    /// <summary>
    /// Переключить режим - Пол
    /// </summary>
    public void Pressed_Floor()
    {
        FloorType _Ftype = FloorType.Carpet;
        HighlightButton(_Ftype);//Изменить цвет кнопки, чтобы было видно что кнопка нажата
        ChangeCursorSprite(_Ftype);
        SendToEditor(_Ftype, edMode);//Передать необходимые настройки редактору
    }

    /// <summary>
    /// Переключить режим - Трава
    /// </summary>
    public void Pressed_Grass()
    {
        FloorType _Ftype = FloorType.Grass;
        HighlightButton(_Ftype);//Изменить цвет кнопки, чтобы было видно что кнопка нажата
        ChangeCursorSprite(_Ftype);
        SendToEditor(_Ftype, edMode);//Передать необходимые настройки редактору
    }

    /// <summary>
    /// Переключить режим - Ластик
    /// </summary>
    public void Pressed_Eraser()
    {
        FloorType _Ftype = FloorType.None;
        HighlightButton(_Ftype);//Изменить цвет кнопки, чтобы было видно что кнопка нажата
        ChangeCursorSprite(_Ftype);
        SendToEditor(_Ftype, edMode);//Передать необходимые настройки редактору
    }

    //------------------------------------------------------------

    /// <summary>
    /// Включить режим создание пола со стенами (комнаты)
    /// </summary>
    public void Pressed_WallsON()
    {
        sr_WallsON.color = Color.green;
        sr_WallsOFF.color = Color.grey;
        editor.SetRoomMode(true);

    }

    /// <summary>
    /// Выключить режим создание пола со стенами (комнаты)
    /// </summary>
    public void Pressed_WallsOFF()
    {
        sr_WallsON.color = Color.grey;
        sr_WallsOFF.color = Color.green;
        editor.SetRoomMode(false);
    }

    //------------------------------------------------------------

    /// <summary>
    /// Изменить цвет кнопки, чтобы было видно что кнопка нажата
    /// </summary>
    /// <param name="_type"></param>
    private void HighlightButton(FloorType _type)
    {
        if (curButtonImage != null)
        {
            curButtonImage.color = Color.white;
        }
        switch (_type)
        {
            case FloorType.Carpet:
                curButtonImage = sr_Floor;
                break;
            case FloorType.Grass:
                curButtonImage = sr_Grass;
                break;
            case FloorType.None:
                curButtonImage = sr_Eraser;
                break;
            default:
                Debug.Log("<color=red>MGUI_FGE HighlightButton(): вид пола не определен! </color>" + _type);
                return;
        }
        curButtonImage.color = Color.yellow;
    }

    /// <summary>
    /// Передать необходимые настройки редактору
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_mode"></param>
    private void SendToEditor(FloorType _type, EditorMode _mode)
    {
        editor.SetFloorTypeMode(_type);
        editor.SetEditorMode(_mode);
    }

    /// <summary>
    /// Поменять спрайт у курсора
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_projType"></param>
    private void ChangeCursorSprite(FloorType _type)
    {
        ProjectionType _projType = editor.ProjectionType;
        Sprite[] _spriteArray = null;
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                _spriteArray = new Sprite[2] { cursorS_Floor_2D, cursorS_FloorEraser_2D };
                break;
            case ProjectionType.Isometric2D:
                _spriteArray = new Sprite[2] { cursorS_Floor_ISO, cursorS_FloorEraser_ISO };
                break;
            default:
                Debug.Log("<color=red> MGUI_FGE ChangeCursorSprite(): нереализованный тип проекции: </color>" + _projType);
                return;
        }

        Sprite _sprite = null;
        switch (_type)
        {
            case FloorType.Carpet:
            case FloorType.Grass:
                _sprite = _spriteArray[0];
                break;
            case FloorType.None:
                _sprite = _spriteArray[1];
                break;
            default:
                Debug.Log("<color=red> MGUI_FGE ChangeCursorSprite(): нереализованный тип пола: </color>" + _type);
                return;
        }
        cursor.SetSprite(_sprite);//устанавливаем спрайт для курсора мыши
    }

    /// <summary>
    /// Выключить кнопки (перекрасить визуально серым цветом)
    /// </summary>
    public void RecolorAllButtonsGrey()
    {
        sr_Floor.color = Color.white;
        sr_Grass.color = Color.white;
        sr_Eraser.color = Color.white;
    }
}
