using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;

public class MG_ModFiller : MonoBehaviour
{
    [SerializeField]
    private MG_XmlLoader xmlLoader;//[R]
    [SerializeField]
    private MG_XmlNodeManager xmlNodeManager;//[R]
    [SerializeField]
    private MG_ModManager modManager;//[R]
    [SerializeField]
    private MG_SpriteLoader spriteLoader;//[R]

    [SerializeField]
    string urlFile = "/root/ModInfo/Files/File";//адрес к именам файлов

    private void Awake()
    {
        if (xmlLoader == null)
            Debug.Log("<color=red> MG_ModFiller Awake(): объект для MG_XmlLoader не прикреплен!</color>");
        if (xmlNodeManager == null)
            Debug.Log("<color=red> MG_ModFiller Awake(): объект для MG_XmlNodeManager не прикреплен!</color>");
        if (modManager == null)
            Debug.Log("<color=red> MG_ModFiller Awake(): объект для MG_ModManager не прикреплен!</color>");
        if (spriteLoader == null)
            Debug.Log("<color=red> MG_ModFiller Awake(): объект для MG_SpriteLoader не прикреплен!</color>");
    }

    /// <summary>
    /// ОБРАБОТАТЬ МОДИФИКАЦИЮ
    /// </summary>
    public void ProcessMod(MG_LoadedMod _mod)
    {
        //-------ИНИЦИАЛИЗИРУЕМ ГЛАВНЫЙ ФАЙЛ МОДА
        ProcessInitFile(_mod);// Обработать Главный файл мода         
    }

    //  <ModInfo TextureMod = "true" SoundMod="false" AIMod="false" EquipMod="false" ReBalanceMod="false" MapMod="false">
    //	<Name>My Mod</Name>
    //	<ModVersion>0.5</ModVersion>
    //	<ReqGameVersion>0.1</ReqGameVersion>
    //	<Description>New textures and more!</Description>
    //	<Author>JackFlash</Author>
    //	<Website>www.MyMod.com</Website>
    //	<Files>
    //		<File>MyAddon.xml</File>
    //	</Files>		
    //  </ModInfo>	

    /// <summary>
    /// Обработать Главный файл мода
    /// </summary>
    /// <param name="_mod"></param>
    private void ProcessInitFile(MG_LoadedMod _mod)
    {
        string _initFileUrl = _mod.DirectoryUrl + @"\" + modManager.ModInitFile;//получаем полную ссылку на главный файл мода
        string _mainTag = modManager.ModMainXMltag;//главный тэг
        XmlDocument _xmlInit = xmlLoader.LoadXMLFile(_initFileUrl);//загружаем файл в переменную
        //List<XmlNodeList> _listOfNodeList = _xmlNodeManager.GetXmlNodes(xmlNodeName);

        XmlNode _XMLnode = _xmlInit.DocumentElement.SelectSingleNode(_mainTag);//получить главный нод
        if (_XMLnode != null)
        {
            SetDescription(_mod, _mainTag, _XMLnode);// Установить описание мода
            SetBoolDescription(_mod, _mainTag, _XMLnode);// Установить описание мода II      
            ProcessAndSetIcon(_mod, _mainTag, _XMLnode);// Установить описание мода II      
            List<XmlDocument> _listXmls = GetXMLFiles(_mod, urlFile, _xmlInit);// Получить файлы мода
            modManager.XMLFileStorage.AddRange(_listXmls);//добавляем все загруженные файлы в менеджер модов 
        }
        else
        {
            Debug.Log("<color=yellow> MG_ModFiller ProcessInitFile(): главный нод не найден!</color>");
        }
    }

    /// <summary>
    /// Установить описание мода
    /// </summary>
    /// <param name="_mod"></param>
    /// <param name="_mainTag"></param>
    /// <param name="_XMLnode"></param>
    private void SetDescription(MG_LoadedMod _mod, string _mainTag, XmlNode _XMLnode)
    {
        //Debug.Log("<color=green> MG_ModFiller SetDescription(): загружаем главный нод!</color>");
        string[] _parents = new string[1] { _mainTag };//иерархия родителей
        string[] _attrs = new string[0] { };//свойства главного родителя

        string _name = xmlNodeManager.ExtractXMLNode(_XMLnode, "Name", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _modVersion = xmlNodeManager.ExtractXMLNode(_XMLnode, "ModVersion", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _reqGameVersion = xmlNodeManager.ExtractXMLNode(_XMLnode, "ReqGameVersion", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _description = xmlNodeManager.ExtractXMLNode(_XMLnode, "Description", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _author = xmlNodeManager.ExtractXMLNode(_XMLnode, "Author", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _website = xmlNodeManager.ExtractXMLNode(_XMLnode, "Website", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        string _tags = xmlNodeManager.ExtractXMLNode(_XMLnode, "Tags", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        
        _mod.FillDescritpion(_name, _modVersion, MG_FloatConvertor.ProcessFloat(_reqGameVersion), _description, _author, _website, _tags);//передаем полученные данные
    }

    /// <summary>
    /// Установить описание мода II
    /// </summary>
    /// <param name="_mod"></param>
    /// <param name="_mainTag"></param>
    /// <param name="_XMLnode"></param>
    private void SetBoolDescription(MG_LoadedMod _mod, string _mainTag, XmlNode _XMLnode)
    {
        string[] _parents = new string[0] { };//иерархия родителей
        string[] _attrs = new string[6] { "TextureMod", "SoundMod", "AIMod", "EquipMod", "ReBalanceMod", "MapMod" };//свойства главного родителя
        xmlNodeManager.ExtractXMLNode(_XMLnode, _mainTag, ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА
        if (_attrs.Length == 6)
        {
            _mod.FillBoolDescription(bool.Parse(_attrs[0]), bool.Parse(_attrs[1]), bool.Parse(_attrs[2]), bool.Parse(_attrs[3]), bool.Parse(_attrs[4]), bool.Parse(_attrs[5]));
        }
        else
        {
            Debug.Log("<color=yellow> MG_ModFiller SetBoolDescription(): ошибка при считывании атрибутов!</color>");
        }
    }

    /// <summary>
    /// Получить файлы мода
    /// </summary>
    /// <param name="_mod"></param>
    /// <param name="_tag"></param>
    /// <param name="_xmlInit"></param>
    private List<XmlDocument> GetXMLFiles(MG_LoadedMod _mod, string _url, XmlDocument _xmlInit)
    {
        List<XmlDocument> _listDocs = new List<XmlDocument>();
        List<string> _fileNames = xmlNodeManager.GetNodesValue(_xmlInit, _url);
        foreach (string _fileName in _fileNames)
        {         
            string _fileURL = _mod.DirectoryUrl + @"\" + _fileName;//склеиваем ссылку на файл
            XmlDocument _doc = xmlLoader.LoadXMLFile(_fileURL);//загрузить XML файл
            _listDocs.Add(_doc);

            modManager.LoadedXMLFileNames.Add(_fileName);
            _mod.AddXmlFileName(_fileName);// Добавить XML файл
        }

        return _listDocs;
    }

    private void ProcessAndSetIcon(MG_LoadedMod _mod, string _mainTag, XmlNode _XMLnode) 
    {
        string[] _parents = new string[1] { _mainTag };//иерархия родителей
        string[] _attrs = new string[0] { };//свойства главного родителя

        string _iconUrl = xmlNodeManager.ExtractXMLNode(_XMLnode, "Icon", ref _attrs, true, _parents);//обработать нод с помощью УНИВЕРСАЛЬНОГО МЕТОДА

        string _initFileUrl = _mod.DirectoryUrl + @"\" + _iconUrl;//получаем полную ссылку на главный файл мода
        Sprite _sprite = spriteLoader.LoadSprite(_initFileUrl, 0.5f, 0.5f, 100);
        _mod.AddIcon(_sprite);
    }
}
