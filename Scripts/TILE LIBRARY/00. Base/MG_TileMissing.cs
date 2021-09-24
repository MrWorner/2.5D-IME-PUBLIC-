using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_TileMissing : MonoBehaviour
{
    protected int identity = -1;// идентификатор для генерации числа
    [SerializeField]
    protected string MissingIdTag = "missingID_";
    [SerializeField]
    protected string MissingТNameTag = "missingName_";
    [SerializeField]
    protected int totalMissing = 0;//кол-во отсутствующих

    /// <summary>
    /// Получить особый текст missing для неполноценных свойств
    /// </summary>
    /// <param name="_itemId"></param>
    /// <param name="_num"></param>
    /// <returns></returns>
    public string GetMissingText(int _itemId, byte _num)
    {
        if (_itemId != identity)
        {
            totalMissing++;
            identity = _itemId;
        }

        switch (_num)
        {
            case 0:
                {
                    return (MissingIdTag + totalMissing);
                }
            case 1:
                {
                    return (MissingТNameTag + totalMissing);
                }
            default:
                return ("UNKNOWN_" + totalMissing);
        }
    }
}
