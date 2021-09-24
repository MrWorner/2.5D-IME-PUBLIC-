using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MG_XmlFileStorage : MonoBehaviour
{
    [SerializeField]
    private List<string> Loaded;//[D] Список имен загруженных XML файлов
    private List<XmlDocument> XMLFileStorage = new List<XmlDocument>();//Все целиком загруженные XML файлы в память

    /// <summary>
    /// Добавить файл в Storage
    /// </summary>
    /// <param name="_doc"></param>
    public void AddXmlFile(XmlDocument _doc, string _name)
    {
        XMLFileStorage.Add(_doc);
        Loaded.Add(_name);
    }

    /// <summary>
    /// Получить Storage
    /// </summary>
    /// <returns></returns>
    public List<XmlDocument> GetStorage()
    {
        return XMLFileStorage;
    }
}


