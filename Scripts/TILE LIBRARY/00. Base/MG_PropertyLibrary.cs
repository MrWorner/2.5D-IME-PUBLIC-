using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PropertyLibrary : MonoBehaviour
{
    protected Dictionary<string, MG_Property> DictionaryProp = new Dictionary<string, MG_Property>();//словарь всех свойств
    protected List<MG_Property> listProp = new List<MG_Property>();//лист всех свойств
    protected List<MG_Property> propertyTileList = new List<MG_Property>();//лист свойств

    [SerializeField]
    protected string id = "<NOT SET>";//[D] имя данной библиотеки
    [SerializeField]
    protected int totalCountPropTile;//Просто для просмотра. Количество доступных свойств с тайлами

    [SerializeField]
    protected List<string> completedIdList;//[D] Для просмотра
    [SerializeField]
    protected List<string> completedNameList;//[D] Для просмотра

    /// <summary>
    /// Добавить свойство в словарь
    /// </summary>
    /// <param name="_tileProp"></param>
    public void AddPropToDic(MG_Property _tileProp)
    {
        if (!DictionaryProp.ContainsKey(_tileProp.ID))//Не допустить одинаковый id свойств модеров. Защита от падения
        {
            DictionaryProp.Add(_tileProp.ID, _tileProp);//добавляем в сам словарь
            listProp.Add(_tileProp);//добавляем в лист
            completedIdList.Add(_tileProp.ID);//добавляем имя в список. ДЛЯ ПРОСМОТРА
            completedNameList.Add(_tileProp.Name);//добавляем имя в список. ДЛЯ ПРОСМОТРА
            totalCountPropTile++;//счетчик
            FillCustomList(_tileProp);
        }
    }

    /// <summary>
    /// Кастомный метод который запускается после добавления в словарь. В основном используется для заполнения для просмотра тайлов[D]
    /// </summary>
    /// <param name="_tileProp"></param>
    public virtual void FillCustomList(MG_Property _tileProp)
    {
        Debug.Log("<color=red>" + id + "  FillCustomList(): метод нереализован!</color>");
    }


    /// <summary>
    /// Получить словарь свойств
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, MG_Property> GetDictionaryProp()
    {
        return DictionaryProp;
    }

    /// <summary>
    /// Получить лист всех свойств
    /// </summary>
    /// <returns></returns>
    public List<MG_Property> getListProp()
    {
        return listProp;
    }

    /// <summary>
    /// Найти свойство по id
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public MG_Property FindTileProp(string _id)//найти свойство из словаря по id
    {
        if (_id == null)
            _id = "<color=red>" + id + " FindTileProp(): EMPTY ERROR! </color>" + _id;
        DictionaryProp.TryGetValue(_id, out MG_Property _tileProp);//ищем в словаре     

        //Debug.Log("Found = " + _tileProp.Name);
        return _tileProp;
    }
}
