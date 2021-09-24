using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MGWN_EntryViewer : MGWN_PropertyViewer
{

    private Dictionary<EntryType, MG_WinI_Prop> dic_chosenProp = new Dictionary<EntryType, MG_WinI_Prop>(); //выбранные элементы каждой категории
    [SerializeField]
    private Image PreviewImageWindow;//[R] ссылка на изображение для панельки быстрого просмотра


    protected override void SendChosenToEditor(MG_WinI_Prop _winI_Prop)
    {
        //Debug.Log("<color=red>" + id + " SendChosenToEditor(): САМ МЕТОД НЕ ЗАДАН РАЗРАБОТЧИКОМ!</color>");
        MG_EntryProperty _prop = (MG_EntryProperty)_winI_Prop.GetProperty();
        editor.SetEntryProperty(_prop);//передаем выбранное свойство в редактор. Сам редактор распознаст тип
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    protected override void Init()
    {

        id = this.gameObject.name;//Получаем имя окна       
        GeneratePalette();//Генерация палитры  
        InitDicTypeItem(EntryType.Door);//Добавить первый элемент для каждого типа
        InitDicTypeItem(EntryType.Window);//Добавить первый элемент для каждого типа
        ShowItemsOfChosenType(EntryType.Window);//Вывести все элементы одного типа
    }

    /// <summary>
    /// Вывести все элементы одного типа
    /// </summary>
    /// <param name="_type"></param>
    private void ShowItemsOfChosenType(EntryType _type)
    {

        foreach (var _item in ListItems)
        {
            MG_EntryProperty _prop = (MG_EntryProperty)_item.GetProperty();
            EntryType _type2 = _prop.Type;
            if (_type.Equals(_type2))
            {
                _item.SetVisible(true);
            }
            else
            {
                _item.SetVisible(false);
            }
        }
        HighlightItemOfType(_type);//Сделать выбранный элемент из словаря по типу
    }

    /// <summary>
    /// Добавить первый элемент для каждого типа
    /// </summary>
    /// <param name="_type"></param>
    private void InitDicTypeItem(EntryType _type)
    {
        foreach (var _winI_Prop in ListItems)
        {
            MG_EntryProperty _prop = (MG_EntryProperty)_winI_Prop.GetProperty();
            EntryType _type2 = _prop.Type;
            if (_type.Equals(_type2))
            {
                dic_chosenProp.Add(_type, _winI_Prop);
                SendChosenToEditor(_winI_Prop);
                ChangeImageOfPreviewer(_winI_Prop);//Поменять иконку превьюверу для каждого типа
                break;
            }
        }
    }

    /// <summary>
    /// ДЛЯ КНОПКОК "Door"
    /// </summary>
    public void ButtonPressed_Door()
    {
        EntryType _type = EntryType.Door;
        ShowItemsOfChosenType(_type);//Вывести все элементы одного типа
    }

    /// <summary>
    /// ДЛЯ КНОПКОК "Window"
    /// </summary>
    public void ButtonPressed_Window()
    {
        EntryType _type = EntryType.Window;
        ShowItemsOfChosenType(_type);//Вывести все элементы одного типа
    }

    /// <summary>
    /// Сделать выбранный элемент из словаря по типу
    /// </summary>
    /// <param name="_type"></param>
    private void HighlightItemOfType(EntryType _type)
    {
        dic_chosenProp.TryGetValue(_type, out MG_WinI_Prop _winI_Prop);//получить айтем
        _winI_Prop.Click();//сделать его выбранным
    }

    /// <summary>
    /// Вызывается в MG_WinI_Prop классе (MG_WinI_Prop)
    /// </summary>
    /// <param name="_winI_Prop"></param>
    public override void SetChosenItem(MG_WinI_Prop _winI_Prop)
    {
        if (chosenWinC_Prop != null) chosenWinC_Prop.SetChosen(false);//делаем item не выбранный, чтобы не светился
        chosenWinC_Prop = _winI_Prop;//задаем новый текущий выбранный
        chosenWinC_Prop_id = _winI_Prop.GetId();//[D]
        SendChosenToEditor(_winI_Prop);//
        ChangeCurPropInDic(_winI_Prop);
        ChangeImageOfPreviewer(_winI_Prop);//Поменять иконку превьюверу
    }

    /// <summary>
    /// Заменить в справочнике выбранное свойство по типу
    /// </summary>
    /// <param name="_winI_Prop"></param>
    private void ChangeCurPropInDic(MG_WinI_Prop _winI_Prop)
    {
        MG_EntryProperty _property = (MG_EntryProperty)_winI_Prop.GetProperty();//получаем свойство
        EntryType _type = _property.Type;//получаем тип
        dic_chosenProp[_type] = _winI_Prop;//заменяем выбранное свойство по типу
    }



    /// <summary>
    /// Поменять иконку превьюверу
    /// </summary>
    /// <param name="_winI_Prop"></param>
    protected override void ChangeImageOfPreviewer(MG_WinI_Prop _winI_Prop)
    {
        MG_EntryProperty _property = (MG_EntryProperty) _winI_Prop.GetProperty();
        EntryType _type = _property.Type;
        if(_type.Equals(EntryType.Door))
        PreviewImage.sprite = _property.Icon;
        else if(_type.Equals(EntryType.Window))
            PreviewImageWindow.sprite = _property.Icon;
    }
}
