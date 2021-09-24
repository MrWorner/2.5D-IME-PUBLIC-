using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class MG_XmlNodeManager : MonoBehaviour
{


    //private void Awake()
    //{
    //    if (XmlFileStorage == null)
    //        Debug.Log("<color=red>MG_XmlNodeManager Awake(): MG_XmlFileStorage не прикреплен!</color>");
    //}

    //Node name, attrs, (value - Null (OR) childrens [])  Null, parent
    //"FloorTile",["id","name","type"], false, ["SpriteIso, SpriteTop"], ""
    //"SpriteIso",["pivotX","pivotY","pixelPerU"], true, [], "FloorTile"
    //"SpriteTop",["pivotX","pivotY","pixelPerU"], true, [], "FloorTile"

    /// <summary>
    /// Прочитать XmlNode и вытащить нужные данные по заданным параметрам
    /// </summary>
    /// <param name="_node"></param>
    /// <param name="_nodeName"></param>
    /// <param name="_attrs"></param>
    /// <param name="_getValue"></param>
    /// <param name="_parents"></param>
    /// <returns></returns>
    public string ExtractXMLNode(XmlNode _node, string _nodeName, ref string[] _attrs, bool _getValue, string[] _parents)
    {
  
        if (_parents.Length > 0)//если есть родители, то _node не тот нод, который мы получили, а его первый предок!
        {
            _node = FindXmlNodeChild(_node, _nodeName, _parents);//получаем нужный нод
        };
 
        if (_attrs.Length > 0)//если есть атрибуты, то считывавем
        {
            _attrs = GetAttrs(_node, _attrs);
        }
 
        string value = "";
        if (_getValue)//если есть значение, то считываем
        {
            value = _node.InnerText;
        }
 
        return value;
    }

    /// <summary>
    /// Получить лист листов нодов
    /// </summary>
    /// <param name="_xmlNodeName"></param>
    /// <param name="_docList"></param>
    /// <returns></returns>
    public List<XmlNodeList> GetListOfXmlNodes(string _xmlNodeName, List<XmlDocument> _listOfXmlFiles)//, List<XmlDocument> _docList
    {
        string _url = "/root/" + _xmlNodeName;//полный путь 
        List<XmlNodeList> _list = new List<XmlNodeList>();
        foreach (XmlDocument _XMLfile in _listOfXmlFiles)//каждый загруженный файл
        {
            XmlNodeList _XMLnodes = _XMLfile.DocumentElement.SelectNodes(_url);//берем все ноды //http://csharp.net-informations.com/xml/how-to-read-xml.htm
            _list.Add(_XMLnodes);
        }
        return _list;
    }

    /// <summary>
    /// Получить значения из всех нодов одной группы
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="url"></param>
    /// <param name="_parents"></param>
    /// <returns></returns>
    public List<string> GetNodesValue(XmlDocument xml, string url)
    {
        List<string> _listValues = new List<string>();
        XmlNodeList _nodeList = xml.SelectNodes(url);//"/Names/Name"
        foreach (XmlNode _node in _nodeList)
        {        
            _listValues.Add(_node.InnerText);
        }
        return _listValues;
    }


    /// <summary>
    /// Найти дочерний нод
    /// </summary>
    /// <param name="_node"></param>
    /// <param name="_nodeName"></param>
    /// <param name="_parents"></param>
    /// <returns></returns>
    private XmlNode FindXmlNodeChild(XmlNode _node, string _nodeName, string[] _parents)
    {
        string _parent = _parents[0];//получаем старейшего родителя (по убыванию, старое поколение к новому)
        _parents = _parents.Where(val => val != _parent).ToArray();//удаляем выбранный элемент с массива
        if (!_parent.Equals(_node.Name))//если родитель не главный нод
        {
            _node = FindXmlNodeChild(_node, _nodeName, _parents);//получаем нужный нод
        }
        XmlNode _childNode = _node.SelectSingleNode(_nodeName);//получаем XmlNode ребенка у XmlNode родителя
        return _childNode;
    }

    /// <summary>
    /// Получить значение атрибутов нода
    /// </summary>
    /// <param name="_node"></param>
    /// <param name="_attrs"></param>
    /// <returns></returns>
    private string[] GetAttrs(XmlNode _node, string[] _attrs)
    {
        string[] _result = new string[_attrs.Length];//создаем массив результата c размером полученных атрибутов
        foreach (XmlAttribute _attribute in _node.Attributes)
        {
            string _name = _attribute.Name;//имя атрибута
            for (int i = 0; i < _attrs.Length; i++)
            {
                if (_name.Equals(_attrs[i]))//если находим нужный атрибут
                {
                    _result[i] = _attribute.Value;//записываем атрибут на нужную позицию результата
                    break;
                }
            }
            var _attr = _attribute.Value;//БЕРЕМ атрибуты из XML нода, НАПРИМЕР: id, pivotX, pivotY  <FloorTile id="MyFloor" pivotX ="0.5" pivotY="0.13">
        }
        return _result;//Возвращаем массив с результатом. (не найденные ячейки по сути будут содержать Null)
    }
}
