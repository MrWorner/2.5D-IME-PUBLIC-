using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGWN_ProplViewer : MGWN_PropertyViewer
{
    private Dictionary<PropSize, MG_WinI_Prop> dic_chosenProp = new Dictionary<PropSize, MG_WinI_Prop>(); //выбранные элементы каждой категории

    protected override void SendChosenToEditor(MG_WinI_Prop _winI_Prop)
    {
        //Debug.Log("<color=red>" + id + " SendChosenToEditor(): САМ МЕТОД НЕ ЗАДАН РАЗРАБОТЧИКОМ!</color>");
        MG_PropProperty _prop = (MG_PropProperty)_winI_Prop.GetProperty();
        editor.SetPropProperty(_prop);//передаем выбранное свойство в редактор. Сам редактор распознаст тип пола
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    protected override void Init()
    {
        id = this.gameObject.name;//Получаем имя окна       
        GeneratePalette();//Генерация палитры  
        MG_WinI_Prop _winI_Prop = ListItems[0];
        _winI_Prop.Click();
        //------InitDicTypeItem(WallType.SmallFence);//Добавить первый элемент для каждого типа
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
        ChangeImageOfPreviewer(_winI_Prop);//Поменять иконку превьюверу
        //---------ChangeCurPropInDic(_winI_Prop);
    }

    /// <summary>
    /// Заменить в справочнике выбранное свойство по типу
    /// </summary>
    /// <param name="_winI_Prop"></param>
    //private void ChangeCurPropInDic(MG_WinI_Prop _winI_Prop)
    //{
    //    MG_WallProperty _property = (MG_WallProperty)_winI_Prop.GetProperty();//получаем свойство
    //    WallType _type = _property.Type;//получаем тип
    //    dic_chosenProp[_type] = _winI_Prop;//заменяем выбранное свойство по типу
    //}

    /// <summary>
    /// Вывести все элементы одного типа
    /// </summary>
    /// <param name="_type"></param>
    //private void ShowItemsOfChosenType(WallType _type)
    //{

    //    foreach (var _item in ListItems)
    //    {
    //        MG_WallProperty _prop = (MG_WallProperty)_item.GetProperty();
    //        WallType _type2 = _prop.Type;
    //        if (_type.Equals(_type2))
    //        {
    //            _item.SetVisible(true);
    //        }
    //        else
    //        {
    //            _item.SetVisible(false);
    //        }
    //    }
    //    HighlightItemOfType(_type);//Сделать выбранный элемент из словаря по типу
    //}

    /// <summary>
    /// Сделать выбранный элемент из словаря по типу
    /// </summary>
    /// <param name="_type"></param>
    //private void HighlightItemOfType(WallType _type)
    //{
    //    dic_chosenProp.TryGetValue(_type, out MG_WinI_Prop _winI_Prop);//получить айтем
    //    _winI_Prop.Click();//сделать его выбранным
    //}

    /// <summary>
    /// Добавить первый элемент для каждого типа
    /// </summary>
    /// <param name="_type"></param>
    //private void InitDicTypeItem(WallType _type)
    //{
    //    foreach (var _winI_Prop in ListItems)
    //    {
    //        MG_WallProperty _prop = (MG_WallProperty)_winI_Prop.GetProperty();
    //        WallType _type2 = _prop.Type;
    //        if (_type.Equals(_type2))
    //        {
    //            dic_chosenProp.Add(_type, _winI_Prop);
    //            SendChosenToEditor(_winI_Prop);
    //            break;
    //        }
    //    }
    //}

    /// <summary>
    /// ДЛЯ БУДУЩЕЙ КНОПКИ "SmallFence"
    /// </summary>
    //public void ButtonPressed_SmallFence()
    //{
    //    WallType _type = WallType.SmallFence;
    //    ShowItemsOfChosenType(_type);//Вывести все элементы одного типа
    //}
}
