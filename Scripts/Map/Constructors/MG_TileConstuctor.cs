using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_TileConstuctor : MonoBehaviour
{
    private static MG_TileConstuctor instance;//в редакторе только один объект должен быть создан
    ///[SerializeField]
    ///protected TextMeshPro prefab_Label;  //Заранее используемый шаблон объекта Text (TextMeshProUGUI) ДЛЯ СОЗДАНИЯ

    [SerializeField]
    private MG_PathConstructor pathConstructor;//[R] конструктор нодов
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MG_TileConstuctor Awake(): MG_TileConstuctor может быть только один компонент на Сцене, другие не нужны.</color>");
        if (editor == null)
            Debug.Log("<color=red>MG_FloorConstructor Awake(): MG_Editor не прикреплен!</color>");
        if (pathConstructor == null)
            Debug.Log("<color=red>MG_FloorConstructor Awake(): MG_PathConstructor не прикреплен!</color>");
    }

    /// <summary>
    /// Создание тайла
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_color"></param>
    /// <param name="_map"></param>
    /// <returns></returns>
    public MG_Tile CreateMgTile(Vector3Int _pos, Color _color, MG_TileMap _map, int _floorInt)
    {
        //--------------if (dictionary_MG_tiles == null) return null;
        //if (map.HasTile(_pos))//На всякий случай, если тайл действительно существует
        Tilemap tileMap = _map.GetFloorMap(_floorInt);//Карта этажа
        Vector3 worldPos = tileMap.CellToWorld(_pos);//переводим в Vector3
        int _x = _pos.x;
        int _y = _pos.y;

        //if (_map.HasMgTile(_pos))//Если в справочнике отсутствует данный тайл, то заносим
        //{
        //    _mg_tile = _map.GetMgTile(_pos);
        //    //Destroy(_mg_tile.Node.gameObject);
        //    _mg_tile.NodePro.Destroy();
        //    _map.dictionary_MG_tiles.Remove(_pos);
        //}

        MG_Tile _mg_tile = new MG_Tile
        {
            Pos = _pos,
            WorldLocation = worldPos,
            //-----------Tile = map.GetTile<Tile>(_pos),
            Map = _map,
            Name = _x + "," + _y,
            Color = _color
        };
        //-----------mg_tile.Node.Current_compCell = mg_tile;
        //--------tileArray[_x, _y] = mg_tile;

        _map.AddMgTile(_pos, _mg_tile);
        //if (_map.addLabel)
        //{
        //    //BEGIN Метка координатная на клетке
        //    TextMeshPro label = Instantiate(prefab_Label); //Создание самой метки
        //    label.rectTransform.SetParent(_map.labelCanvas.transform, false); //Задаем Parent
        //    label.rectTransform.anchoredPosition = new Vector2(worldPos.x, worldPos.y);//Задаем позицию
        //    if (!_map.emptyTextLabel)
        //        label.text = _x + ", " + _y; //Задаем текст            
        //    label.name = "Label (" + _x + ", " + _y + ")";
        //    _mg_tile.Label = label;
        //}
        MapType _type = _map.GetMapType();
        pathConstructor.InitPointNode(_mg_tile, _type);
        return _mg_tile;
    }
}
