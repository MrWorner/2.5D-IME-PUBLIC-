using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_PropPropertyConstructor : MonoBehaviour
{
    [SerializeField]
    private MG_PropPropertyLibrary propPropertyLibrary;//[R] библиотека свойств
    [SerializeField]
    private MG_PropDefault_data propDefault_data;//[R] компонент для загрузки default пола от самого разраба
    [SerializeField]
    private MG_PropXML_data propXML_data;//[R] компонент для загрузки пользовательского пола
    [SerializeField]
    private MG_PropMissing propMissing;//[R]
    [SerializeField]
    private Tile baseTile;//[R] базовый тайл

    private void Awake()
    {
        if (baseTile == null)
            Debug.Log("<color=red>MG_PropPropertyConstructor Awake(): объект Tile для baseTile не прикреплен!</color>");
        if (propPropertyLibrary == null)
            Debug.Log("<color=red>MG_PropPropertyConstructor Awake(): propPropertyLibrary не прикреплен!</color>");
        if (propDefault_data == null)
            Debug.Log("<color=red>MG_PropPropertyConstructor Awake(): propDefault_data не прикреплен!</color>");
        if (propXML_data == null)
            Debug.Log("<color=red>MG_PropPropertyConstructor Awake(): propXML_data не прикреплен!</color>");
        if (propMissing == null)
            Debug.Log("<color=red>MG_PropPropertyConstructor Awake(): MG_PropMissing не прикреплен!</color>");
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init()//Инициализация
    {
        ProcessAndConstruct();//ищем дубликаты свойства с default и пользовательскими, чтобы заменить на пользовательские. Также создаем свойство и добавляем в библиотеку
    }

    /// <summary>
    /// Обработать и создать свойства. Пользовательские свойства с одинаковым Id будут заменять стандартное свойство
    /// </summary>
    private void ProcessAndConstruct()
    {
        List<int> _iList = new List<int>();//все те которые заменили стандартные свойства

        var _arrayID_default = propDefault_data.GetIDList();
        var _arrayName_default = propDefault_data.GetNameList();
        var _arraySpriteIso_default_N = propDefault_data.GetSpriteIsoList(Direction.North);
        var _arraySpriteIso_default_E = propDefault_data.GetSpriteIsoList(Direction.East);
        var _arraySpriteIso_default_S = propDefault_data.GetSpriteIsoList(Direction.South);
        var _arraySpriteIso_default_W = propDefault_data.GetSpriteIsoList(Direction.West);
        var _arraySprite2D_default_N = propDefault_data.GetSprite2DList(Direction.North);
        var _arraySprite2D_default_E = propDefault_data.GetSprite2DList(Direction.East);
        var _arraySprite2D_default_S = propDefault_data.GetSprite2DList(Direction.South);
        var _arraySprite2D_default_W = propDefault_data.GetSprite2DList(Direction.West);

        var _arrayPropSize_default = propDefault_data.GetPropSizeList();

        var _arrayID_XML = propXML_data.GetIDList();
        var _arrayName_XML = propXML_data.GetNameList();
        var _arraySpriteIso_XML_N = propXML_data.GetSpriteList(Direction.North, ProjectionType.Isometric2D);
        var _arraySpriteIso_XML_E = propXML_data.GetSpriteList(Direction.East, ProjectionType.Isometric2D);
        var _arraySpriteIso_XML_S = propXML_data.GetSpriteList(Direction.South, ProjectionType.Isometric2D);
        var _arraySpriteIso_XML_W = propXML_data.GetSpriteList(Direction.West, ProjectionType.Isometric2D);
        var _arraySprite2D_XML_N = propXML_data.GetSpriteList(Direction.North, ProjectionType.TopDown2D);
        var _arraySprite2D_XML_E = propXML_data.GetSpriteList(Direction.East, ProjectionType.TopDown2D);
        var _arraySprite2D_XML_S = propXML_data.GetSpriteList(Direction.South, ProjectionType.TopDown2D);
        var _arraySprite2D_XML_W = propXML_data.GetSpriteList(Direction.West, ProjectionType.TopDown2D);
        
        var _arrayPropSize_XML = propXML_data.GetPropSizeList();

        int i = 0;
        foreach (var _id in _arrayID_default)
        {
            bool _found = false;
            foreach (var _idXML in _arrayID_XML)
            {
                int ii = 0;
                if (_idXML.Equals(_id))
                {
                    _found = true;
                    _iList.Add(ii);
                    Construct(_arrayID_XML[ii], _arrayName_XML[ii], _arraySprite2D_XML_N[ii], _arraySprite2D_XML_E[ii], _arraySprite2D_XML_S[ii], _arraySprite2D_XML_W[ii], _arraySpriteIso_XML_N[ii], _arraySpriteIso_XML_E[ii], _arraySpriteIso_XML_S[ii], _arraySpriteIso_XML_W[ii], _arrayPropSize_XML[ii]);
                    ii++;
                    //continue;//УБРАЛ ДЛЯ МНОГОЧИСЛЕННОЙ ПЕРЕЗАПИСИ ИЗ ~ДРУГИХ~ МОДОВ!
                }

                ii++;
            }
            if (!_found) Construct(_arrayID_default[i], _arrayName_default[i], _arraySprite2D_default_N[i], _arraySprite2D_default_E[i], _arraySprite2D_default_S[i], _arraySprite2D_default_W[i], _arraySpriteIso_default_N[i], _arraySpriteIso_default_E[i], _arraySpriteIso_default_S[i], _arraySpriteIso_default_W[i], _arrayPropSize_default[i]);
            i++;
        }
     
        for (int iii = 0; iii < _arrayID_XML.Count; iii++)
        {
            bool _skip = false;
            foreach (int ii in _iList)
            {
                if (ii == iii) _skip = true;
            }
            if (!_skip)
                Construct(_arrayID_XML[iii], _arrayName_XML[iii], _arraySprite2D_XML_N[iii], _arraySprite2D_XML_E[iii], _arraySprite2D_XML_S[iii], _arraySprite2D_XML_W[iii], _arraySpriteIso_XML_N[iii], _arraySpriteIso_XML_E[iii], _arraySpriteIso_XML_S[iii], _arraySpriteIso_XML_W[iii], _arrayPropSize_XML[iii]);
        }
    }

    /// <summary>
    /// Создание свойства, также добавление в библиотеку
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_name"></param>
    /// <param name="_sprite2D_N"></param>
    /// <param name="_sprite2D_E"></param>
    /// <param name="_sprite2D_S"></param>
    /// <param name="_sprite2D_W"></param>
    /// <param name="_sprite2Diso_N"></param>
    /// <param name="_sprite2Diso_E"></param>
    /// <param name="_sprite2Diso_S"></param>
    /// <param name="_sprite2Diso_W"></param>
    private void Construct(string _id, string _name, Sprite _sprite2D_N, Sprite _sprite2D_E, Sprite _sprite2D_S, Sprite _sprite2D_W, Sprite _sprite2Diso_N, Sprite _sprite2Diso_E, Sprite _sprite2Diso_S, Sprite _sprite2Diso_W, PropSize _size)
    {
        MG_PropProperty _tileProp = GeneratePropProperty(_id, _name, _sprite2D_N, _sprite2D_E, _sprite2D_S, _sprite2D_W, _sprite2Diso_N, _sprite2Diso_E, _sprite2Diso_S, _sprite2Diso_W, _size);
        propPropertyLibrary.AddPropToDic(_tileProp);
    }

    /// <summary>
    /// Сгенерировать свойство
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_name"></param>
    /// <param name="_sprite2D_N"></param>
    /// <param name="_sprite2D_E"></param>
    /// <param name="_sprite2D_S"></param>
    /// <param name="_sprite2D_W"></param>
    /// <param name="_sprite2Diso_N"></param>
    /// <param name="_sprite2Diso_E"></param>
    /// <param name="_sprite2Diso_S"></param>
    /// <param name="_sprite2Diso_W"></param>
    /// <returns></returns>
    private MG_PropProperty GeneratePropProperty(string _id, string _name, Sprite _sprite2D_N, Sprite _sprite2D_E, Sprite _sprite2D_S, Sprite _sprite2D_W, Sprite _sprite2Diso_N, Sprite _sprite2Diso_E, Sprite _sprite2Diso_S, Sprite _sprite2Diso_W, PropSize _size)
    {
        MG_PropProperty _tileProp = new MG_PropProperty
        {
            ID = _id,
            Name = _name,
            Tile2DIso_N = Instantiate(baseTile),
            Tile2DIso_E = Instantiate(baseTile),
            Tile2DIso_S = Instantiate(baseTile),
            Tile2DIso_W = Instantiate(baseTile),
            Tile2D_N = Instantiate(baseTile),
            Tile2D_E = Instantiate(baseTile),
            Tile2D_S = Instantiate(baseTile),
            Tile2D_W = Instantiate(baseTile),
            Size = _size
        };
        _tileProp.Tile2D_N.sprite = _sprite2D_N;//задаем новому тайлу спрайт
        _tileProp.Tile2D_E.sprite = _sprite2D_E;//задаем новому тайлу спрайт
        _tileProp.Tile2D_S.sprite = _sprite2D_S;//задаем новому тайлу спрайт
        _tileProp.Tile2D_W.sprite = _sprite2D_W;//задаем новому тайлу спрайт
        _tileProp.Tile2DIso_N.sprite = _sprite2Diso_N;//задаем новому тайлу спрайт
        _tileProp.Tile2DIso_E.sprite = _sprite2Diso_E;//задаем новому тайлу спрайт
        _tileProp.Tile2DIso_S.sprite = _sprite2Diso_S;//задаем новому тайлу спрайт
        _tileProp.Tile2DIso_W.sprite = _sprite2Diso_W;//задаем новому тайлу спрайт
        _tileProp.Icon = _sprite2Diso_S;//задаем иконку для свойства
        return _tileProp;
    }

    /// <summary>
    /// Сконструировать временное свойство для ненайденных свойств во время загрузки карты.
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    public MG_PropProperty ConstructTemp(string _id, PropSize _type)
    {
        Sprite _sprite2D_N = propMissing.GetSprite2D(Direction.North);
        Sprite _sprite2D_E = propMissing.GetSprite2D(Direction.East);
        Sprite _sprite2D_S = propMissing.GetSprite2D(Direction.South);
        Sprite _sprite2D_W = propMissing.GetSprite2D(Direction.West);

        Sprite _sprite2Diso_N = propMissing.GetSpriteIso(Direction.North);
        Sprite _sprite2Diso_E = propMissing.GetSpriteIso(Direction.East);
        Sprite _sprite2Diso_S = propMissing.GetSpriteIso(Direction.South);
        Sprite _sprite2Diso_W = propMissing.GetSpriteIso(Direction.West);

        MG_PropProperty _tileProp = GeneratePropProperty(_id, "temp",  _sprite2D_N,  _sprite2D_E,  _sprite2D_S,  _sprite2D_W,  _sprite2Diso_N,  _sprite2Diso_E,  _sprite2Diso_S,  _sprite2Diso_W, _type);
        propPropertyLibrary.AddPropToDic(_tileProp);
        return _tileProp;
    }
}
