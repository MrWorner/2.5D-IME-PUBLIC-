using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;

public class MG_FloorXML_data : MG_TileXML_data
{

    [SerializeField]
    private List<Sprite> XMLSpriteIsoList;//Спрайт Isometric будущего свойства
    [SerializeField]
    private List<Sprite> XMLSprite2DList;//Спрайт 2D Top Down будущего свойства
    [SerializeField]
    private List<FloorType> XMLFloorTypeList;//Тип пола будущего свойства

    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D 
    [SerializeField]
    private List<float> XMLPivotX_2D;
    [SerializeField]
    private List<float> XMLPivotY_2D;
    [SerializeField]
    private List<float> XMLPixelPerU_2D;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D ISOMETRIC 
    [SerializeField]
    private List<float> XMLPivotX_Iso;
    [SerializeField]
    private List<float> XMLPivotY_Iso;
    [SerializeField]
    private List<float> XMLPixelPerU_Iso;


    /// <summary>
    /// получить лист со спрайтами
    /// </summary>
    /// <param name="_projType"></param>
    /// <returns></returns>
    public List<Sprite> GetSpriteList(ProjectionType _projType)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                return XMLSprite2DList;
            case ProjectionType.Isometric2D:
                return XMLSpriteIsoList;
            default:
                Debug.Log("<color=red>MG_FloorXML_data GetSpriteList(): неверный ! ProjectionType = </color>" + _projType);
                return null;
        }
    }

    /// <summary>
    /// Добавить спрайт
    /// </summary>
    /// <param name="_sprite2D"></param>
    /// <param name="_spriteIso"></param>
    public void AddSprite(Sprite _sprite2D, Sprite _spriteIso)
    {
        XMLSprite2DList.Add(_sprite2D);
        XMLSpriteIsoList.Add(_spriteIso);
    }

    //-----------------------------------------------

    /// <summary>
    /// Получить лист с типами пола
    /// </summary>
    /// <returns></returns>
    public List<FloorType> GetFloorTypeList()
    {
        return XMLFloorTypeList;
    }

    /// <summary>
    /// Добавить тип пола в список
    /// </summary>
    /// <param name="_floorType"></param>
    public void AddFloorType(FloorType _floorType)
    {
        XMLFloorTypeList.Add(_floorType);
    }

    //----------------------------------------


    /// <summary>
    /// Получить список Пивотов
    /// </summary>
    /// <param name="_projType"></param>
    /// <param name="_mathDir"></param>
    /// <returns></returns>
    public List<float> GetPivotList(ProjectionType _projType, MathDirection _mathDir)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                if (_mathDir.Equals(MathDirection.X))
                    return XMLPivotX_2D;
                else if (_mathDir.Equals(MathDirection.Y))
                    return XMLPivotY_2D;
                else
                    Debug.Log("<color=red>MG_FloorXML_data GetPivotList(): неверный MathDirection! _mathDir = </color>" + _mathDir);
                return null;
            case ProjectionType.Isometric2D:
                if (_mathDir.Equals(MathDirection.X))
                    return XMLPivotX_Iso;
                else if (_mathDir.Equals(MathDirection.Y))
                    return XMLPivotY_Iso;
                else
                    Debug.Log("<color=red>MG_FloorXML_data GetPivotList(): неверный MathDirection! _mathDir = </color>" + _mathDir);
                return null;
            default:
                Debug.Log("<color=red>MG_FloorXML_data GetPivotList(): ProjectionType не освоен! _projType = </color>" + _projType);
                return null;
        }
    }

    /// <summary>
    /// Добавить Пивот
    /// </summary>
    /// <param name="_pivotX2D"></param>
    /// <param name="_pivotY2D"></param>
    /// <param name="_pivotXISO"></param>
    /// <param name="_pivotYISO"></param>
    public void AddPivot(float _pivotX2D, float _pivotY2D, float _pivotXISO, float _pivotYISO)
    {
        XMLPivotX_2D.Add(_pivotX2D);
        XMLPivotY_2D.Add(_pivotY2D);
        XMLPivotX_Iso.Add(_pivotXISO);
        XMLPivotY_Iso.Add(_pivotYISO);
    }

    //---------------------------------------

    /// <summary>
    /// Добавить PixelPerU
    /// </summary>
    /// <param name="_pixelPerU2D"></param>
    /// <param name="_pixelPerUISO"></param>
    public void AddPixelPerU(float _pixelPerU2D, float _pixelPerUISO)
    {
        XMLPixelPerU_2D.Add(_pixelPerU2D);
        XMLPixelPerU_Iso.Add(_pixelPerUISO);
    }

    /// <summary>
    /// Получить PixelPerU
    /// </summary>
    /// <param name="_projType"></param>
    /// <returns></returns>
    public List<float> GetPixelPerU(ProjectionType _projType)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                return XMLPixelPerU_2D;
            case ProjectionType.Isometric2D:
                return XMLPixelPerU_Iso;
            default:
                Debug.Log("<color=red>MG_FloorXML_data GetPixelPerU(): ProjectionType не освоен! _projType = </color>" + _projType);
                return null;
        }
    }
}
