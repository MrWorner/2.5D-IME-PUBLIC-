using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_WallPropertyConstructor : MonoBehaviour
{
    [SerializeField]
    private MG_WallPropertyLibrary wallPropertyLibrary;//[R] библиотека свойств
    [SerializeField]
    private MG_WallDefault_data wallDefault_data;//[R] компонент для загрузки default пола от самого разраба
    [SerializeField]
    private MG_WallXML_data wallXML_data;//[R] компонент для загрузки пользовательского пола
    [SerializeField]
    private MG_WallMissing wallMissing;//[R]
    [SerializeField]
    private Tile baseTile;//[R] базовый тайл

    private void Awake()
    {
        if (baseTile == null)
            Debug.Log("<color=red>MG_WallPropertyConstructor Awake(): объект Tile для baseTile не прикреплен!</color>");
        if (wallPropertyLibrary == null)
            Debug.Log("<color=red>MG_WallPropertyConstructor Awake(): MG_WallPropertyLibrary не прикреплен!</color>");
        if (wallDefault_data == null)
            Debug.Log("<color=red>MG_WallPropertyConstructor Awake(): MG_WallDefault_data не прикреплен!</color>");
        if (wallXML_data == null)
            Debug.Log("<color=red>MG_WallPropertyConstructor Awake(): MG_WallXML_data не прикреплен!</color>");
        if (wallMissing == null)
            Debug.Log("<color=red>MG_WallPropertyConstructor Awake(): MG_WallMissing не прикреплен!</color>");
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

        var _arrayID_default = wallDefault_data.GetIDList();
        var _arrayName_default = wallDefault_data.GetNameList();
        var _arraySpriteIsoH_default = wallDefault_data.GetSpriteIsoHList();
        var _arraySpriteIsoV_default = wallDefault_data.GetSpriteIsoVList();
        var _arraySprite2DH_default = wallDefault_data.GetSprite2DHList();
        var _arraySprite2DV_default = wallDefault_data.GetSprite2DVList();
        var _arrayType_default = wallDefault_data.GetWallTypeList();

        var _arrayID_XML = wallXML_data.GetIDList();
        var _arrayName_XML = wallXML_data.GetNameList();
        var _arraySpriteIsoH_XML = wallXML_data.GetSpriteListH(ProjectionType.Isometric2D);
        var _arraySpriteIsoV_XML = wallXML_data.GetSpriteListV(ProjectionType.Isometric2D);
        var _arraySprite2DH_XML = wallXML_data.GetSpriteListH(ProjectionType.TopDown2D);
        var _arraySprite2DV_XML = wallXML_data.GetSpriteListV(ProjectionType.TopDown2D);
        var _arrayType_XML = wallXML_data.GetWallTypeList();

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
                    Construct(_arrayID_XML[ii], _arrayName_XML[ii], _arraySprite2DH_XML[ii], _arraySprite2DV_XML[ii], _arraySpriteIsoH_XML[ii], _arraySpriteIsoV_XML[ii], _arrayType_XML[ii]);
                    ii++;
                    //continue;//УБРАЛ ДЛЯ МНОГОЧИСЛЕННОЙ ПЕРЕЗАПИСИ ИЗ ДРУГИХ МОДОВ!
                }
                ii++;
            }
            if (!_found) Construct(_arrayID_default[i], _arrayName_default[i], _arraySprite2DH_default[i], _arraySprite2DV_default[i], _arraySpriteIsoH_default[i], _arraySpriteIsoV_default[i], _arrayType_default[i]);
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
                Construct(_arrayID_XML[iii], _arrayName_XML[iii], _arraySprite2DH_XML[iii], _arraySprite2DV_XML[iii], _arraySpriteIsoH_XML[iii], _arraySpriteIsoV_XML[iii], _arrayType_XML[iii]);
        }
    }

    /// <summary>
    /// Создание свойства, также добавление в библиотеку
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_name"></param>
    /// <param name="_sprite2D"></param>
    /// <param name="_sprite2Diso"></param>
    /// <param name="_type"></param>
    private void Construct(string _id, string _name, Sprite _sprite2DH, Sprite _sprite2DV, Sprite _spriteIsoH, Sprite _spriteIsoV, WallType _type)
    {
        MG_WallProperty _tileProp = GenerateFloorProperty(_id, _name, _sprite2DH, _sprite2DV, _spriteIsoH, _spriteIsoV, _type);
        wallPropertyLibrary.AddPropToDic(_tileProp);
    }

    /// <summary>
    /// Сгенерировать свойство
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_name"></param>
    /// <param name="_sprite2DH"></param>
    /// <param name="_sprite2DV"></param>
    /// <param name="_spriteIsoH"></param>
    /// <param name="_spriteIsoV"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    private MG_WallProperty GenerateFloorProperty(string _id, string _name, Sprite _sprite2DH, Sprite _sprite2DV, Sprite _spriteIsoH, Sprite _spriteIsoV, WallType _type)
    {
        MG_WallProperty _tileProp = new MG_WallProperty
        {
            ID = _id,
            Name = _name,
            Tile2DIso_H = Instantiate(baseTile),
            Tile2DIso_V = Instantiate(baseTile),
            Tile2D_H = Instantiate(baseTile),
            Tile2D_V = Instantiate(baseTile),
            Type = _type
        };
        _tileProp.Tile2D_H.sprite = _sprite2DH;//задаем новому тайлу спрайт
        _tileProp.Tile2D_V.sprite = _sprite2DV;//задаем новому тайлу спрайт
        _tileProp.Tile2DIso_H.sprite = _spriteIsoH;//задаем новому тайлу спрайт
        _tileProp.Tile2DIso_V.sprite = _spriteIsoV;//задаем новому тайлу спрайт
        _tileProp.Icon = _spriteIsoH;//задаем иконку для свойства
        return _tileProp;
    }

    /// <summary>
    /// Сконструировать временное свойство для ненайденных свойств во время загрузки карты.
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    public MG_WallProperty ConstructTemp(string _id, WallType _type)
    {
        Sprite _sprite2DH = wallMissing.GetSprite2DH();
        Sprite _sprite2DV = wallMissing.GetSprite2DV();
        Sprite _spriteIsoH = wallMissing.GetSpriteIsoH();
        Sprite _spriteIsoV = wallMissing.GetSpriteIsoV();

        MG_WallProperty _tileProp = GenerateFloorProperty( _id,  "temp",  _sprite2DH,  _sprite2DV,  _spriteIsoH,  _spriteIsoV,  _type);
        wallPropertyLibrary.AddPropToDic(_tileProp);
        return _tileProp;
    }
}
