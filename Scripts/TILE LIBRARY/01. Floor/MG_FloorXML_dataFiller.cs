using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MG_FloorXML_dataFiller : MG_TileXML_dataFiller
{
    //-------------ПРИМЕР НОДА
    //1 <FloorTile id="MyFloor" Name="My carpet" FloorType="Floor">
    //1.1      <SpriteIso pivotX ="0.5" pivotY="0.13" pixelPerU ="100f">1.png</SpriteIso>
    //1.2      <SpriteTop pivotX ="0.5" pivotY="0.13" pixelPerU ="50f">1d.png</SpriteTop>
    // </FloorTile>

    [SerializeField]
    private MG_FloorMissing floorMissing;//[R] для получения 'оранжевых' шаблонов
    [SerializeField]
    private MG_FloorPropertyConstructor floorPropertyConstructor;//[R] для получения 'оранжевых' шаблонов
    [SerializeField]
    private MG_FloorXML_data floorXML_data;//[R]

    private void Awake()
    {
        if (floorMissing == null)
            Debug.Log("<color=red>" + id + "  Awake(): MG_FloorMissing не прикреплен!</color>");
        if (floorPropertyConstructor == null)
            Debug.Log("<color=red>" + id + "  Awake(): MG_FloorPropertyConstructor не прикреплен!</color>");
        if (floorXML_data == null)
            Debug.Log("<color=red>" + id + "  Awake(): MG_FloorXML_data не прикреплен!</color>");
        if (xmlNodeManager == null)
            Debug.Log("<color=red>" + id + "  Awake(): MG_XmlLoader не прикреплен!</color>");
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public override void Init(List<XmlDocument> _listXmlDocs)//получить все XML файлы мода)
    {
        List<XmlNodeList> _listOfNodeList = xmlNodeManager.GetListOfXmlNodes(xmlNodeName, _listXmlDocs);// получить лист объектов XmlNodeList, которые содержат XML ноды: напр <FLOOR></FLOOR>>
        ProcessXMLData(_listOfNodeList);// Обработать листы объектов XmlNodeList
        floorPropertyConstructor.Init();//даем зеленый свет конструктору, чтобы начал создавать свойства для библиотеки
    }

    /// <summary>
    /// Обработать XmlNode
    /// </summary>
    /// <param name="_node"></param>
    protected override void ProcessXmlNode(XmlNode _node)
    {
        int _count = floorXML_data.Count();//подсчитываем общее кол-во записей

        //---------------ОБРАБАТЫВАЕМ ДАННЫЕ, ЕСЛИ ЧЕГО ТО НЕ ХВАТАЕТ, ТО БУДЕТ ПОДПРАВЛЕНО 
        //1 FloorTile
        string[] _parents = new string[] { };//иерархия родителей
        string[] _attrs = new string[] { "id", "Name", "FloorType" };//свойства главного родителя
        base.xmlNodeManager.ExtractXMLNode(_node, "FloorTile", ref _attrs, true, _parents);//обработать родительский нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _id = ProcessId(_attrs[0], _count);//обработать айди объекта
        string _name = ProcessName(_attrs[1], _count);//обработать наим объекта
        FloorType _floorType = ProcessType(_attrs[2]);//обработать тип объекта

        //1.1 - 1.2  SpriteTop, SpriteIso
        _parents = new string[1] { "FloorTile" };//иерархия родителей для внутренних нодов
  
        //1.1 SpriteIso
        string[] _attrs_1 = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIso
        string _spriteURL_2D = base.xmlNodeManager.ExtractXMLNode(_node, "SpriteTop", ref _attrs_1, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА  
        float _pivotX_2D = MG_FloatConvertor.ProcessFloat(_attrs_1[0]);//обработать пивот
        float _pivotY_2D = MG_FloatConvertor.ProcessFloat(_attrs_1[1]);//обработать пивот
        float _pixelPerU_2D = MG_FloatConvertor.ProcessFloat(_attrs_1[2]);//обработать Pixel Per Unit
        Sprite _sprite2D = ProcessSprite2D(_spriteURL_2D, _pivotX_2D, _pivotY_2D, _pixelPerU_2D);//обработать спрайт

        //1.2 SpriteTop
        string[] _attrs_2 = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteTop
        string _spriteURL_Iso = base.xmlNodeManager.ExtractXMLNode(_node, "SpriteIso", ref _attrs_2, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА   
        float _pivotX_Iso = MG_FloatConvertor.ProcessFloat(_attrs_2[0]);//обработать пивот
        float _pivotY_Iso = MG_FloatConvertor.ProcessFloat(_attrs_2[1]);//обработать пивот
        float _pixelPerU_Iso = MG_FloatConvertor.ProcessFloat(_attrs_2[2]);//обработать Pixel Per Unit
        Sprite _spriteIso = ProcessSpriteIso(_spriteURL_Iso, _pivotX_Iso, _pivotY_Iso, _pixelPerU_Iso);//обработать спрайт

        //---------ЗАПОЛНЯЕМ ЛИСТЫ, ДЛЯ ВСЕХ УЖЕ ЗАГОТОВЛЕНЫ ГОТОВЫЕ ДАННЫЕ!

        //1 FloorTile
        floorXML_data.AddID(_id);
        floorXML_data.AddName(_name);
        floorXML_data.AddFloorType(_floorType);

        //1.1 - 1.2 SpriteTop SpriteIso
        floorXML_data.AddSprite(_sprite2D, _spriteIso);
        floorXML_data.AddPivot(_pivotX_2D, _pivotY_2D, _pivotX_Iso, _pivotY_Iso);
        floorXML_data.AddPixelPerU(_pixelPerU_2D, _pixelPerU_Iso);
    }

    /// <summary>
    /// Обработать Спрайт Isometric
    /// </summary>
    /// <param name="_file"></param>
    /// <returns></returns>
    private Sprite ProcessSpriteIso(string _file, float _pivotX, float _pivotY, float _pixelPerU)
    {
        Sprite _spriteIso;
        if (_file == null)
            _spriteIso = floorMissing.GetSpriteIso();
        else
        {
            string _fileUrl = modManager.GetModDirectory() + "/" + _file;
            _spriteIso = spriteLoader.LoadSprite(_fileUrl, _pivotX, _pivotY, _pixelPerU);
            if (_spriteIso == null)
            {
                _spriteIso = floorMissing.GetSpriteIso();
            }
        }
        return _spriteIso;
    }

    /// <summary>
    /// Обработать Спрайт 2D Top Down
    /// </summary>
    /// <param name="_file"></param>
    /// <returns></returns>
    private Sprite ProcessSprite2D(string _file, float _pivotX, float _pivotY, float _pixelPerU)
    {
        Sprite _sprite2D;
        if (_file == null)
            _sprite2D = floorMissing.GetSprite2D();
        else
        {
            string _fileUrl = modManager.GetModDirectory() + "/" + _file;
            _sprite2D = spriteLoader.LoadSprite(_fileUrl, _pivotX, _pivotY, _pixelPerU);
            if (_sprite2D == null)
            {
                _sprite2D = floorMissing.GetSprite2D();
            }
        }
        return _sprite2D;
    }

    /// <summary>
    /// Обработать тип пола
    /// </summary>
    /// <param name="_typeStr"></param>
    /// <returns></returns>
    private FloorType ProcessType(string _typeStr)
    {
        FloorType _type;
        if (_typeStr == null)
            _type = FloorType.Carpet;
        else
        {
            bool _result = Enum.TryParse(_typeStr, out _type);
            if (!_result) _type = FloorType.Carpet;
        }
        return _type;
    }
}
