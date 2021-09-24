using System.IO;
using UnityEngine;

public class MG_JSON
{

    public static MG_TileContainer tileContainer = new MG_TileContainer();
    public static string dataPath = Path.Combine(Application.dataPath, "MG_tiles2.json");

    public static void Load()
    {
        string json = File.ReadAllText(dataPath);
        tileContainer = JsonUtility.FromJson<MG_TileContainer>(json);
        //Debug.Log("loading tile map SIZE = " + tileContainer.mapSize.x + "x" + tileContainer.mapSize.y);
        //foreach (MG_TileData data in tileContainer.MG_Tiles)
        //{
        //    Debug.Log("loaded: Tile " + data.x + "," + data.y + " map size: " + tileContainer.mapSize.x + "x" + tileContainer.mapSize.y);
        //}

        //OnLoaded();

        //ClearTilesList();
    }

    public static void Save()
    {
        Debug.Log("Saving JSON tiles! Total tile count = " + tileContainer.MG_Tiles.Count);
        string json = JsonUtility.ToJson(tileContainer, false);
        StreamWriter sw = File.CreateText(dataPath);
        sw.Close();
        File.WriteAllText(dataPath, json);

        ClearTilesList();
    }

    //public static void AddTileData(MG_TileData data)
    //{
    //    tileContainer.MG_Tiles.Add(data);
    //}

    public static void ClearTilesList()
    {
        tileContainer.MG_Tiles.Clear();
    }



}
