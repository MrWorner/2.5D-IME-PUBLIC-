using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MG_EntryXML_dataFiller : MG_TileXML_dataFiller
{
    //-------------ПРИМЕР НОДА
    //1.  <EntryTile id = "MyWin" Name="My window" EntryType="Window">
	//1.1	<SpriteIsoH pivotX = "0.5" pivotY="0.13" pixelPerU ="100">XXX.png</SpriteIsoH>
	//1.2	<SpriteIsoV pivotX = "0.5" pivotY="0.13" pixelPerU ="100">XXX.png</SpriteIsoV>
	//1.3	<SpriteTopH pivotX = "0.5" pivotY="0.13" pixelPerU ="50">XXX.png</SpriteTopH>
	//1.4	<SpriteTopV pivotX = "0.5" pivotY="0.13" pixelPerU ="50">XXX.png</SpriteTopV>
	//</EntryTile>

    [SerializeField]
    private MG_EntryXML_data entryXML_data;//[R] компонент MG_XmlLoadFiles, где все файлы загружены
    [SerializeField]
    private MG_EntryMissing entryMissing;//[R] для получения 'оранжевых' шаблонов
    [SerializeField]
    private MG_EntryPropertyConstructor entryPropertyConstructor;//[R] для создания свойств

    private void Awake()
    {
        if (entryXML_data == null)
            Debug.Log("<color=red>" + id + " Awake(): MG_EntryXML_data не прикреплен!</color>");
        if (entryMissing == null)
            Debug.Log("<color=red>" + id + " Awake(): MG_EntryMissing не прикреплен!</color>");
        if (entryPropertyConstructor == null)
            Debug.Log("<color=red>" + id + " Awake(): MG_EntryPropertyConstructor не прикреплен!</color>");
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public override void Init(List<XmlDocument> _listXmlDocs)
    {
        List<XmlNodeList> _listOfNodeList = xmlNodeManager.GetListOfXmlNodes(xmlNodeName, _listXmlDocs);// получить лист объектов XmlNodeList, которые содержат XML ноды: напр <FLOOR></FLOOR>>
        ProcessXMLData(_listOfNodeList);// Обработать листы объектов XmlNodeList
        entryPropertyConstructor.Init();//даем зеленый свет конструктору, чтобы начал создавать свойства для библиотеки
    }

    /// <summary>
    /// Обработать XmlNode
    /// </summary>
    /// <param name="_node"></param>
    protected override void ProcessXmlNode(XmlNode _node)
    {
        int _count = entryXML_data.Count();//подсчитываем общее кол-во записей

        //---------------ОБРАБАТЫВАЕМ ДАННЫЕ, ЕСЛИ ЧЕГО ТО НЕ ХВАТАЕТ, ТО БУДЕТ ПОДПРАВЛЕНО 

        //1 WallTile
        string[] _parents = new string[] { };//иерархия родителей
        string[] _attrs = new string[] { "id", "Name", "EntryType" };//свойства главного родителя
        xmlNodeManager.ExtractXMLNode(_node, "EntryTile", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _id = ProcessId(_attrs[0], _count);//обработать айди объекта
        string _name = ProcessName(_attrs[1], _count);//обработать наим объекта
        EntryType _entryType = ProcessType(_attrs[2]);//обработать тип объекта

        //1.1 - 1.4  SpriteIsoH, SpriteIsoV, SpriteTopH, SpriteTopV
        _parents = new string[1] { "EntryTile" };//иерархия родителей для внутренних нодов
        LineDirection _H = LineDirection.Horizontal;
        LineDirection _V = LineDirection.Vertical;


        //1.1 SpriteTopH
        string[] _attrs_2DH = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIsoH
        string _spriteURL_2DH = xmlNodeManager.ExtractXMLNode(_node, "SpriteTopH", ref _attrs_2DH, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА       
        float _pivotX_2DH = MG_FloatConvertor.ProcessFloat(_attrs_2DH[0]);//обработать пивот
        float _pivotY_2DH = MG_FloatConvertor.ProcessFloat(_attrs_2DH[1]);//обработать пивот
        float _pixelPerU_2DH = MG_FloatConvertor.ProcessFloat(_attrs_2DH[2]);//обработать Pixel Per Unit
        Sprite _sprite_2DH = ProcessSprite2D(_spriteURL_2DH, _pivotX_2DH, _pivotY_2DH, _pixelPerU_2DH, _H, _count, _entryType);//обработать спрайт

        //1.2 SpriteTopV
        string[] _attrs_2DV = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIsoV
        string _spriteURL_2DV = xmlNodeManager.ExtractXMLNode(_node, "SpriteTopV", ref _attrs_2DV, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА     
        float _pivotX_2DV = MG_FloatConvertor.ProcessFloat(_attrs_2DV[0]);//обработать пивот
        float _pivotY_2DV = MG_FloatConvertor.ProcessFloat(_attrs_2DV[1]);//обработать пивот
        float _pixelPerU_2DV = MG_FloatConvertor.ProcessFloat(_attrs_2DV[2]);//обработать Pixel Per Unit
        Sprite _sprite_2DV = ProcessSprite2D(_spriteURL_2DV, _pivotX_2DV, _pivotY_2DV, _pixelPerU_2DV, _V, _count, _entryType);//обработать спрайт

        //1.3 SpriteIsoH
        string[] _attrs_IsoH = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIsoV
        string _spriteURL_IsoH = xmlNodeManager.ExtractXMLNode(_node, "SpriteIsoH", ref _attrs_IsoH, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА      
        float _pivotX_IsoH = MG_FloatConvertor.ProcessFloat(_attrs_IsoH[0]);//обработать пивот
        float _pivotY_IsoH = MG_FloatConvertor.ProcessFloat(_attrs_IsoH[1]);//обработать пивот
        float _pixelPerU_IsoH = MG_FloatConvertor.ProcessFloat(_attrs_IsoH[2]);//обработать Pixel Per Unit
        Sprite _sprite_IsoH = ProcessSpriteIso(_spriteURL_IsoH, _pivotX_IsoH, _pixelPerU_IsoH, _pivotY_IsoH, _H, _count, _entryType);//обработать спрайт

        //1.4 SpriteIsoV
        string[] _attrs_IsoV = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIsoV
        string _spriteURL_IsoV = xmlNodeManager.ExtractXMLNode(_node, "SpriteIsoH", ref _attrs_IsoV, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА    
        float _pivotX_IsoV = MG_FloatConvertor.ProcessFloat(_attrs_IsoV[0]);//обработать пивот
        float _pivotY_IsoV = MG_FloatConvertor.ProcessFloat(_attrs_IsoV[1]);//обработать пивот
        float _pixelPerU_IsoV = MG_FloatConvertor.ProcessFloat(_attrs_IsoV[2]);//обработать Pixel Per Unit
        Sprite _sprite_IsoV = ProcessSpriteIso(_spriteURL_IsoV, _pivotX_IsoV, _pivotY_IsoV, _pixelPerU_IsoV, _V, _count, _entryType);//обработать спрайт

        //---------ЗАПОЛНЯЕМ ЛИСТЫ, ДЛЯ ВСЕХ УЖЕ ЗАГОТОВЛЕНЫ ГОТОВЫЕ ДАННЫЕ!
        //1 EntryTile
        entryXML_data.AddID(_id);
        entryXML_data.AddName(_name);
        entryXML_data.AddEntryType(_entryType);

        //1.1 - 1.2 SpriteTop SpriteIso
        entryXML_data.AddSprite(_sprite_2DH, _sprite_2DV, _sprite_IsoH, _sprite_IsoV);
        entryXML_data.AddPivotH(_pivotX_2DH, _pivotY_2DH, _pivotX_IsoH, _pivotY_IsoH);
        entryXML_data.AddPivotV(_pivotX_2DV, _pivotY_2DV, _pivotX_IsoV, _pivotY_IsoV);
        entryXML_data.AddPixelPerU(_pixelPerU_2DH, _pixelPerU_2DV, _pixelPerU_IsoH, _pixelPerU_IsoV);
    }

    /// <summary>
    /// Обработать Спрайт Isometric
    /// </summary>
    /// <param name="_spriteIsoUrl"></param>
    /// <param name="_lineDir"></param>
    /// <param name="_identity"></param>
    /// <param name="_entryType"></param>
    /// <returns></returns>
    private Sprite ProcessSpriteIso(string _file, float _pivotX, float _pivotY, float _pixelPerU, LineDirection _lineDir, int _identity, EntryType _entryType)
    {
        bool _missing = false;
        Sprite _spriteIso = null;

        if (_file == null)
            _missing = true;
        else
        {
            string _fileUrl = modManager.GetModDirectory() + "/" + _file;
            _spriteIso = spriteLoader.LoadSprite(_fileUrl, _pivotX, _pivotY, _pixelPerU);
            if (_spriteIso == null)
            {
                _missing = true;
            }
        }

        if (_missing)
        {
            switch (_lineDir)
            {
                case LineDirection.Horizontal:
                    _spriteIso = entryMissing.GetSprite(_lineDir, ProjectionType.Isometric2D, _entryType);
                    break;
                case LineDirection.Vertical:
                    _spriteIso = entryMissing.GetSprite(_lineDir, ProjectionType.Isometric2D, _entryType);
                    break;
            }
        }
        return _spriteIso;
    }

    /// <summary>
    /// Обработать Спрайт 2D Top Down
    /// </summary>
    /// <param name="_sprite2DUrl"></param>
    /// <param name="_lineDir"></param>
    /// <param name="_identity"></param>
    /// <param name="_entryType"></param>
    /// <returns></returns>
    private Sprite ProcessSprite2D(string _file, float _pivotX, float _pivotY, float _pixelPerU, LineDirection _lineDir, int _identity, EntryType _entryType)
    {
        bool _missing = false;
        Sprite _sprite2D = null;

        if (_file == null)
            _missing = true;
        else
        {
            string _fileUrl = modManager.GetModDirectory() + "/" + _file;
            _sprite2D = spriteLoader.LoadSprite(_fileUrl, _pivotX, _pivotY, _pixelPerU);
            if (_sprite2D == null)
            {
                _missing = true;
            }
        }
        if (_missing)
        {
            switch (_lineDir)
            {
                case LineDirection.Horizontal:
                    _sprite2D = entryMissing.GetSprite(_lineDir, ProjectionType.TopDown2D, _entryType);
                    break;
                case LineDirection.Vertical:
                    _sprite2D = entryMissing.GetSprite(_lineDir, ProjectionType.TopDown2D, _entryType);
                    break;
            }
        }

        return _sprite2D;
    }

    /// <summary>
    /// Обработать тип стены
    /// </summary>
    /// <param name="_typeStr"></param>
    /// <returns></returns>
    private EntryType ProcessType(string _typeStr)
    {
        EntryType _type;
        if (_typeStr == null)
            _type = EntryType.Door;
        else
        {
            bool _result = Enum.TryParse(_typeStr, out _type);
            if (!_result) _type = EntryType.Door;
        }
        return _type;
    }






}
