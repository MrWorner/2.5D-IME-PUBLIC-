using System.Collections.Generic;
using UnityEngine;

public class MGWN_FloorViewer : MGWN_PropertyViewer
{

    private Dictionary<FloorType, MG_WinI_Prop> dic_chosenProp = new Dictionary<FloorType, MG_WinI_Prop>(); //выбранные элементы каждой категории

    protected override void SendChosenToEditor(MG_WinI_Prop _winI_Prop)
    {
        //Debug.Log("<color=red>" + id + " SendChosenToEditor(): САМ МЕТОД НЕ ЗАДАН РАЗРАБОТЧИКОМ!</color>");
        MG_FloorProperty _prop = (MG_FloorProperty)_winI_Prop.GetProperty();
        editor.SetFloorProperty(_prop);//передаем выбранное свойство в редактор. Сам редактор распознаст тип пола
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    protected override void Init()
    {

        id = this.gameObject.name;//Получаем имя окна       
        GeneratePalette();//Генерация палитры  
        InitDicTypeItem(FloorType.Carpet);//Добавить первый элемент для каждого типа
        InitDicTypeItem(FloorType.Grass);//Добавить первый элемент для каждого типа
        ShowItemsOfChosenType(FloorType.Carpet);//Вывести все элементы одного типа
    }

    /// <summary>
    /// Вывести все элементы одного типа
    /// </summary>
    /// <param name="_type"></param>
    private void ShowItemsOfChosenType(FloorType _type)
    {

        foreach (var _item in ListItems)
        {
            MG_FloorProperty _prop = (MG_FloorProperty)_item.GetProperty();
            FloorType _type2 = _prop.Type;
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
    private void InitDicTypeItem(FloorType _type)
    {
        foreach (var _winI_Prop in ListItems)
        {
            MG_FloorProperty _prop = (MG_FloorProperty)_winI_Prop.GetProperty();
            FloorType _type2 = _prop.Type;
            if (_type.Equals(_type2))
            {
                dic_chosenProp.Add(_type, _winI_Prop);
                SendChosenToEditor(_winI_Prop);
                break;
            }
        }
    }

    /// <summary>
    /// Сделать выбранный элемент из словаря по типу
    /// </summary>
    /// <param name="_type"></param>
    private void HighlightItemOfType(FloorType _type)
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
        MG_FloorProperty _property = (MG_FloorProperty)_winI_Prop.GetProperty();//получаем свойство
        FloorType _type = _property.Type;//получаем тип
        dic_chosenProp[_type] = _winI_Prop;//заменяем выбранное свойство по типу
    }

    /// <summary>
    /// ДЛЯ КНОПКИ "FLOOR"
    /// </summary>
    public void ButtonPressed_Carpet()
    {
        FloorType _type = FloorType.Carpet;
        ShowItemsOfChosenType(_type);//Вывести все элементы одного типа
    }

    /// <summary>
    /// ДЛЯ КНОПКИ "GRASS"
    /// </summary>
    public void ButtonPressed_Grass()
    {
        FloorType _type = FloorType.Grass;
        ShowItemsOfChosenType(_type);//Вывести все элементы одного типа
    }
}
