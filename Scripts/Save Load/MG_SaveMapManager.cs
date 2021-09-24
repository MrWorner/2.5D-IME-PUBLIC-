using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MG_SaveMapManager : MonoBehaviour
{
    private static MG_SaveMapManager instance;//в редакторе только один объект должен быть создан
    [SerializeField]
    private MGJSON_TileConstructor jsonTileConstructor;//[R]
    [SerializeField]
    private MGSJON_TileDecryptor json_TileDecryptor;//[R]
    //[SerializeField]
    //private MG_Editor editor;//[R] Настройки редактора
    [SerializeField]
    private string dataPath = "SavedMaps/TestMap2019.json";
    [SerializeField]
    private int countSavedTiles;//[D]
    [SerializeField]
    private int counLoadedTiles;//[D]

    
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MG_SaveMapManager Awake(): MG_SaveMapManager может быть только один компонент на Сцене, другие не нужны.</color>");
        //if (editor == null)
        //    Debug.Log("<color=red>MG_SaveMapManager Awake(): MG_Editor не прикреплен!</color>");
        if (jsonTileConstructor == null)
            Debug.Log("<color=red>MG_SaveMapManager Awake(): MGJSON_TileConstructor не прикреплен!</color>");
        if (json_TileDecryptor == null)
            Debug.Log("<color=red>MG_SaveMapManager Awake(): MGSJON_TileDecryptor не прикреплен!</color>");
    }

    /// <summary>
    /// Загрузить карту
    /// </summary>
    public void Load(MG_TileMap _map)
    {
        MGJSON_TileContainer _container = GetContainer(dataPath);//Получить контейнер Тайлов из файла JSON
        foreach (MGJSON_Tile _tile in _container.list)
        {
            json_TileDecryptor.DecryptJsonTile(_tile, _map);
        }
    }

    /// <summary>
    /// Получить контейнер Тайлов из файла JSON
    /// </summary>
    /// <param name="_dataPath"></param>
    /// <returns></returns>
    private MGJSON_TileContainer GetContainer(string _dataPath)
    {
        string _jsonFile = File.ReadAllText(_dataPath);
        MGJSON_TileContainer _container = JsonUtility.FromJson<MGJSON_TileContainer>(_jsonFile);

        counLoadedTiles = _container.GetSize();//получить кол-во сохраненных MG_Tiles
        Debug.Log("MG_LoadMap Load(): LOaded JSON tiles! Total tile count = " + counLoadedTiles);
        return _container;
    }

    /// <summary>
    /// Сохранить карту
    /// </summary>
    public void Save(MG_TileMap _map)
    {
        MGJSON_TileContainer _container = jsonTileConstructor.ConstructContainer(_map);//Обрабатываем информацию и получаем контейнер      

        string json = JsonUtility.ToJson(_container, true);
        StreamWriter sw = File.CreateText(dataPath);
        sw.Close();
        File.WriteAllText(dataPath, json);

        countSavedTiles = _container.GetSize();//получить кол-во сохраненных MG_Tiles
        Debug.Log("MG_SaveMap Save(): SAVED JSON tiles! Total tile count = " + countSavedTiles);
    } 
}
