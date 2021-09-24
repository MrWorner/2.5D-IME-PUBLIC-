using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_FloorDefault_data : MonoBehaviour
{
    [SerializeField]
    private int TotalCountPropTile;
    //Заполняется самим разработчиком в редакторе, а не во время запуска!
    [SerializeField]
    private List<string> IDList;
    [SerializeField]
    private List<string> NameList;
    [SerializeField]
    private List<Sprite> SpriteIsoList;
    [SerializeField]
    private List<Sprite> Sprite2DList;
    [SerializeField]
    private List<FloorType> FloorTypeList;

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
            Debug.Log("<color=red>MG_TL_FloorDefault CheckData(): MG_FloorDefault_data НЕ ЗАПОЛНЕН РАЗРАБОТЧИКОМ!</color>");
        if (TotalCountPropTile != NameList.Count)
            Debug.Log("<color=red>MG_TL_FloorDefault CheckData(): NameList не равен количеству ID</color>");
        if (TotalCountPropTile != SpriteIsoList.Count)
            Debug.Log("<color=red>MG_TL_FloorDefault CheckData(): SpriteList не равен количеству ID</color>");
        if (TotalCountPropTile != Sprite2DList.Count)
            Debug.Log("<color=red>MG_TL_FloorDefault CheckData(): Sprite2DList не равен количеству ID</color>");
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
    /// Получить лист со спрайтами Iso
    /// </summary>
    /// <returns></returns>
    public List<Sprite> GetSpriteIsoList()
    {
        return SpriteIsoList;
    }

    /// <summary>
    /// Получить лист со спрайтами 2D Top Down
    /// </summary>
    /// <returns></returns>
    public List<Sprite> GetSprite2DList()
    {
        return Sprite2DList;
    }

    /// <summary>
    /// Получить лист с типами
    /// </summary>
    /// <returns></returns>
    public List<FloorType> GetFloorTypeList()
    {
        return FloorTypeList;
    }
}
