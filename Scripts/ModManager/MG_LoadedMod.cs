using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MG_LoadedMod : MonoBehaviour
{
    [SerializeField]
    private string modName = "<NOT SET>";//название мода
    [SerializeField]
    private string modVersion = "<NOT SET>";//версия мода
    [SerializeField]
    private float reqGameVersion = 0.1f;//необходимая версия игры
    [SerializeField]
    private string description = "<NOT SET>";//описание мода
    [SerializeField]
    private string author = "<NOT SET>";//автор мода
    [SerializeField]
    private string website = "<NOT SET>";//вебсайт создателя
    [SerializeField]
    private string tags = "<NOT SET>";//вебсайт создателя
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string directoryUrl = "<NOT SET>";//путь к моду
    [SerializeField]
    private List<string> fileNames;//имена файлов

    [SerializeField]
    private bool TextureMod;
    [SerializeField]
    private bool SoundMod;
    [SerializeField]
    private bool AIMod;
    [SerializeField]
    private bool NewEquipmentMod;
    [SerializeField]
    private bool ReBalanceMod;
    [SerializeField]
    private bool NewMapMod;

    //[SerializeField]
    //private List<int> priority;//выставленный приоритет мода
    //private List<XmlDocument> xmlFileStorage = new List<XmlDocument>();//Все целиком загруженные XML файлы в память

    public string DirectoryUrl { get => directoryUrl; set => directoryUrl = value; }

    //public List<int> Priority { get => priority; set => priority = value; }
    //public List<XmlDocument> XMLFileStorage => xmlFileStorage = new List<XmlDocument>();

    /// <summary>
    /// Заполнить информацию о моде
    /// </summary>
    /// <param name="_modName"></param>
    /// <param name="_modVersion"></param>
    /// <param name="_reqGameVersion"></param>
    /// <param name="_description"></param>
    /// <param name="_author"></param>
    /// <param name="_website"></param>
    /// <param name="_tags"></param>
    public void FillDescritpion(string _modName, string _modVersion, float _reqGameVersion, string _description, string _author, string _website, string _tags)
    {
        modName = _modName;
        modVersion = _modVersion;
        reqGameVersion = _reqGameVersion;
        description = _description;
        author = _author;
        website = _website;
        tags = _tags;
        //directoryUrl = DirectoryUrl;   
    }

    /// <summary>
    /// Заполнить информацию о моде II
    /// </summary>
    /// <param name="_textureMod"></param>
    /// <param name="_soundMod"></param>
    /// <param name="_AIMod"></param>
    /// <param name="_newEquipmentMod"></param>
    /// <param name="_reBalanceMod"></param>
    /// <param name="_newMapMod"></param>
    public void FillBoolDescription(bool _textureMod, bool _soundMod, bool _AIMod, bool _newEquipmentMod, bool _reBalanceMod, bool _newMapMod)
    {
        TextureMod = _textureMod;
        SoundMod = _soundMod;
        AIMod = _AIMod;
        NewEquipmentMod = _newEquipmentMod;
        ReBalanceMod = _reBalanceMod;
        NewMapMod = _newMapMod;
    }

    /// <summary>
    /// Добавить XML файл
    /// </summary>
    /// <param name="_doc"></param>
    /// <param name="_fileName"></param>
    public void AddXmlFileName(string _fileName)
    {
        //xmlFileStorage.Add(_doc);
        fileNames.Add(_fileName);
    }

    /// <summary>
    /// Установить иконку мода
    /// </summary>
    /// <param name="_icon"></param>
    public void AddIcon(Sprite _icon)
    {
        icon = _icon;
    }
}