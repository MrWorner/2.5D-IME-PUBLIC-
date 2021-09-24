using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_FloorPropertyConstructor : MonoBehaviour
{
    [SerializeField]
    private MG_FloorPropertyLibrary floorPropertyLibrary;//[R] библиотека свойств
    [SerializeField]
    private MG_FloorDefault_data floorDefault_data;//[R] компонент для загрузки default пола от самого разраба
    [SerializeField]
    private MG_FloorXML_data floorXML_data;//[R] компонент для загрузки пользовательского пола
    [SerializeField]
    private MG_FloorMissing floorMissing;//[R]
    [SerializeField]
    private Tile baseTile;//[R] базовый тайл

    private void Awake()
    {
        if (baseTile == null)
            Debug.Log("<color=red>MG_FloorPropertyConstructor Awake(): объект Tile для baseTile не прикреплен!</color>");
        if (floorPropertyLibrary == null)
            Debug.Log("<color=red>MG_FloorPropertyConstructor Awake(): MG_FloorPropertyLibrary не прикреплен!</color>");
        if (floorDefault_data == null)
            Debug.Log("<color=red>MG_FloorPropertyConstructor Awake(): MG_FloorDefault_data не прикреплен!</color>");
        if (floorXML_data == null)
            Debug.Log("<color=red>MG_FloorPropertyConstructor Awake(): MG_FloorXML_data не прикреплен!</color>");
        if (floorMissing == null)
            Debug.Log("<color=red>MG_FloorPropertyConstructor Awake(): MG_FloorMissing не прикреплен!</color>");
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

        var _arrayID_default = floorDefault_data.GetIDList();
        var _arrayName_default = floorDefault_data.GetNameList();
        var _arraySpriteIso_default = floorDefault_data.GetSpriteIsoList();
        var _arraySprite2D_default = floorDefault_data.GetSprite2DList();
        var _arrayType_default = floorDefault_data.GetFloorTypeList();

        var _arrayID_XML = floorXML_data.GetIDList();
        var _arrayName_XML = floorXML_data.GetNameList();
        var _arraySpriteIso_XML = floorXML_data.GetSpriteList(ProjectionType.Isometric2D);
        var _arraySprite2D_XML = floorXML_data.GetSpriteList(ProjectionType.TopDown2D);
        var _arrayType_XML = floorXML_data.GetFloorTypeList();

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
                    Construct(_arrayID_XML[ii], _arrayName_XML[ii], _arraySprite2D_XML[ii], _arraySpriteIso_XML[ii], _arrayType_XML[ii]);
                    ii++;
                    //continue;//УБРАЛ ДЛЯ МНОГОЧИСЛЕННОЙ ПЕРЕЗАПИСИ ИЗ ДРУГИХ МОДОВ!
                }

                ii++;
            }
            if (!_found) Construct(_arrayID_default[i], _arrayName_default[i], _arraySprite2D_default[i], _arraySpriteIso_default[i], _arrayType_default[i]);
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
                Construct(_arrayID_XML[iii], _arrayName_XML[iii], _arraySprite2D_XML[iii], _arraySpriteIso_XML[iii], _arrayType_XML[iii]);
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
    private void Construct(string _id, string _name, Sprite _sprite2D, Sprite _sprite2Diso, FloorType _type)
    {
        MG_FloorProperty _tileProp = GenerateFloorProperty(_id, _name, _sprite2D, _sprite2Diso, _type);
        floorPropertyLibrary.AddPropToDic(_tileProp);
    }

    /// <summary>
    /// Сгенерировать свойство
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_name"></param>
    /// <param name="_sprite2D"></param>
    /// <param name="_sprite2Diso"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    private MG_FloorProperty GenerateFloorProperty(string _id, string _name, Sprite _sprite2D, Sprite _sprite2Diso, FloorType _type)
    {
        MG_FloorProperty _tileProp = new MG_FloorProperty
        {
            ID = _id,
            Name = _name,
            Tile2DIso = Instantiate(baseTile),
            Tile2D = Instantiate(baseTile),
            Type = _type
        };
        _tileProp.Tile2D.sprite = _sprite2D;//задаем новому тайлу спрайт
        _tileProp.Tile2DIso.sprite = _sprite2Diso;//задаем новому тайлу спрайт
        _tileProp.Icon = _sprite2Diso;//задаем иконку для свойства
        return _tileProp;
    }

    /// <summary>
    /// Сконструировать временное свойство для ненайденных свойств во время загрузки карты.
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    public MG_FloorProperty ConstructTemp(string _id, FloorType _type)
    {
        Sprite _sprite2D = floorMissing.GetSprite2D();
        Sprite _sprite2Diso = floorMissing.GetSpriteIso();

        MG_FloorProperty _tileProp = GenerateFloorProperty(_id, "temp", _sprite2D, _sprite2Diso, _type);
        floorPropertyLibrary.AddPropToDic(_tileProp);
        return _tileProp;
    }

}
