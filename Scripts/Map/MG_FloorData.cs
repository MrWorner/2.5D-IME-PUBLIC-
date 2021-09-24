using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_FloorData : MonoBehaviour
{
    [SerializeField]
    private int floorNum = -999;//[R] номер этажа
    [SerializeField]
    private Tilemap floorTileMap;//[R] карта пола
    [SerializeField]
    private Tilemap wallTileMap_H;//[R] карта стен по горизонтали
    [SerializeField]
    private Tilemap wallTileMap_V;//[R] карта стен по вертикали
    [SerializeField]
    private Tilemap propTileMap;//[R] карта объектов

    // Получить карту пола
    public Tilemap FloorTileMap { get => floorTileMap; }
    // Получить карту стен Horizontal
    public Tilemap WallTileMap_H { get => wallTileMap_H; }
    // Получить карту стен Vertical
    public Tilemap WallTileMap_V { get => wallTileMap_V; }
    // Получить карту Props
    public Tilemap PropTileMap { get => propTileMap; }
    /// Получить номер этажа
    public int FloorNum { get => floorNum; }

    //[SerializeField]
    //private Canvas labelCanvas; //Заранее используемый Объект Canvas, для хранения меток для карты префаба TextLabel

    private void Awake()
    {
        if (floorNum == (-999))
            Debug.Log("<color=red>MG_TileMap Awake(): объект для floorNum не прикреплен!</color>");
        if (floorTileMap == null)
            Debug.Log("<color=red>MG_TileMap Awake(): объект для floorTileMap не прикреплен!</color>");
        if (wallTileMap_H == null)
            Debug.Log("<color=red>MG_TileMap Awake(): объект для wallTileMap_H не прикреплен!</color>");
        if (wallTileMap_V == null)
            Debug.Log("<color=red>MG_TileMap Awake(): объект для wallTileMap_V не прикреплен!</color>");
        if (propTileMap == null)
            Debug.Log("<color=red>MG_TileMap Awake(): объект для propTileMap не прикреплен!</color>");
    }

    /// <summary>
    /// Установить номер этажа
    /// </summary>
    /// <param name="_num"></param>
    public void SetFloorNum(int _num)
    {
        floorNum = _num;
        this.gameObject.name = "Floor: " + _num;
    }


    //----------------------------------------------------------------

    /// <summary>
    /// Очистить все от тайлов
    /// </summary>
    public void RemoveAllTiles()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap_H.ClearAllTiles();
        wallTileMap_V.ClearAllTiles();
        propTileMap.ClearAllTiles();
    }

}
