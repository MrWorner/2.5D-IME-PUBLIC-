using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MGWN_PropertyViewer : MonoBehaviour
{

    //private Dictionary<string, MG_WinI_Prop> dic_items = new Dictionary<string, MG_WinI_Prop>(); //все созданные MG_WinC_Prop
    protected List<MG_WinI_Prop> ListItems = new List<MG_WinI_Prop>();//Все сгенерированные компоненты

    [SerializeField]
    protected string id = "{Property} viewer";//Id окна (название окна)

    [SerializeField]
    protected GameObject mainWindow;//[R] Главное окно
    [SerializeField]
    protected GameObject panelForTiles;//[R] Панель для тайлов
    [SerializeField]
    protected GameObject winC_PropObj;//[R] шаблон компонента свойства для таблицы
    [SerializeField]
    protected MG_PropertyLibrary library;//[R] библиотека свойств
    [SerializeField]
    protected MG_Editor editor;//[R] редактор, куда будем скидывать выбранные свойства
    [SerializeField]
    protected Image PreviewImage;//[R] ссылка на изображение для панельки быстрого просмотра

    protected MG_WinI_Prop chosenWinC_Prop;// выбранный тайл

    [SerializeField]
    protected string chosenWinC_Prop_id;//[D]  выбранный тайл (Его Айди)
    [SerializeField]
    protected int countCompProps = 0;//[D] Общее кол-во MG_WinC_Prop

    protected void Start()
    {
        CheckAttrs();//проверяем все атрибуты
        Init();//Инициализация
        SetVisible(false);//убираем видимость
    }

    /// <summary>
    /// Проверка данных
    /// </summary>
    private void CheckAttrs()
    {
        if (winC_PropObj == null)
            Debug.Log("<color=red>" + id + " Init(): объект для winC_Prop не прикреплен!</color>");
        if (library == null)
            Debug.Log("<color=red>" + id + " Init(): MG_PropertyLibrary для library не прикреплен!</color>");
        if (mainWindow == null)
            Debug.Log("<color=red>" + id + " Init(): объект для mainWindow не прикреплен!</color>");
        if (PreviewImage == null)
            Debug.Log("<color=red>" + id + " Init(): объект для PreviewImage не прикреплен!</color>");
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    /// 
    protected virtual void Init()
    {
        id = this.gameObject.name;//Получаем имя окна       
        GeneratePalette();//Генерация палитры       
    }

    /// <summary>
    /// Открыть окно
    /// </summary>
    /// <param name="_isVisible"></param>
    public void SetVisible(bool _isVisible)
    {
        mainWindow.SetActive(_isVisible);
    }

    /// <summary>
    /// Генерация палитры
    /// </summary>
    protected virtual void GeneratePalette()
    {

        foreach (MG_Property _propTile in library.getListProp())
        {
            countCompProps++;
            string _id = _propTile.ID;//Имя будущего объекта

            GameObject _winC_PropObj = Instantiate(winC_PropObj);//создаем объект
            _winC_PropObj.transform.SetParent(panelForTiles.transform, false); //Задаем Parent

            MG_WinI_Prop _winC_Prop = _winC_PropObj.transform.GetComponentInChildren<MG_WinI_Prop>();//получаем компонент
            _winC_Prop.Init(_propTile, _propTile.Icon, _id, this);//Передаем все атрибуты

            ListItems.Add(_winC_Prop);//Добавить в список
        }
    }

    /// <summary>
    /// Вызывается в MG_WinI_Prop классе (MG_WinI_Prop)
    /// </summary>
    /// <param name="_winI_Prop"></param>
    public virtual void SetChosenItem(MG_WinI_Prop _winI_Prop)
    {     
        if (chosenWinC_Prop != null) chosenWinC_Prop.SetChosen(false);//делаем item не выбранный, чтобы не светился
        chosenWinC_Prop = _winI_Prop;//задаем новый текущий выбранный
        chosenWinC_Prop_id = _winI_Prop.GetId();//[D]
        SendChosenToEditor(_winI_Prop);//Передать выбранный элемент Редактору
        ChangeImageOfPreviewer(_winI_Prop);//Поменять иконку превьюверу
    }


    /// <summary>
    /// Передать выбранный элемент Редактору
    /// </summary>
    /// <param name="_winI_Prop"></param>
    protected virtual void SendChosenToEditor(MG_WinI_Prop _winI_Prop)
    {
        Debug.Log("<color=red>" + id + " SendChosenToEditor(): САМ МЕТОД НЕ ЗАДАН РАЗРАБОТЧИКОМ!</color>");
    }

    /// <summary>
    /// Поменять иконку превьюверу
    /// </summary>
    /// <param name="_winI_Prop"></param>
    protected virtual void ChangeImageOfPreviewer(MG_WinI_Prop _winI_Prop)
    {
        MG_Property _property = _winI_Prop.GetProperty();
        PreviewImage.sprite = _property.Icon;
    }
 
}
