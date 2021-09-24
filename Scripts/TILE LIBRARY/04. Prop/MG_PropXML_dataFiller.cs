using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MG_PropXML_dataFiller : MG_TileXML_dataFiller
{
    //1. <PropTile id = "MyProp" Name="My prop">
    //1.1	<SpriteIsoN pivotX = "0.5" pivotY="0.13" pixelPerU ="100">XXX.png</SpriteIsoN>
    //1.2	<SpriteIsoE pivotX = "0.5" pivotY="0.13" pixelPerU ="100">XXX.png</SpriteIsoE>
    //1.3	<SpriteIsoS pivotX = "0.5" pivotY="0.13" pixelPerU ="100">XXX.png</SpriteIsoS>
    //1.4	<SpriteIsoW pivotX = "0.5" pivotY="0.13" pixelPerU ="100">XXX.png</SpriteIsoW>		
    //1.5	<SpriteTopN pivotX = "0.5" pivotY="0.13" pixelPerU ="50">XXX.png</SpriteTopN>
    //1.6	<SpriteTopE pivotX = "0.5" pivotY="0.13" pixelPerU ="50">XXX.png</SpriteTopE>
    //1.7	<SpriteTopS pivotX = "0.5" pivotY="0.13" pixelPerU ="50">XXX.png</SpriteTopS>
    //1.8	<SpriteTopW pivotX = "0.5" pivotY="0.13" pixelPerU ="50">XXX.png</SpriteTopW>
    //</PropTile>

    [SerializeField]
    private MG_PropXML_data propXML_data;//[R] компонент MG_PropXML_data
    [SerializeField]
    private MG_PropMissing propMissing;//[R] для получения 'оранжевых' шаблонов
    [SerializeField]
    private MG_PropPropertyConstructor propPropertyConstructor;//[R] для создания свойств

    private void Awake()
    {
        if (propXML_data == null)
            Debug.Log("<color=red>" + id + " Awake(): MG_XmlLoadFiles не прикреплен!</color>");
        if (propMissing == null)
            Debug.Log("<color=red>" + id + " Awake(): propMissing не прикреплен!</color>");
        if (propPropertyConstructor == null)
            Debug.Log("<color=red>" + id + " Awake(): MG_PropPropertyConstructor не прикреплен!</color>");
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public override void Init(List<XmlDocument> _listXmlDocs)
    {
        List<XmlNodeList> _listOfNodeList = xmlNodeManager.GetListOfXmlNodes(xmlNodeName, _listXmlDocs);// получить лист объектов XmlNodeList, которые содержат XML ноды: напр <FLOOR></FLOOR>>
        ProcessXMLData(_listOfNodeList);// Обработать листы объектов XmlNodeList
        propPropertyConstructor.Init();//даем зеленый свет конструктору, чтобы начал создавать свойства для библиотеки
    }

    /// <summary>
    /// Обработать XmlNode
    /// </summary>
    /// <param name="_node"></param>
    protected override void ProcessXmlNode(XmlNode _node)
    {
        int _count = propXML_data.Count();//подсчитываем общее кол-во записей

        //---------------ОБРАБАТЫВАЕМ ДАННЫЕ, ЕСЛИ ЧЕГО ТО НЕ ХВАТАЕТ, ТО БУДЕТ ПОДПРАВЛЕНО 

        //1 WallTile
        string[] _parents = new string[] { };//иерархия родителей
        string[] _attrs = new string[] { "id", "Name", "WallType", "size" };//свойства главного родителя
        xmlNodeManager.ExtractXMLNode(_node, "PropTile", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _id = ProcessId(_attrs[0], _count);//обработать айди объекта
        string _name = ProcessName(_attrs[1], _count);//обработать наим объекта
        PropSize _propSize = ProcessSize(_attrs[3]);
        //PropSize _propType = ProcessType(_attrs[2]);//обработать тип объекта

        //1.1 - 1.4  SpriteIsoH, SpriteIsoV, SpriteTopH, SpriteTopV
        _parents = new string[1] { "PropTile" };//иерархия родителей для внутренних нодов

        //1.1 SpriteIsoN
        string[] _attrs_IsoN = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIsoN
        string _spriteURL_IsoN = xmlNodeManager.ExtractXMLNode(_node, "SpriteIsoN", ref _attrs_IsoN, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА       
        float _pivotX_IsoN = MG_FloatConvertor.ProcessFloat(_attrs_IsoN[0]);//обработать пивот
        float _pivotY_IsoN = MG_FloatConvertor.ProcessFloat(_attrs_IsoN[1]);//обработать пивот
        float _pixelPerU_IsoN = MG_FloatConvertor.ProcessFloat(_attrs_IsoN[2]);//обработать Pixel Per Unit 
        //Debug.Log(_pivotX_IsoN + " " + _pivotY_IsoN + " " + _pixelPerU_IsoN);
        Sprite _sprite_IsoN = ProcessSpriteIso(_spriteURL_IsoN, _pivotX_IsoN, _pivotY_IsoN, _pixelPerU_IsoN, _count, Direction.North);//обработать спрайт

        //1.2 SpriteIsoE
        string[] _attrs_IsoE = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIsoE
        string _spriteURL_IsoE = xmlNodeManager.ExtractXMLNode(_node, "SpriteIsoE", ref _attrs_IsoE, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА      
        float _pivotX_IsoE = MG_FloatConvertor.ProcessFloat(_attrs_IsoE[0]);//обработать пивот
        float _pivotY_IsoE = MG_FloatConvertor.ProcessFloat(_attrs_IsoE[1]);//обработать пивот
        float _pixelPerU_IsoE = MG_FloatConvertor.ProcessFloat(_attrs_IsoE[2]);//обработать Pixel Per Unit
        Sprite _sprite_IsoE = ProcessSpriteIso(_spriteURL_IsoE, _pivotX_IsoE, _pivotY_IsoE, _pixelPerU_IsoE, _count, Direction.East);//обработать спрайт

        //1.3 SpriteIsoS
        string[] _attrs_IsoS = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIsoS
        string _spriteURL_IsoS = xmlNodeManager.ExtractXMLNode(_node, "SpriteIsoS", ref _attrs_IsoS, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА        
        float _pivotX_IsoS = MG_FloatConvertor.ProcessFloat(_attrs_IsoS[0]);//обработать пивот
        float _pivotY_IsoS = MG_FloatConvertor.ProcessFloat(_attrs_IsoS[1]);//обработать пивот
        float _pixelPerU_IsoS = MG_FloatConvertor.ProcessFloat(_attrs_IsoS[2]);//обработать Pixel Per Unit
        Sprite _sprite_IsoS = ProcessSpriteIso(_spriteURL_IsoS, _pivotX_IsoS, _pivotY_IsoS, _pixelPerU_IsoS, _count, Direction.South);//обработать спрайт

        //1.4 SpriteIsoW
        string[] _attrs_IsoW = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteIsoS
        string _spriteURL_IsoW = xmlNodeManager.ExtractXMLNode(_node, "SpriteIsoW", ref _attrs_IsoW, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА      
        float _pivotX_IsoW = MG_FloatConvertor.ProcessFloat(_attrs_IsoW[0]);//обработать пивот
        float _pivotY_IsoW = MG_FloatConvertor.ProcessFloat(_attrs_IsoW[1]);//обработать пивот
        float _pixelPerU_IsoW = MG_FloatConvertor.ProcessFloat(_attrs_IsoW[2]);//обработать Pixel Per Unit
        Sprite _sprite_IsoW = ProcessSpriteIso(_spriteURL_IsoW, _pivotX_IsoW, _pivotY_IsoW, _pixelPerU_IsoW, _count, Direction.West);//обработать спрайт

        //1.5 SpriteTopN
        string[] _attrs_2DN = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteTopN
        string _spriteURL_2DN = xmlNodeManager.ExtractXMLNode(_node, "SpriteTopN", ref _attrs_2DN, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА     
        float _pivotX_2DN = MG_FloatConvertor.ProcessFloat(_attrs_2DN[0]);//обработать пивот
        float _pivotY_2DN = MG_FloatConvertor.ProcessFloat(_attrs_2DN[1]);//обработать пивот
        float _pixelPerU_2DN = MG_FloatConvertor.ProcessFloat(_attrs_2DN[2]);//обработать Pixel Per Unit
        Sprite _sprite_2DN = ProcessSprite2D(_spriteURL_2DN, _pivotX_2DN, _pivotY_2DN, _pixelPerU_2DN, _count, Direction.North);//обработать спрайт

        //1.6 SpriteTopE
        string[] _attrs_2DE = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteTopE
        string _spriteURL_2DE = xmlNodeManager.ExtractXMLNode(_node, "SpriteTopE", ref _attrs_2DE, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА   
        float _pivotX_2DE = MG_FloatConvertor.ProcessFloat(_attrs_2DE[0]);//обработать пивот
        float _pivotY_2DE = MG_FloatConvertor.ProcessFloat(_attrs_2DE[1]);//обработать пивот
        float _pixelPerU_2DE = MG_FloatConvertor.ProcessFloat(_attrs_2DE[2]);//обработать Pixel Per Unit
        Sprite _sprite_2DE = ProcessSprite2D(_spriteURL_2DE, _pivotX_2DE, _pivotY_2DE, _pixelPerU_2DE, _count, Direction.East);//обработать спрайт

        //1.7 SpriteTopS
        string[] _attrs_2DS = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteTopS
        string _spriteURL_2DS = xmlNodeManager.ExtractXMLNode(_node, "SpriteTopS", ref _attrs_2DS, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА     
        float _pivotX_2DS = MG_FloatConvertor.ProcessFloat(_attrs_2DS[0]);//обработать пивот
        float _pivotY_2DS = MG_FloatConvertor.ProcessFloat(_attrs_2DS[1]);//обработать пивот
        float _pixelPerU_2DS = MG_FloatConvertor.ProcessFloat(_attrs_2DS[2]);//обработать Pixel Per Unit
        Sprite _sprite_2DS = ProcessSprite2D(_spriteURL_2DS, _pivotX_2DS, _pivotY_2DS, _pixelPerU_2DS, _count, Direction.South);//обработать спрайт

        //1.8 SpriteTopW
        string[] _attrs_2DW = new string[3] { "pivotX", "pivotY", "pixelPerU" };//свойства SpriteTopW
        string _spriteURL_2DW = xmlNodeManager.ExtractXMLNode(_node, "SpriteTopW", ref _attrs_2DW, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА     
        float _pivotX_2DW = MG_FloatConvertor.ProcessFloat(_attrs_2DW[0]);//обработать пивот
        float _pivotY_2DW = MG_FloatConvertor.ProcessFloat(_attrs_2DW[1]);//обработать пивот
        float _pixelPerU_2DW = MG_FloatConvertor.ProcessFloat(_attrs_2DW[2]);//обработать Pixel Per Unit
        Sprite _sprite_2DW = ProcessSprite2D(_spriteURL_2DW, _pivotX_2DW, _pivotY_2DW, _pixelPerU_2DW, _count, Direction.West);//обработать спрайт

        //---------ЗАПОЛНЯЕМ ЛИСТЫ, ДЛЯ ВСЕХ УЖЕ ЗАГОТОВЛЕНЫ ГОТОВЫЕ ДАННЫЕ!
        //1 PropTile
        propXML_data.AddID(_id);
        propXML_data.AddName(_name);
        propXML_data.AddPropSize(_propSize);
        //propXML_data.AddPropSize(_wallType);

        //1.1 - 1.8 SpriteTopN SpriteTopE SpriteTopS SpriteTopW SpriteIsoN SpriteIsoE SpriteIsoS SpriteIsoW
        propXML_data.AddSprite(_sprite_IsoN, _sprite_IsoE, _sprite_IsoS, _sprite_IsoW, ProjectionType.Isometric2D);
        propXML_data.AddSprite(_sprite_2DN, _sprite_2DE, _sprite_2DS, _sprite_2DW, ProjectionType.TopDown2D);

        propXML_data.AddPivot(_pivotX_IsoN, _pivotY_IsoN, Direction.North, ProjectionType.Isometric2D);
        propXML_data.AddPivot(_pivotX_IsoE, _pivotY_IsoE, Direction.East, ProjectionType.Isometric2D);
        propXML_data.AddPivot(_pivotX_IsoS, _pivotY_IsoS, Direction.South, ProjectionType.Isometric2D);
        propXML_data.AddPivot(_pivotX_IsoW, _pivotY_IsoW, Direction.West, ProjectionType.Isometric2D);
        propXML_data.AddPivot(_pivotX_2DN, _pivotY_2DN, Direction.North, ProjectionType.TopDown2D);
        propXML_data.AddPivot(_pivotX_2DE, _pivotY_2DE, Direction.East, ProjectionType.TopDown2D);
        propXML_data.AddPivot(_pivotX_2DS, _pivotY_2DS, Direction.South, ProjectionType.TopDown2D);
        propXML_data.AddPivot(_pivotX_2DW, _pivotY_2DW, Direction.West, ProjectionType.TopDown2D);

        propXML_data.AddPixelPerU(_pixelPerU_IsoN, _pixelPerU_IsoE, _pixelPerU_IsoS, _pixelPerU_IsoW, ProjectionType.Isometric2D);
        propXML_data.AddPixelPerU(_pixelPerU_2DN, _pixelPerU_2DE, _pixelPerU_2DS, _pixelPerU_2DW, ProjectionType.TopDown2D);
    }

    /// <summary>
    /// Обработать Спрайт Isometric
    /// </summary>
    /// <param name="_spriteIsoUrl"></param>
    /// <param name="_identity"></param>
    /// <param name="_dir"></param>
    /// <returns></returns>
    private Sprite ProcessSpriteIso(string _file, float _pivotX, float _pivotY, float _pixelPerU, int _identity, Direction _dir)
    {
        Sprite _spriteIso;
        if (_file == null)
            _spriteIso = propMissing.GetSpriteIso(_dir);
        else
        {
            string _fileUrl = modManager.GetModDirectory() + "/" + _file;
            _spriteIso = spriteLoader.LoadSprite(_fileUrl, _pivotX, _pivotY, _pixelPerU);
            if (_spriteIso == null)
            {
                _spriteIso = propMissing.GetSpriteIso(_dir);
            }
        }
        return _spriteIso;
    }

    /// <summary>
    /// Обработать Спрайт 2D Top Down
    /// </summary>
    /// <param name="_sprite2DUrl"></param>
    /// <param name="_identity"></param>
    /// <param name="_dir"></param>
    /// <returns></returns>
    private Sprite ProcessSprite2D(string _file, float _pivotX, float _pivotY, float _pixelPerU, int _identity, Direction _dir)
    {
        Sprite _sprite2D;
        if (_file == null)
            _sprite2D = propMissing.GetSprite2D(_dir);
        else
        {
            string _fileUrl = modManager.GetModDirectory() + "/" + _file;
            _sprite2D = spriteLoader.LoadSprite(_fileUrl, _pivotX, _pivotY, _pixelPerU);
            if (_sprite2D == null)
            {
                _sprite2D = propMissing.GetSprite2D(_dir);
            }
        }
        return _sprite2D;
    }

    /// <summary>
    /// Получить размер
    /// </summary>
    /// <param name="_typeStr"></param>
    /// <returns></returns>
    private PropSize ProcessSize(string _typeStr)
    {
        PropSize _type;
        if (_typeStr == null)
            _type = PropSize.Big;
        else
        {
            bool _result = Enum.TryParse(_typeStr, out _type);
            if (!_result) _type = PropSize.Big;
        }
        return _type;
    }
}
