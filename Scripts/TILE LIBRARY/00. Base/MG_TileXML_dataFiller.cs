using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;

public class MG_TileXML_dataFiller : MonoBehaviour
{
    [SerializeField]
    protected string id = "<MG_TileXML_dataFiller>";
    [SerializeField]
    protected MG_XmlNodeManager xmlNodeManager;//[R]
    [SerializeField]
    protected MG_ModManager modManager;//[R]
    [SerializeField]
    protected MG_TileMissing tileMissing;//[R] для получения 'оранжевых' шаблонов
    [SerializeField]
    protected MG_SpriteLoader spriteLoader;//[R] для загрузки спрайтов
    [SerializeField]
    protected string xmlNodeName = "<EMPTY>";//тэг для XML по которому нужно искать

    private void Awake()
    {
        if (modManager == null)
            Debug.Log("<color=red>" + id + " Awake(): MG_ModManager не прикреплен!</color>");
        if (xmlNodeManager == null)
            Debug.Log("<color=red>" + id + "  Awake(): MG_XmlNodeManager не прикреплен!</color>");
        if (tileMissing == null)
            Debug.Log("<color=red>" + id + "  Awake(): MG_TileMissing не прикреплен!</color>");
        if (spriteLoader == null)
            Debug.Log("<color=red>" + id + "  Awake(): MG_SpriteLoader не прикреплен!</color>");
        //if (floorPropertyConstructor == null)
        //    Debug.Log("<color=red>MG_FloorXML_data Awake(): MG_FloorPropertyConstructor не прикреплен!</color>");
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public virtual void Init(List<XmlDocument> _listXmlDocs)
    {
        Debug.Log("<color=red>" + id + "  Init(): МЕТОД НЕ РЕАЛИЗОВАН!</color>");
    }

    /// <summary>
    /// Обработать листы объектов XmlNodeList
    /// </summary>
    /// <param name="_listOfNodeList"></param>
    protected void ProcessXMLData(List<XmlNodeList> _listOfNodeList)
    {
        foreach (XmlNodeList _nodeList in _listOfNodeList)
        {
            foreach (XmlNode _node in _nodeList)
            {
                ProcessXmlNode(_node);
            }
        }
    }

    /// <summary>
    /// Обработать XmlNode
    /// </summary>
    /// <param name="_node"></param>
    protected virtual void ProcessXmlNode(XmlNode _node)
    {
        Debug.Log("<color=red>" + id + "  ProcessXmlNode(): МЕТОД НЕ РЕАЛИЗОВАН!</color>");
    }

    /// <summary>
    /// Обработать ID
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_identity"></param>
    /// <returns></returns>
    protected string ProcessId(string _id, int _identity)
    {
        if (_id == null)
            _id = tileMissing.GetMissingText(_identity, 0);
        return _id;
    }

    /// <summary>
    /// Обработать наименование
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_identity"></param>
    /// <returns></returns>
    protected string ProcessName(string _name, int _identity)
    {
        if (_name == null)
            _name = tileMissing.GetMissingText(_identity, 1);
        return _name;
    }

    ///// <summary>
    ///// Обработать Спрайт Isometric
    ///// </summary>
    ///// <param name="_file"></param>
    ///// <returns></returns>
    //protected virtual Sprite ProcessSpriteIso(string _file)
    //{
    //    Debug.Log("<color=red>" + id + "  ProcessSpriteIso(): МЕТОД НЕ РЕАЛИЗОВАН!</color>");
    //    return null;
    //}

    ///// <summary>
    ///// Обработать Спрайт 2D Top Down
    ///// </summary>
    ///// <param name="_file"></param>
    ///// <returns></returns>
    //protected virtual Sprite ProcessSprite2D(string _file)
    //{
    //    Debug.Log("<color=red>" + id + "  ProcessSprite2D(): МЕТОД НЕ РЕАЛИЗОВАН!</color>");
    //    return null;
    //}

}
