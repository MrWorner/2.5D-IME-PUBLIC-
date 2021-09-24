using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PropDefault_data : MonoBehaviour
{
    [SerializeField]
    private int TotalCountPropTile;
    //Заполняется самим разработчиком в редакторе, а не во время запуска!
    [SerializeField]
    private List<string> IDList;
    [SerializeField]
    private List<string> NameList;
    [SerializeField]
    private List<Sprite> SpriteIsoList_N;
    [SerializeField]
    private List<Sprite> SpriteIsoList_E;
    [SerializeField]
    private List<Sprite> SpriteIsoList_S;
    [SerializeField]
    private List<Sprite> SpriteIsoList_W;
    [SerializeField]
    private List<Sprite> Sprite2DList_N;
    [SerializeField]
    private List<Sprite> Sprite2DList_E;
    [SerializeField]
    private List<Sprite> Sprite2DList_S;
    [SerializeField]
    private List<Sprite> Sprite2DList_W;
    [SerializeField]
    private List<PropSize> PropSizeList;

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
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): MG_FloorDefault_data НЕ ЗАПОЛНЕН РАЗРАБОТЧИКОМ!</color>");
        if (TotalCountPropTile != NameList.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): NameList не равен количеству ID</color>");
        if (TotalCountPropTile != SpriteIsoList_N.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): SpriteIsoList_N не равен количеству ID</color>");
        if (TotalCountPropTile != SpriteIsoList_E.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): SpriteIsoList_E не равен количеству ID</color>");
        if (TotalCountPropTile != SpriteIsoList_S.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): SpriteIsoList_S не равен количеству ID</color>");
        if (TotalCountPropTile != SpriteIsoList_W.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): SpriteIsoList_W не равен количеству ID</color>");
        if (TotalCountPropTile != Sprite2DList_N.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): Sprite2DList_N не равен количеству ID</color>");
        if (TotalCountPropTile != Sprite2DList_E.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): Sprite2DList_E не равен количеству ID</color>");
        if (TotalCountPropTile != Sprite2DList_S.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): Sprite2DList_S не равен количеству ID</color>");
        if (TotalCountPropTile != Sprite2DList_W.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): Sprite2DList_W не равен количеству ID</color>");
        if (TotalCountPropTile != PropSizeList.Count)
            Debug.Log("<color=red>MG_PropDefault_data CheckData(): PropSizeList не равен количеству ID</color>");
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
    /// <param name="_dir"></param>
    /// <returns></returns>
    public List<Sprite> GetSpriteIsoList(Direction _dir)
    {
        switch (_dir)
        {
            case Direction.North:
                return SpriteIsoList_N;
            case Direction.East:
                return SpriteIsoList_E;
            case Direction.South:
                return SpriteIsoList_S;
            case Direction.West:
                return SpriteIsoList_W;
            default:
                Debug.Log("MG_PropXML_data GetSpriteIsoList(): нереализованное направление " + _dir);
                return null;
        }
    }

    /// <summary>
    /// Получить лист со спрайтами 2D Top Down
    /// </summary>
    /// <param name="_dir"></param>
    /// <returns></returns>
    public List<Sprite> GetSprite2DList(Direction _dir)
    {
        switch (_dir)
        {
            case Direction.North:
                return Sprite2DList_N;
            case Direction.East:
                return Sprite2DList_E;
            case Direction.South:
                return Sprite2DList_S;
            case Direction.West:
                return Sprite2DList_W;
            default:
                Debug.Log("MG_PropXML_data GetSprite2DList(): нереализованное направление " + _dir);
                return null;
        }
    }

    /// <summary>
    /// Получить размер
    /// </summary>
    /// <returns></returns>
    public List<PropSize> GetPropSizeList()
    {
        return PropSizeList;
    }

}
