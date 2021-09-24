using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MGUI_Tool : MonoBehaviour, iDimension
{
    [SerializeField]
    private string id = "{Не задан} MGUI_Tool";
    [SerializeField]
    private EditorMode edMode = EditorMode.Walls;
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора
    [SerializeField]
    private MG_Cursor cursor;//[R] курсор
    [SerializeField]
    private MGUI_GeneralTools UI_generalTools;//класс главной панели MGUI_GeneralTools

    //----СПРАЙТЫ ДЛЯ КУРСОРА МЫШИ 2D TOP DOWN
    [SerializeField]
    private Sprite s_N_2D;//[R] s - Спрайт, N - Север, 2D - для TOP DOWN 2D
    [SerializeField]
    private Sprite s_E_2D;//[R]
    [SerializeField]
    private Sprite s_S_2D;//[R]
    [SerializeField]
    private Sprite s_W_2D;//[R]
    [SerializeField]
    private Sprite s_Eraser_2D;//[R]
    //----СПРАЙТЫ ДЛЯ КУРСОРА МЫШИ 2D ISOMETRIC
    [SerializeField]
    private Sprite s_N_ISO;//[R] s - Спрайт, N - Север, ISO - для Isometric 2D
    [SerializeField]
    private Sprite s_E_ISO;//[R]
    [SerializeField]
    private Sprite s_S_ISO;//[R]
    [SerializeField]
    private Sprite s_W_ISO;//[R]
    [SerializeField]
    private Sprite s_Eraser_ISO;//[R]
    //----ИКОНКИ ДЛЯ КНОПОК 2D TOP DOWN
    [SerializeField]
    private Sprite img_N_2D;//[R] img - Изображение, N - Север, 2D - для TOP DOWN 2D
    [SerializeField]
    private Sprite img_E_2D;//[R]
    [SerializeField]
    private Sprite img_S_2D;//[R]
    [SerializeField]
    private Sprite img_W_2D;//[R]
    //[SerializeField]
    //private Sprite img_Eraser_2D;//[R]
    //----ИКОНКИ ДЛЯ КНОПОК 2D ISOMETRIC
    [SerializeField]
    private Sprite img_N_ISO;//[R] img - Изображение, N - Север,  ISO - для Isometric 2D
    [SerializeField]
    private Sprite img_E_ISO;//[R]
    [SerializeField]
    private Sprite img_S_ISO;//[R]
    [SerializeField]
    private Sprite img_W_ISO;//[R]
    //[SerializeField]
    //private Sprite img_Eraser_ISO;//[R]
    //----Спрайт изображение кнопок на панели
    [SerializeField]
    private Image sr_N;//[R] sr - Спрайт изображение,  N - Север
    [SerializeField]
    private Image sr_E;//[R]
    [SerializeField]
    private Image sr_S;//[R]
    [SerializeField]
    private Image sr_W;//[R]
    [SerializeField]
    private Image sr_Erase;//[R]

    private Image[] srArray;//массив всех кнопок

    //[SerializeField]
    private Image curSpriteImg;//рендер нажатой кнопки
    private bool isDisabledVisually = true;//выключен ли визуально (перекрашен в красный)

    private void Awake()
    {
        if (editor == null)
            Debug.Log("<color=red>" + id + " Awake(): MG_Editor не прикреплен!</color>");
        if (cursor == null)
            Debug.Log("<color=red>" + id + " Awake(): MG_Cursor не прикреплен!</color>");
        if (UI_generalTools == null)
            Debug.Log("<color=red>" + id + " Awake(): MGUI_GeneralTools не прикреплен!</color>");
        //----СПРАЙТЫ ДЛЯ КУРСОРА МЫШИ 2D TOP DOWN
        if (s_N_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_N_2D не прикреплен!</color>");
        if (s_E_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_E_2D не прикреплен!</color>");
        if (s_S_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_S_2D не прикреплен!</color>");
        if (s_W_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_W_2D не прикреплен!</color>");
        if (s_Eraser_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_Eraser_2D не прикреплен!</color>");
        //----СПРАЙТЫ ДЛЯ КУРСОРА МЫШИ 2D ISOMETRIC
        if (s_N_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_N_ISO не прикреплен!</color>");
        if (s_E_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_E_ISO не прикреплен!</color>");
        if (s_S_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_S_ISO не прикреплен!</color>");
        if (s_W_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_W_ISO не прикреплен!</color>");
        if (s_Eraser_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для s_Eraser_ISO не прикреплен!</color>");
        //----ИКОНКИ ДЛЯ КНОПОК 2D TOP DOWN
        if (img_N_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для img_N_2D не прикреплен!</color>");
        if (img_E_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для img_E_2D не прикреплен!</color>");
        if (img_S_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для img_S_2D не прикреплен!</color>");
        if (img_W_2D == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для img_W_2D не прикреплен!</color>");
        //if (img_Eraser_2D == null)
        //   Debug.Log("<color=red>" + id + " Awake(): объект для img_Eraser_2D не прикреплен!</color>");
        //----ИКОНКИ ДЛЯ КНОПОК 2D ISOMETRIC
        if (img_N_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для img_N_ISO не прикреплен!</color>");
        if (img_E_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для img_E_ISO не прикреплен!</color>");
        if (img_S_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для img_S_ISO не прикреплен!</color>");
        if (img_W_ISO == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для img_W_ISO не прикреплен!</color>");
        //if (img_Eraser_ISO == null)
        //   Debug.Log("<color=red>" + id + " Awake(): объект для img_Eraser_ISO не прикреплен!</color>");
        //----Спрайт изображение кнопок на панели
        if (sr_N == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для sr_N не прикреплен!</color>");
        if (sr_E == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для sr_E не прикреплен!</color>");
        if (sr_S == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для sr_S не прикреплен!</color>");
        if (sr_W == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для sr_W не прикреплен!</color>");
        if (sr_Erase == null)
            Debug.Log("<color=red>" + id + " Awake(): объект для sr_Erase не прикреплен!</color>");

        srArray = new Image[5] { sr_N, sr_E, sr_S, sr_W, sr_Erase };//все рендереры кнопок
    }

    public void Pressed_N()
    {
        Direction _dir = Direction.North;//Направление объекта

        ProjectionType _projType = editor.ProjectionType;//взять тип проекции
        ChangeCursorSprite(_dir, _projType);//Поменять спрайт у курсора
        UI_generalTools.RecolorButtons(this);//Отправить главной панели о том что была нажата кнопка на данной панели (ДЛЯ ПЕРЕКРАСКИ КНОПОК!)
        HighlightButton(_dir);//Визуально показать нажатую кнопку
        SendToEditor(_dir, edMode);//Передать необходимые настройки редактору
    }

    public void Pressed_E()
    {
        Direction _dir = Direction.East;//Направление объекта

        ProjectionType _projType = editor.ProjectionType;//взять тип проекции
        ChangeCursorSprite(_dir, _projType);//Поменять спрайт у курсора
        UI_generalTools.RecolorButtons(this);//Отправить главной панели о том что была нажата кнопка на данной панели (ДЛЯ ПЕРЕКРАСКИ КНОПОК!)
        HighlightButton(_dir);//Визуально показать нажатую кнопку
        SendToEditor(_dir, edMode);//Передать необходимые настройки редактору
    }

    public void Pressed_S()
    {
        Direction _dir = Direction.South;//Направление объекта

        ProjectionType _projType = editor.ProjectionType;//взять тип проекции
        ChangeCursorSprite(_dir, _projType);//Поменять спрайт у курсора
        UI_generalTools.RecolorButtons(this);//Отправить главной панели о том что была нажата кнопка на данной панели (ДЛЯ ПЕРЕКРАСКИ КНОПОК!)
        HighlightButton(_dir);//Визуально показать нажатую кнопку
        SendToEditor(_dir, edMode);//Передать необходимые настройки редактору
    }

    public void Pressed_W()
    {
        Direction _dir = Direction.West;//Направление объекта

        ProjectionType _projType = editor.ProjectionType;//взять тип проекции
        ChangeCursorSprite(_dir, _projType);//Поменять спрайт у курсора
        UI_generalTools.RecolorButtons(this);//Отправить главной панели о том что была нажата кнопка на данной панели (ДЛЯ ПЕРЕКРАСКИ КНОПОК!)
        HighlightButton(_dir);//Визуально показать нажатую кнопку
        SendToEditor(_dir, edMode);//Передать необходимые настройки редактору
    }

    public void Pressed_Eraser()
    {
        Direction _dir = Direction.None;//Направление объекта

        ProjectionType _projType = editor.ProjectionType;//взять тип проекции
        ChangeCursorSprite(_dir, _projType);//Поменять спрайт у курсора
        UI_generalTools.RecolorButtons(this);//Отправить главной панели о том что была нажата кнопка на данной панели (ДЛЯ ПЕРЕКРАСКИ КНОПОК!)
        HighlightButton(_dir);//Визуально показать нажатую кнопку
        SendToEditor(_dir, edMode);//Передать необходимые настройки редактору
    }

    /// <summary>
    /// Передать необходимые настройки редактору
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_mode"></param>
    private void SendToEditor(Direction _dir, EditorMode _mode)
    {
        editor.SetDirection(_dir);
        editor.SetEditorMode(_mode);
    }

    /// <summary>
    /// Поменять спрайт у курсора
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_projType"></param>
    private void ChangeCursorSprite(Direction _dir, ProjectionType _projType)
    {
        Sprite[] _spriteArray;
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                _spriteArray = new Sprite[5] { s_N_2D, s_E_2D, s_S_2D, s_W_2D, s_Eraser_2D };
                break;
            case ProjectionType.Isometric2D:
                _spriteArray = new Sprite[5] { s_N_ISO, s_E_ISO, s_S_ISO, s_W_ISO, s_Eraser_ISO };
                break;
            default:
                Debug.Log("<color=red>" + id + " ChangeCursorSprite(): нереализованный тип проекции: </color>" + _projType);
                return;
        }

        Sprite _sprite = null;
        switch (_dir)
        {
            case Direction.North:
                _sprite = _spriteArray[0];//Север
                break;
            case Direction.East:
                _sprite = _spriteArray[1];//Восток
                break;
            case Direction.South:
                _sprite = _spriteArray[2];//Юг
                break;
            case Direction.West:
                _sprite = _spriteArray[3];//Запад
                break;
            case Direction.None:
                _sprite = _spriteArray[4];//Нет направления (ластик)
                break;
            default:
                Debug.Log("<color=red>" + id + " ChangeCursorSprite(): нереализованный тип направления: </color>" + _dir);
                break;
        }
        cursor.SetSprite(_sprite);//устанавливаем спрайт для курсора мыши
    }

    /// <summary>
    /// Визуально показать нажатую кнопку
    /// </summary>
    /// <param name="_dir"></param>
    private void HighlightButton(Direction _dir)
    {
        if (curSpriteImg != null)
        {
            curSpriteImg.color = Color.white;
        }

        switch (_dir)
        {
            case Direction.North:
                curSpriteImg = sr_N;
                break;
            case Direction.East:
                curSpriteImg = sr_E;
                break;
            case Direction.South:
                curSpriteImg = sr_S;
                break;
            case Direction.West:
                curSpriteImg = sr_W;
                break;
            case Direction.None:
                curSpriteImg = sr_Erase;
                break;
            default:
                Debug.Log("<color=red>" + id + " HighlightButton(): нереализованный тип направления: </color>" + _dir);
                break;
        }
        curSpriteImg.color = Color.yellow;
    }

    /// <summary>
    /// Перекрасить блок кнопок на красный цвет
    /// </summary>
    public void DisableVisuallyButtons()
    {
        if (!isDisabledVisually)//если не перекрашены кнопки в красный
        {           
            foreach (var _sr in srArray)
            {
                _sr.color = Color.red;//перекрашиваем в красный
            }
            isDisabledVisually = true;
            curSpriteImg = null;//сбрасываем выбранный рендер кнопки
        }
       
    }

    /// <summary>
    /// Перекрасить блок кнопок на белый цвет
    /// </summary>
    public void EnableVisuallyButtons()
    {
        if (isDisabledVisually)//если перекрашены кнопки в красный
        {
            EditorMode _edModeEditor = editor.GetEditorMode();//получаем вид редактирования
            if (!_edModeEditor.Equals(edMode))//проверяем что редактор имеет другой режим редактирования. (если не проверять, то каждый раз будет перекрашивать уже перекрашенные после нажатия)
            {
                foreach (var _sr in srArray)
                {
                    _sr.color = Color.white;//перекрашиваем в белый
                }
            }
            isDisabledVisually = false;
        }
    }

    //--------------------------iDimension ИНТЕРФЕЙС
    /// <summary>
    /// Переключен режим на 2D Top Down. Меняем иконки на интерфейсе.
    /// </summary>
    public void On2DTopDown()
    {
        sr_N.sprite = img_N_2D;
        sr_E.sprite = img_E_2D;
        sr_S.sprite = img_S_2D;
        sr_W.sprite = img_W_2D;
    }

    /// <summary>
    /// Переключен режим на 2D Isometric. Меняем иконки на интерфейсе.
    /// </summary>
    public void On2DIsometric()
    {
        sr_N.sprite = img_N_ISO;
        sr_E.sprite = img_E_ISO;
        sr_S.sprite = img_S_ISO;
        sr_W.sprite = img_W_ISO;
    }
}