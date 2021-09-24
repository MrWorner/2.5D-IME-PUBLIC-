using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_EntryPropertyConstructor : MonoBehaviour
{

    [SerializeField]
    private MG_EntryPropertyLibrary entryPropertyLibrary;//[R] библиотека свойств
    [SerializeField]
    private MG_EntryDefault_data entryDefault_data;//[R] компонент для загрузки default пола от самого разраба
    [SerializeField]
    private MG_EntryXML_data entryXML_data;//[R] компонент для загрузки пользовательского пола
    [SerializeField]
    private MG_EntryMissing entryMissing;//[R]
    [SerializeField]
    private Tile baseTile;//[R] базовый тайл

    private void Awake()
    {
        if (baseTile == null)
            Debug.Log("<color=red>MG_EntryPropertyConstructor Awake(): объект Tile для baseTile не прикреплен!</color>");
        if (entryPropertyLibrary == null)
            Debug.Log("<color=red>MG_EntryPropertyConstructor Awake(): entryPropertyLibrary не прикреплен!</color>");
        if (entryDefault_data == null)
            Debug.Log("<color=red>MG_EntryPropertyConstructor Awake(): entryDefault_data не прикреплен!</color>");
        if (entryXML_data == null)
            Debug.Log("<color=red>MG_EntryPropertyConstructor Awake(): entryXML_data не прикреплен!</color>");
        if (entryMissing == null)
            Debug.Log("<color=red>MG_EntryPropertyConstructor Awake(): MG_EntryMissing не прикреплен!</color>");
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

        var _arrayID_default = entryDefault_data.GetIDList();
        var _arrayName_default = entryDefault_data.GetNameList();
        var _arraySpriteIsoH_default = entryDefault_data.GetSpriteIsoHList();
        var _arraySpriteIsoV_default = entryDefault_data.GetSpriteIsoVList();
        var _arraySprite2DH_default = entryDefault_data.GetSprite2DHList();
        var _arraySprite2DV_default = entryDefault_data.GetSprite2DVList();
        var _arrayType_default = entryDefault_data.GetEntryTypeList();

        var _arrayID_XML = entryXML_data.GetIDList();
        var _arrayName_XML = entryXML_data.GetNameList();
        var _arraySpriteIsoH_XML = entryXML_data.GetSpriteListH(ProjectionType.Isometric2D);
        var _arraySpriteIsoV_XML = entryXML_data.GetSpriteListV(ProjectionType.Isometric2D);
        var _arraySprite2DH_XML = entryXML_data.GetSpriteListH(ProjectionType.TopDown2D);
        var _arraySprite2DV_XML = entryXML_data.GetSpriteListV(ProjectionType.TopDown2D);
        var _arrayType_XML = entryXML_data.GetEntryTypeList();

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
    private void Construct(string _id, string _name, Sprite _sprite2DH, Sprite _sprite2DV, Sprite _spriteIsoH, Sprite _spriteIsoV, EntryType _type)
    {
        MG_EntryProperty _tileProp = GenerateEntryProperty(_id, _name, _sprite2DH, _sprite2DV, _spriteIsoH, _spriteIsoV, _type);
        entryPropertyLibrary.AddPropToDic(_tileProp);
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
    private MG_EntryProperty GenerateEntryProperty(string _id, string _name, Sprite _sprite2DH, Sprite _sprite2DV, Sprite _spriteIsoH, Sprite _spriteIsoV, EntryType _type)
    {
        MG_EntryProperty _tileProp = new MG_EntryProperty
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
    public MG_EntryProperty ConstructTemp(string _id, EntryType _type)
    {
        Sprite _sprite2DH = entryMissing.GetSprite(LineDirection.Horizontal, ProjectionType.TopDown2D, _type);
        Sprite _sprite2DV = entryMissing.GetSprite(LineDirection.Vertical, ProjectionType.TopDown2D, _type);
        Sprite _spriteIsoH = entryMissing.GetSprite(LineDirection.Horizontal, ProjectionType.Isometric2D, _type);
        Sprite _spriteIsoV = entryMissing.GetSprite(LineDirection.Vertical, ProjectionType.Isometric2D, _type);

        MG_EntryProperty _tileProp = GenerateEntryProperty(_id, "temp",  _sprite2DH,  _sprite2DV,  _spriteIsoH,  _spriteIsoV, _type);
        entryPropertyLibrary.AddPropToDic(_tileProp);
        return _tileProp;
    }
}
