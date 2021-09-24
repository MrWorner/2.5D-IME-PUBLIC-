using System;
using System.Collections.Generic;
using UnityEngine;

public class MG_PropXML_data : MG_TileXML_data
{
    [SerializeField]
    private List<Sprite> XMLSpriteIsoList_N;//Спрайт Isometric будущего свойства
    [SerializeField]
    private List<Sprite> XMLSpriteIsoList_E;//Спрайт Isometric будущего свойства
    [SerializeField]
    private List<Sprite> XMLSpriteIsoList_S;//Спрайт Isometric будущего свойства
    [SerializeField]
    private List<Sprite> XMLSpriteIsoList_W;//Спрайт Isometric будущего свойства
    [SerializeField]
    private List<Sprite> XMLSprite2DList_N;//Спрайт 2D Top Down будущего свойства
    [SerializeField]
    private List<Sprite> XMLSprite2DList_E;//Спрайт 2D Top Down будущего свойства
    [SerializeField]
    private List<Sprite> XMLSprite2DList_S;//Спрайт 2D Top Down будущего свойства
    [SerializeField]
    private List<Sprite> XMLSprite2DList_W;//Спрайт 2D Top Down будущего свойства
    [SerializeField]
    private List<PropSize> XMLPropSizeList;//размер

    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D North
    [SerializeField]
    private List<float> XMLPivotX_2DN;
    [SerializeField]
    private List<float> XMLPivotY_2DN;
    [SerializeField]
    private List<float> XMLPixelPerU_2DN;
   
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D East
    [SerializeField]
    private List<float> XMLPivotX_2DE;
    [SerializeField]
    private List<float> XMLPivotY_2DE;
    [SerializeField]
    private List<float> XMLPixelPerU_2DE;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D South
    [SerializeField]
    private List<float> XMLPivotX_2DS;
    [SerializeField]
    private List<float> XMLPivotY_2DS;
    [SerializeField]
    private List<float> XMLPixelPerU_2DS;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D West
    [SerializeField]
    private List<float> XMLPivotX_2DW;
    [SerializeField]
    private List<float> XMLPivotY_2DW;
    [SerializeField]
    private List<float> XMLPixelPerU_2DW;

    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D ISOMETRIC North
    [SerializeField]
    private List<float> XMLPivotX_IsoN;
    [SerializeField]
    private List<float> XMLPivotY_IsoN;
    [SerializeField]
    private List<float> XMLPixelPerU_IsoN;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D ISOMETRIC East
    [SerializeField]
    private List<float> XMLPivotX_IsoE;
    [SerializeField]
    private List<float> XMLPivotY_IsoE;
    [SerializeField]
    private List<float> XMLPixelPerU_IsoE;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D ISOMETRIC South
    [SerializeField]
    private List<float> XMLPivotX_IsoS;
    [SerializeField]
    private List<float> XMLPivotY_IsoS;
    [SerializeField]
    private List<float> XMLPixelPerU_IsoS;
    //-----НАСТРОЙКИ ДЛЯ ЗАГРУЖАЕМОГО СРПАЙТА 2D ISOMETRIC West
    [SerializeField]
    private List<float> XMLPivotX_IsoW;
    [SerializeField]
    private List<float> XMLPivotY_IsoW;
    [SerializeField]
    private List<float> XMLPixelPerU_IsoW;


    /// <summary>
    /// Добавить спрайт
    /// </summary>
    /// <param name="_spriteN"></param>
    /// <param name="_spriteE"></param>
    /// <param name="_spriteS"></param>
    /// <param name="_spriteW"></param>
    /// <param name="_projType"></param>
    public void AddSprite(Sprite _spriteN, Sprite _spriteE, Sprite _spriteS, Sprite _spriteW, ProjectionType _projType)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                XMLSprite2DList_N.Add(_spriteN);
                XMLSprite2DList_E.Add(_spriteE);
                XMLSprite2DList_S.Add(_spriteS);
                XMLSprite2DList_W.Add(_spriteW);
                break;
            case ProjectionType.Isometric2D:
                XMLSpriteIsoList_N.Add(_spriteN);
                XMLSpriteIsoList_E.Add(_spriteE);
                XMLSpriteIsoList_S.Add(_spriteS);
                XMLSpriteIsoList_W.Add(_spriteW);
                break;
            default:
                Debug.Log("MG_PropXML_data AddSprite(): нереализованное ProjectionType " + _projType);
                break;
        }
    }

    /// <summary>
    /// Получить лист со спрайтами
    /// </summary>
    /// <param name="_dir"></param>
    /// <returns></returns>
    public List<Sprite> GetSpriteList(Direction _dir, ProjectionType _projType)
    {
        List<Sprite>[] _listIso = new List<Sprite>[] { XMLSpriteIsoList_N, XMLSpriteIsoList_E, XMLSpriteIsoList_S, XMLSpriteIsoList_W };//список списков Iso спрайтов
        List<Sprite>[] _list2DTop = new List<Sprite>[] { XMLSprite2DList_N, XMLSprite2DList_E, XMLSprite2DList_S, XMLSprite2DList_W };//список списков 2DTop спрайтов
        List<Sprite>[] _chosenList = new List<Sprite>[] { };//выбранный список списков спрайтов
        List<Sprite> _spriteList = new List<Sprite> { };//выбранный спрайт лист

        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                _chosenList = _list2DTop;
                break;
            case ProjectionType.Isometric2D:
                _chosenList = _listIso;
                break;
            default:
                Debug.Log("MG_PropXML_data AddSprite(): нереализованное ProjectionType " + _projType);
                return null;
        }

        switch (_dir)
        {
            case Direction.North:
                _spriteList = _chosenList[0];
                break;
            case Direction.East:
                _spriteList = _chosenList[1];
                break;
            case Direction.South:
                _spriteList = _chosenList[2];
                break;
            case Direction.West:
                _spriteList = _chosenList[3];
                break;
            default:
                Debug.Log("MG_PropXML_data AddSprite(): нереализованное Direction " + _dir);
                return null;
        }

        return _spriteList;
    }

    //-----------------------------

    /// <summary>
    /// Добавить Pivot
    /// </summary>
    /// <param name="_pivotX"></param>
    /// <param name="_pivotY"></param>
    /// <param name="_dir"></param>
    /// <param name="_projType"></param>
    public void AddPivot(float _pivotX, float _pivotY, Direction _dir, ProjectionType _projType)
    {
        List<float>[] _listIsoX = new List<float>[] { XMLPivotX_IsoN, XMLPivotX_IsoE, XMLPivotX_IsoS, XMLPivotX_IsoW };
        List<float>[] _listIsoY = new List<float>[] { XMLPivotY_IsoN, XMLPivotY_IsoE, XMLPivotY_IsoS, XMLPivotY_IsoW };
        List<float>[] _list2DTopX = new List<float>[] { XMLPivotX_2DN, XMLPivotX_2DE, XMLPivotX_2DS, XMLPivotX_2DW };
        List<float>[] _list2DTopY = new List<float>[] { XMLPivotY_2DN, XMLPivotY_2DE, XMLPivotY_2DS, XMLPivotY_2DW };
        List<float>[] _chosenListX = new List<float>[] { };//выбранный список списков спрайтов
        List<float>[] _chosenListY = new List<float>[] { };//выбранный список списков спрайтов

        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                _chosenListX = _list2DTopX;
                _chosenListY = _list2DTopY;
                break;
            case ProjectionType.Isometric2D:
                _chosenListX = _listIsoX;
                _chosenListY = _listIsoY;
                break;
            default:
                Debug.Log("MG_PropXML_data AddPivot(): нереализованное ProjectionType " + _projType);
                break;
        }

        switch (_dir)
        {
            case Direction.North:
                _chosenListX[0].Add(_pivotX);
                _chosenListY[0].Add(_pivotY);
                break;
            case Direction.East:
                _chosenListX[1].Add(_pivotX);
                _chosenListY[1].Add(_pivotY);
                break;
            case Direction.South:
                _chosenListX[2].Add(_pivotX);
                _chosenListY[2].Add(_pivotY);
                break;
            case Direction.West:
                _chosenListX[3].Add(_pivotX);
                _chosenListY[3].Add(_pivotY);
                break;
            default:
                Debug.Log("MG_PropXML_data AddPivot(): нереализованное Direction " + _dir);
                break;
        }

    }

    /// <summary>
    /// Получить Пивот X
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_projType"></param>
    /// <returns></returns>
    public List<float> GetListPivotX(Direction _dir, ProjectionType _projType)
    {
        List<float>[] _listIsoX = new List<float>[] { XMLPivotX_IsoN, XMLPivotX_IsoE, XMLPivotX_IsoS, XMLPivotX_IsoW };
        List<float>[] _list2DTopX = new List<float>[] { XMLPivotX_2DN, XMLPivotX_2DE, XMLPivotX_2DS, XMLPivotX_2DW };
        List<float>[] _chosenListX = new List<float>[] { };//выбранный список списков спрайтов

        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                _chosenListX = _list2DTopX;
                break;
            case ProjectionType.Isometric2D:
                _chosenListX = _listIsoX;
                break;
            default:
                Debug.Log("MG_PropXML_data GetListPivotX(): нереализованное ProjectionType " + _projType);
                return null;
        }

        switch (_dir)
        {
            case Direction.North:
                return _chosenListX[0];
            case Direction.East:
                return _chosenListX[1];
            case Direction.South:
                return _chosenListX[2];
            case Direction.West:
                return _chosenListX[3];
            default:
                Debug.Log("MG_PropXML_data GetListPivotX(): нереализованное Direction " + _dir);
                return null;
        }
    }

    /// <summary>
    /// Получить Пивот Y
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_projType"></param>
    /// <returns></returns>
    public List<float> GetListPivotY(Direction _dir, ProjectionType _projType)
    {
        List<float>[] _listIsoY = new List<float>[] { XMLPivotY_IsoN, XMLPivotY_IsoE, XMLPivotY_IsoS, XMLPivotY_IsoW };
        List<float>[] _list2DTopY = new List<float>[] { XMLPivotY_2DN, XMLPivotY_2DE, XMLPivotY_2DS, XMLPivotY_2DW };
        List<float>[] _chosenListY = new List<float>[] { };//выбранный список списков спрайтов

        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                _chosenListY = _list2DTopY;
                break;
            case ProjectionType.Isometric2D:
                _chosenListY = _listIsoY;
                break;
            default:
                Debug.Log("MG_PropXML_data GetListPivotY(): нереализованное ProjectionType " + _projType);
                return null;
        }

        switch (_dir)
        {
            case Direction.North:
                return _chosenListY[0];
            case Direction.East:
                return _chosenListY[1];
            case Direction.South:
                return _chosenListY[2];
            case Direction.West:
                return _chosenListY[3];
            default:
                Debug.Log("MG_PropXML_data GetListPivotY(): нереализованное Direction " + _dir);
                return null;
        }
    }

    //-----------------------------

    /// <summary>
    /// Добавить Pixel Per U
    /// </summary>
    /// <param name="_pixelPerU_N"></param>
    /// <param name="_pixelPerU_E"></param>
    /// <param name="_pixelPerU_S"></param>
    /// <param name="_pixelPerU_W"></param>
    public void AddPixelPerU(float _pixelPerU_N, float _pixelPerU_E, float _pixelPerU_S, float _pixelPerU_W, ProjectionType _projType)
    {
        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                XMLPixelPerU_2DN.Add(_pixelPerU_N);
                XMLPixelPerU_2DE.Add(_pixelPerU_E);
                XMLPixelPerU_2DS.Add(_pixelPerU_S);
                XMLPixelPerU_2DW.Add(_pixelPerU_W);
                break;
            case ProjectionType.Isometric2D:
                XMLPixelPerU_IsoN.Add(_pixelPerU_N);
                XMLPixelPerU_IsoE.Add(_pixelPerU_E);
                XMLPixelPerU_IsoS.Add(_pixelPerU_S);
                XMLPixelPerU_IsoW.Add(_pixelPerU_W);
                break;
            default:
                Debug.Log("MG_PropXML_data AddPixelPerU(): нереализованное ProjectionType " + _projType);
                break;
        }
    }

    public List<float> GetPixelPerU(Direction _dir, ProjectionType _projType)
    {
        List<float>[] _listIso = new List<float>[] { XMLPixelPerU_IsoN, XMLPixelPerU_IsoE, XMLPixelPerU_IsoS, XMLPixelPerU_IsoW };
        List<float>[] _list2DTop = new List<float>[] { XMLPixelPerU_2DN, XMLPixelPerU_2DE, XMLPixelPerU_2DS, XMLPixelPerU_2DW };
        List<float>[] _chosenList = new List<float>[] { };

        switch (_projType)
        {
            case ProjectionType.TopDown2D:
                _chosenList = _list2DTop;
                break;
            case ProjectionType.Isometric2D:
                _chosenList = _listIso;
                break;
            default:
                Debug.Log("MG_PropXML_data AddPivot(): нереализованное ProjectionType " + _projType);
                return null;
        }

        switch (_dir)
        {
            case Direction.North:
                return _chosenList[0];
            case Direction.East:
                return _chosenList[1];
            case Direction.South:
                return _chosenList[2];
            case Direction.West:
                return _chosenList[3];
            default:
                Debug.Log("MG_PropXML_data AddPivot(): нереализованное Direction " + _dir);
                return null;
        }
    }


    //-----------------------------

    /// <summary>
    /// Получить список размеров
    /// </summary>
    /// <returns></returns>
    public List<PropSize> GetPropSizeList()
    {
        return XMLPropSizeList;
    }

    /// <summary>
    /// Добавить prop size
    /// </summary>
    /// <param name="_propSize"></param>
    public void AddPropSize(PropSize _propSize)
    {
        XMLPropSizeList.Add(_propSize);
    }

    //--------------------------





}
