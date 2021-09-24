using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_EntryDefault_data : MonoBehaviour
{
    [SerializeField]
    private int TotalCountPropTile;
    //Заполняется самим разработчиком в редакторе, а не во время запуска!
    [SerializeField]
    private List<string> IDList;
    [SerializeField]
    private List<string> NameList;
    [SerializeField]
    private List<Sprite> SpriteIsoHList;
    [SerializeField]
    private List<Sprite> SpriteIsoVList;
    [SerializeField]
    private List<Sprite> Sprite2DHList;
    [SerializeField]
    private List<Sprite> Sprite2DVList;
    [SerializeField]
    private List<EntryType> EntryTypeList;


    private void Awake()
    {
        CheckData();
    }

    /// <summary>
    /// Проверяем целосность данных разработчика
    /// </summary>
    private void CheckData()//Проверяем что стандартный ассет разработчика в целосности и ничего не забыто
    {
        TotalCountPropTile = IDList.Count;//подсчитываем кол-во айдишников, сколько всего должно быть
        if (TotalCountPropTile == 0)
            Debug.Log("<color=red>MG_EntryDefault_data CheckData(): MG_FloorDefault_data НЕ ЗАПОЛНЕН РАЗРАБОТЧИКОМ!</color>");
        if (TotalCountPropTile != NameList.Count)
            Debug.Log("<color=red>MG_EntryDefault_data CheckData(): NameList не равен количеству ID</color>");
        if (TotalCountPropTile != SpriteIsoHList.Count)
            Debug.Log("<color=red>MG_EntryDefault_data CheckData(): SpriteIsoHList не равен количеству ID</color>");
        if (TotalCountPropTile != SpriteIsoVList.Count)
            Debug.Log("<color=red>MG_EntryDefault_data CheckData(): SpriteIsoVList не равен количеству ID</color>");
        if (TotalCountPropTile != Sprite2DHList.Count)
            Debug.Log("<color=red>MG_EntryDefault_data CheckData(): Sprite2DHList не равен количеству ID</color>");
        if (TotalCountPropTile != Sprite2DVList.Count)
            Debug.Log("<color=red>MG_EntryDefault_data CheckData(): Sprite2DVList не равен количеству ID</color>");
    }

    /// <summary>
    /// Получить лист с ID
    /// </summary>
    /// <returns></returns>
    public List<string> GetIDList()
    {
        return IDList;
    }

    /// <summary>
    /// Получить лист с Наименованиями
    /// </summary>
    /// <returns></returns>
    public List<string> GetNameList()
    {
        return NameList;
    }

    /// <summary>
    /// Получить лист со спрайтами Iso H
    /// </summary>
    /// <returns></returns>
    public List<Sprite> GetSpriteIsoHList()
    {
        return SpriteIsoHList;
    }

    /// <summary>
    /// Получить лист со спрайтами Iso V
    /// </summary>
    /// <returns></returns>
    public List<Sprite> GetSpriteIsoVList()
    {
        return SpriteIsoVList;
    }

    /// <summary>
    /// Получить лист со спрайтами 2D Top Down H
    /// </summary>
    /// <returns></returns>
    public List<Sprite> GetSprite2DHList()
    {
        return Sprite2DHList;
    }

    /// <summary>
    /// Получить лист со спрайтами 2D Top Down V
    /// </summary>
    /// <returns></returns>
    public List<Sprite> GetSprite2DVList()
    {
        return Sprite2DVList;
    }

    /// <summary>
    /// Получить лист с типами
    /// </summary>
    /// <returns></returns>
    public List<EntryType> GetEntryTypeList()
    {
        return EntryTypeList;
    }
}
