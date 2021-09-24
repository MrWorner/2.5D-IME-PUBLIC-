using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
//https://gamedev.stackexchange.com/questions/146104/is-it-possible-to-not-pack-asset-files-into-archives-when-building-a-unity-game
//https://answers.unity.com/questions/990637/loading-in-sprites-outside-of-unity.html
//https://coffeebraingames.wordpress.com/2017/09/26/unity-game-considerations-for-modding-support/
//https://www.raywenderlich.com/479-using-streaming-assets-in-unity

public class MG_XmlLoader : MonoBehaviour
{
    [SerializeField]
    private int countLoadedXmlFiles = 0;//[D] Для разработчика, сколько загружено

    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="_fullAddressFile"></param>
    /// <returns></returns>
    public XmlDocument LoadXMLFile(string _fullAddressFile)
    {
        XmlDocument _xmlDoc = new XmlDocument();//создаем XML док  
        _xmlDoc.Load(_fullAddressFile);//загружаем XML файл в память
        countLoadedXmlFiles++;
        return _xmlDoc;
    }


}
