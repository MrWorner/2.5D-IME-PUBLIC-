using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class MG_ModManager : MonoBehaviour
{
    [SerializeField]
    private int countLoadedMods = 0;
    [SerializeField]
    private string modDirectory = "addons";
    [SerializeField]
    private string modInitFile = "init.xml";
    [SerializeField]
    private string modMainXMltag = "ModInfo";

    [SerializeField]
    private GameObject _modComp;//[R]
    [SerializeField]
    private MG_ModFiller _modFiller;//[R]

    private List<MG_LoadedMod> loadedModList = new List<MG_LoadedMod>();
    private List<XmlDocument> xmlFileStorage = new List<XmlDocument>();
    [SerializeField]
    private List<string> loadedXMLFileNames;//[D]

    public string ModInitFile { get => modInitFile; }
    public string ModMainXMltag { get => modMainXMltag; }
    public List<XmlDocument> XMLFileStorage { get => xmlFileStorage; set => xmlFileStorage = value; }
    public List<string> LoadedXMLFileNames { get => loadedXMLFileNames; }//[D]

    [SerializeField]
    private MG_FloorXML_dataFiller floorXML_dataFiller;//[R] 
    [SerializeField]
    private MG_WallXML_dataFiller wallXML_dataFiller;//[R] 
    [SerializeField]
    private MG_EntryXML_dataFiller entryXML_dataFiller;//[R]
    [SerializeField]
    private MG_PropXML_dataFiller propXML_dataFiller;//[R]

    private void Awake()
    {
        if (floorXML_dataFiller == null)
            Debug.Log("<color=red>MG_ModManager Awake(): MG_FloorXML_dataFiller не прикреплен!</color>");
        if (wallXML_dataFiller == null)
            Debug.Log("<color=red>MG_ModManager Awake(): MG_WallXML_dataFiller не прикреплен!</color>");
        if (entryXML_dataFiller == null)
            Debug.Log("<color=red>MG_ModManager Awake(): MG_EntryXML_dataFiller не прикреплен!</color>");
        if (propXML_dataFiller == null)
            Debug.Log("<color=red>MG_ModManager Awake(): MG_PropXML_dataFiller не прикреплен!</color>");

        if (_modComp == null)
            Debug.Log("<color=red> MG_ModManager Awake(): объект для _modComp не прикреплен!</color>");
        if (_modFiller == null)
            Debug.Log("<color=red> MG_ModManager Awake(): объект для MG_ModFiller не прикреплен!</color>");

        FindMods();//Найти и инициализировать моды
        InitMods();//Инициализировать все модификации

        Init3rdParties();//Запускаем инициализацию других классов
    }

    /// <summary>
    /// Когда все XML файлы загружены, даем 'зеленый цвет' другим классам на работу
    /// </summary>
    private void Init3rdParties()
    {
        floorXML_dataFiller.Init(XMLFileStorage);//Запускаем инициализацию класса MG_FloorXML_data
        wallXML_dataFiller.Init(XMLFileStorage);//Запускаем инициализацию класса MG_WallXML_data
        entryXML_dataFiller.Init(XMLFileStorage);//Запускаем инициализацию класса MG_EntryXML_data
        propXML_dataFiller.Init(XMLFileStorage);//Запускаем инициализацию класса MG_EntryXML_data
    }

    /// <summary>
    /// Получить главную директорию модов
    /// </summary>
    /// <returns></returns>
    public string GetModDirectory()
    {
        return modDirectory;
    }
    /// <summary>
    /// Найти и инициализировать моды
    /// </summary>
    private void FindMods()
    {
        DirectoryInfo _mainDir = new DirectoryInfo(@modDirectory);//получаем папку директории с ссылки
        var _dirs = _mainDir.GetDirectories();//получаем все директории у главной директории

        foreach (var _dir in _dirs)
        {
            FileInfo[] _Files = _dir.GetFiles("*.xml");//получаем все файлы с расширением XML      
            foreach (var _file in _Files)
            {
                string _name = _file.Name.ToLower();
                if (_name.Equals(ModInitFile))
                {
                    GameObject _modCompObj = Instantiate(_modComp);//создаем объект
                    _modCompObj.transform.SetParent(this.transform, false); //Задаем Parent
                    MG_LoadedMod _loadedMod = _modCompObj.GetComponent<MG_LoadedMod>();//получить компонент                  
                    loadedModList.Add(_loadedMod);//добавить в список
                    _loadedMod.DirectoryUrl = _dir.FullName;//привязываем полный путь к моду директории

                    countLoadedMods++;
                    _modCompObj.name = countLoadedMods + ": " + _dir.Name;//переименовываем GameObject
                }
            }
        }
    }


    /// <summary>
    /// Инициализировать все модификации
    /// </summary>
    private void InitMods()
    {
        foreach (MG_LoadedMod _mod in loadedModList)
        {
            _modFiller.ProcessMod(_mod);
        }
    }


}
