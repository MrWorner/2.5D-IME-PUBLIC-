using System;
using System.Collections.Generic;
using UnityEngine;

public class MG_WallXML_data : MG_TileXML_data
{

    [SerializeField]
    private List<Sprite> XMLSpriteIsoHList;//Спрайт Isometric H будущего свойства 
    [SerializeField]
    private List<Sprite> XMLSpriteIsoVList;//Спрайт Isometric V будущего свойства
    [SerializeField]
    private List<Sprite> XMLSprite2DHList;//Спрайт 2D Top Down H будущего свойства
    [SerializeField]
    private List<Sprite> XMLSprite2DVList;//Спрайт 2D Top Down V будущего свойства
    [SerializeField]
    private List<WallType> XMLWallTypeList;//Тип пола будущего свойства


    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D Horizontal
    [SerializeField]
    private List<float> XMLPivotX_2DH;
    [SerializeField]
    private List<float> XMLPivotY_2DH;
    [SerializeField]
    private List<float> XMLPixelPerU_2DH;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D Vertical
    [SerializeField]
    private List<float> XMLPivotX_2DV;
    [SerializeField]
    private List<float> XMLPivotY_2DV;
    [SerializeField]
    private List<float> XMLPixelPerU_2DV;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D ISOMETRIC Horizontal
    [SerializeField]
    private List<float> XMLPivotX_IsoH;
    [SerializeField]
    private List<float> XMLPivotY_IsoH;
    [SerializeField]
    private List<float> XMLPixelPerU_IsoH;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D ISOMETRIC Vertical
    [SerializeField]
    private List<float> XMLPivotX_IsoV;
    [SerializeField]
    private List<float> XMLPivotY_IsoV;
    [SerializeField]
    private List<float> XMLPixelPerU_IsoV;

    /// <summary>
    /// получить лист со спрайтами Horizontal
    /// </summary>
    /// <param name="_projType"></param>
    /// <returns></returns>
    public List<Sprite> GetSpriteListH(ProjectionType _projType)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                return XMLSprite2DHList;
            case ProjectionType.Isometric2D:
                return XMLSpriteIsoHList;
            default:
                Debug.Log("<color=red>MG_WallXML_data GetSpriteListH(): неверный ! ProjectionType = </color>" + _projType);
                return null;
        }
    }

    /// <summary>
    /// получить лист со спрайтами Vertical
    /// </summary>
    /// <param name="_projType"></param>
    /// <returns></returns>
    public List<Sprite> GetSpriteListV(ProjectionType _projType)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                return XMLSprite2DVList;
            case ProjectionType.Isometric2D:
                return XMLSpriteIsoVList;
            default:
                Debug.Log("<color=red>MG_WallXML_data GetSpriteListV(): неверный ! ProjectionType = </color>" + _projType);
                return null;
        }
    }

    /// <summary>
    /// Добавить спрайты
    /// </summary>
    /// <param name="_sprite2DH"></param>
    /// <param name="_sprite2DV"></param>
    /// <param name="_spriteIsoH"></param>
    /// <param name="_spriteIsoV"></param>
    public void AddSprite(Sprite _sprite2DH, Sprite _sprite2DV, Sprite _spriteIsoH, Sprite _spriteIsoV)
    {
        XMLSprite2DHList.Add(_sprite2DH);
        XMLSprite2DVList.Add(_sprite2DV);
        XMLSpriteIsoHList.Add(_spriteIsoH);
        XMLSpriteIsoVList.Add(_spriteIsoV);
    }

    //-----------------------------------------

    /// <summary>
    /// Получить лист с типами стен
    /// </summary>
    /// <returns></returns>
    public List<WallType> GetWallTypeList()
    {
        return XMLWallTypeList;
    }

    /// <summary>
    /// Добавить тип стены в список
    /// </summary>
    /// <param name="_wallType"></param>
    public void AddWallType(WallType _wallType)
    {
        XMLWallTypeList.Add(_wallType);
    }

    //-------------------------

    /// <summary>
    /// Получить список Пивотов Horizontal
    /// </summary>
    /// <returns></returns>
    public List<float> GetPivotListH(ProjectionType _projType, MathDirection _mathDir)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                if (_mathDir.Equals(MathDirection.X))
                    return XMLPivotX_2DH;
                else if (_mathDir.Equals(MathDirection.Y))
                    return XMLPivotY_2DH;
                else
                    Debug.Log("<color=red>MG_WallXML_data GetPivotListH(): неверный MathDirection! _lineDir = </color>" + _mathDir);
                return null;
            case ProjectionType.Isometric2D:
                if (_mathDir.Equals(MathDirection.X))
                    return XMLPivotX_IsoH;
                else if (_mathDir.Equals(MathDirection.Y))
                    return XMLPivotY_IsoH;
                else
                    Debug.Log("<color=red>MG_WallXML_data GetPivotListH(): неверный MathDirection! _lineDir = </color>" + _mathDir);
                return null;
            default:
                Debug.Log("<color=red>MG_WallXML_data GetPivotList(): ProjectionType не освоен! _projType = </color>" + _projType);
                return null;
        }
    }


    /// <summary>
    /// Получить список Пивотов Vertical
    /// </summary>
    /// <returns></returns>
    public List<float> GetPivotListV(ProjectionType _projType, MathDirection _mathDir)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                if (_mathDir.Equals(MathDirection.X))
                    return XMLPivotX_2DV;
                else if (_mathDir.Equals(MathDirection.Y))
                    return XMLPivotY_2DV;
                else
                    Debug.Log("<color=red>MG_WallXML_data GetPivotListV(): неверный MathDirection! _lineDir = </color>" + _mathDir);
                return null;
            case ProjectionType.Isometric2D:
                if (_mathDir.Equals(MathDirection.X))
                    return XMLPivotX_IsoV;
                else if (_mathDir.Equals(MathDirection.Y))
                    return XMLPivotY_IsoV;
                else
                    Debug.Log("<color=red>MG_WallXML_data GetPivotListV(): неверный MathDirection! _lineDir = </color>" + _mathDir);
                return null;
            default:
                Debug.Log("<color=red>MG_WallXML_data GetPivotList(): ProjectionType не освоен! _projType = </color>" + _projType);
                return null;
        }
    }

    /// <summary>
    /// Добавить Пивот Horizontal
    /// </summary>
    /// <param name="_pivotX2D"></param>
    /// <param name="_pivotY2D"></param>
    /// <param name="_pivotXISO"></param>
    /// <param name="_pivotYISO"></param>
    public void AddPivotH(float _pivotX2D, float _pivotY2D, float _pivotXISO, float _pivotYISO)
    {
        XMLPivotX_2DH.Add(_pivotX2D);
        XMLPivotY_2DH.Add(_pivotY2D);
        XMLPivotX_IsoH.Add(_pivotXISO);
        XMLPivotY_IsoH.Add(_pivotYISO);
    }

    /// <summary>
    /// Добавить Пивот Vertical
    /// </summary>
    /// <param name="_pivotX2D"></param>
    /// <param name="_pivotY2D"></param>
    /// <param name="_pivotXISO"></param>
    /// <param name="_pivotYISO"></param>
    public void AddPivotV(float _pivotX2D, float _pivotY2D, float _pivotXISO, float _pivotYISO)
    {
        XMLPivotX_2DV.Add(_pivotX2D);
        XMLPivotY_2DV.Add(_pivotY2D);
        XMLPivotX_IsoV.Add(_pivotXISO);
        XMLPivotY_IsoV.Add(_pivotYISO);
    }

    //--------------------------------------------

    /// <summary>
    /// Получить PixelPerU Horizontal
    /// </summary>
    /// <param name="_projType"></param>
    /// <returns></returns>
    public List<float> GetPixelPerU_H(ProjectionType _projType)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                return XMLPixelPerU_2DH;
            case ProjectionType.Isometric2D:
                return XMLPixelPerU_IsoH;
            default:
                Debug.Log("<color=red>MG_FloorXML_data GetPixelPerU_H(): ProjectionType не освоен! _projType = </color>" + _projType);
                return null;
        }
    }
    /// <summary>
    /// Получить PixelPerU Vertical
    /// </summary>
    /// <param name="_projType"></param>
    /// <returns></returns>
    public List<float> GetPixelPerU_V(ProjectionType _projType)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                return XMLPixelPerU_2DV;
            case ProjectionType.Isometric2D:
                return XMLPixelPerU_IsoV;
            default:
                Debug.Log("<color=red>MG_FloorXML_data GetPixelPerU_V(): ProjectionType не освоен! _projType = </color>" + _projType);
                return null;
        }
    }

    /// <summary>
    /// Добавить PixelPerU
    /// </summary>
    /// <param name="_pixelPerU2DH"></param>
    /// <param name="_pixelPerU2DV"></param>
    /// <param name="_pixelPerUISOH"></param>
    /// <param name="_pixelPerUISOV"></param>
    public void AddPixelPerU(float _pixelPerU2DH, float _pixelPerU2DV, float _pixelPerUISOH, float _pixelPerUISOV)
    {
        XMLPixelPerU_2DH.Add(_pixelPerU2DH);
        XMLPixelPerU_2DV.Add(_pixelPerU2DV);
        XMLPixelPerU_IsoH.Add(_pixelPerUISOH);
        XMLPixelPerU_IsoV.Add(_pixelPerUISOV);
    }


}
