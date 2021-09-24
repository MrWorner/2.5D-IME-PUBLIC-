using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_TileXML_data : MonoBehaviour
{
  

    [SerializeField]
    protected string xmlNodeName = "FloorTile";//тэг для XML по которому нужно искать
    [SerializeField]
    protected List<string> XMLIDList;//ID будущего свойства
    [SerializeField]
    protected List<string> XMLNameList;//Имя будущего свойства



    /// <summary>
    /// Получить лист с ID
    /// </summary>
    /// <returns></returns>
    public List<string> GetIDList()
    {
        return XMLIDList;
    }

    /// <summary>
    /// Добавить ID
    /// </summary>
    /// <param name="_id"></param>
    public void AddID(string _id)
    {
        XMLIDList.Add(_id);
    }

    /// <summary>
    /// Количество уникальных записей
    /// </summary>
    /// <returns></returns>
    public int Count()
    {
        return XMLIDList.Count;
    }

    //-----------------------------------------------

    /// <summary>
    /// Получить лист с Наименованиями
    /// </summary>
    /// <returns></returns>
    public List<string> GetNameList()
    {
        return XMLNameList;
    }

    /// <summary>
    /// Добавить имя в лист
    /// </summary>
    /// <param name="_name"></param>
    public void AddName(string _name)
    {
        XMLNameList.Add(_name);
    }
}
