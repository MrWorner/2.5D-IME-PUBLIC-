using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGSJON_TileDecryptor : MonoBehaviour
{
    private static MGSJON_TileDecryptor instance;//в редакторе только один объект должен быть создан
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора
    [SerializeField]
    private MG_FloorConstructor floorConstuctor;//[R] конструктор 
    [SerializeField]
    private MG_WallConstructor wallConstuctor;//[R] конструктор 
    [SerializeField]
    private MG_EntryConstructor entryConstuctor;//[R] конструктор 
    [SerializeField]
    private MG_PropConstructor propConstuctor;//[R] конструктор 

    [SerializeField]
    private MG_FloorPropertyLibrary floorPropertyLibrary;//[R] библиотека спрайтов Пола
    [SerializeField]
    private MG_WallPropertyLibrary wallPropertyLibrary;//[R] библиотека спрайтов Стен
    [SerializeField]
    private MG_EntryPropertyLibrary entryPropertyLibrary;//[R] библиотека спрайтов Входа
    [SerializeField]
    private MG_PropPropertyLibrary propPropertyLibrary;//[R] библиотека спрайтов объектов

    [SerializeField]
    private MG_FloorPropertyConstructor floorPropertyConstructor;//[R] 
    [SerializeField]
    private MG_WallPropertyConstructor wallPropertyConstructor;//[R]
    [SerializeField]
    private MG_EntryPropertyConstructor entryPropertConstructor;//[R] 
    [SerializeField]
    private MG_PropPropertyConstructor propPropertyConstructor;//[R] 


    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MGSJON_TileDecryptor может быть только один компонент на Сцене, другие не нужны.</color>");
        if (editor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_Editor не прикреплен!</color>");

        if (floorConstuctor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_FloorConstructor не прикреплен!</color>");
        if (wallConstuctor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_WallConstructor не прикреплен!</color>");
        if (entryConstuctor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_EntryConstructor не прикреплен!</color>");
        if (propConstuctor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_PropConstructor не прикреплен!</color>");

        if (floorPropertyLibrary == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_FloorPropertyLibrary не прикреплен!</color>");
        if (wallPropertyLibrary == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_WallPropertyLibrary не прикреплен!</color>");
        if (entryPropertyLibrary == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_EntryPropertyLibrary не прикреплен!</color>");
        if (propPropertyLibrary == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_PropPropertyLibrary не прикреплен!</color>");
   
        if (floorPropertyConstructor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_FloorPropertyConstructor не прикреплен!</color>");
        if (wallPropertyConstructor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_WallPropertyConstructor не прикреплен!</color>");
        if (entryPropertConstructor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_EntryPropertyConstructor не прикреплен!</color>");
        if (propPropertyConstructor == null)
            Debug.Log("<color=red>MGSJON_TileDecryptor Awake(): MG_PropPropertyConstructor не прикреплен!</color>");
    }

    /// <summary>
    /// Конвертировать MGJSON_Tile в MG_Tile с помощью всех необходимых конструкторов
    /// </summary>
    /// <param name="_jsonTile"></param>
    /// <returns></returns>
    public void DecryptJsonTile(MGJSON_Tile _jsonTile, MG_TileMap _map)
    {
        int _floorN = 0;//!ПОКА ЭТАЖНОСТЬ НЕРЕАЛИЗОВАНО!
        DecryptFloor(_jsonTile, _map, _floorN);//Конвертировать данные для пола
        DecryptWalls(_jsonTile, _map, _floorN);//Конвертировать данные для стен
        DecryptEntries(_jsonTile, _map, _floorN);//Конвертировать данные для входа
        DecryptProps(_jsonTile, _map, _floorN);//Конвертировать данные для объектов
    }

    /// <summary>
    /// Конвертировать данные для пола
    /// </summary>
    /// <param name="_jsonTile"></param>
    private void DecryptFloor(MGJSON_Tile _jsonTile, MG_TileMap _map, int _floorN)
    {
        string _id = _jsonTile.floor;//получить id свойства        
        MG_FloorProperty _property = (MG_FloorProperty)floorPropertyLibrary.FindTileProp(_id);//получить свойство

        if(_property == null)//если свойство не найдено, создать временное свойство
        {
            Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptFloor(): Свойство '" + _id + "' не найдено!</color>");
            FloorType _type = _jsonTile.floorType;//получить тип
            _property = floorPropertyConstructor.ConstructTemp(_id, _type);
        }

        Vector3Int _pos = _jsonTile.pos;//получить позицию
        Color _color = _jsonTile.color;//получить цвет пола (комнаты

        floorConstuctor.Construct(_pos, _property, _color, _map, _floorN);//Выполняем сам метод создания/смена свойств и тектсуры пола
    }

    /// <summary>
    /// Конвертировать данные для стен
    /// </summary>
    /// <param name="_jsonTile"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    private void DecryptWalls(MGJSON_Tile _jsonTile, MG_TileMap _map, int _floorN)
    {
        Vector3Int _pos = _jsonTile.pos;//получить позицию
        WallType _wallTypeN = _jsonTile.wallTypeN;//тип стены
        WallType _wallTypeE = _jsonTile.wallTypeE;//тип стены
        WallType _wallTypeS = _jsonTile.wallTypeS;//тип стены
        WallType _wallTypeW = _jsonTile.wallTypeW;//тип стены

        if (!_wallTypeN.Equals(WallType.None))
        {
            string _id = _jsonTile.wallN;//получить id свойства        
            MG_WallProperty _property = (MG_WallProperty)wallPropertyLibrary.FindTileProp(_id);//получить свойство

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptWalls(): Свойство '" + _id + "' не найдено!</color>");
                _property = wallPropertyConstructor.ConstructTemp(_id, _wallTypeN);
            }

            wallConstuctor.Construct(_pos, _property, Direction.North, _map, _floorN);
        }

        if (!_wallTypeE.Equals(WallType.None))
        {
            string _id = _jsonTile.wallE;//получить id свойства
            MG_WallProperty _property = (MG_WallProperty)wallPropertyLibrary.FindTileProp(_id);//получить свойство

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptWalls(): Свойство '" + _id + "' не найдено!</color>");
                _property = wallPropertyConstructor.ConstructTemp(_id, _wallTypeE);
            }

            wallConstuctor.Construct(_pos, _property, Direction.East, _map, _floorN);
        }

        if (!_wallTypeS.Equals(WallType.None))
        {
            string _id = _jsonTile.wallS;//получить id свойства
            MG_WallProperty _property = (MG_WallProperty)wallPropertyLibrary.FindTileProp(_id);//получить свойство

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptWalls(): Свойство '" + _id + "' не найдено!</color>");
                _property = wallPropertyConstructor.ConstructTemp(_id, _wallTypeS);
            }

            wallConstuctor.Construct(_pos, _property, Direction.South, _map, _floorN);
        }

        if (!_wallTypeW.Equals(WallType.None))
        {
            string _id = _jsonTile.wallW;//получить id свойства
            MG_WallProperty _property = (MG_WallProperty)wallPropertyLibrary.FindTileProp(_id);//получить свойство

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptWalls(): Свойство '" + _id + "' не найдено!</color>");
                _property = wallPropertyConstructor.ConstructTemp(_id, _wallTypeW);
            }

            wallConstuctor.Construct(_pos, _property, Direction.West, _map, _floorN);
        }
    }

    /// <summary>
    /// Конвертировать данные для входа
    /// </summary>
    /// <param name="_jsonTile"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    private void DecryptEntries(MGJSON_Tile _jsonTile, MG_TileMap _map, int _floorN)
    {
        Vector3Int _pos = _jsonTile.pos;//получить позицию
        EntryType _entryTypeN = _jsonTile.entryTypeN;//тип входа
        EntryType _entryTypeE = _jsonTile.entryTypeE;//тип входа
        EntryType _entryTypeS = _jsonTile.entryTypeS;//тип входа
        EntryType _entryTypeW = _jsonTile.entryTypeW;//тип входа

        if (!_entryTypeN.Equals(EntryType.None))
        {
            string _id = _jsonTile.entryN;//получить id свойства        
            MG_EntryProperty _property = (MG_EntryProperty)entryPropertyLibrary.FindTileProp(_id);//получить свойство

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptEntries(): Свойство '" + _id + "' не найдено!</color>");
                _property = entryPropertConstructor.ConstructTemp(_id, _entryTypeN);
            }

            entryConstuctor.Construct(_pos, _property, Direction.North, _map, _floorN);
        }

        if (!_entryTypeE.Equals(EntryType.None))
        {       
            string _id = _jsonTile.entryE;//получить id свойства   
            MG_EntryProperty _property = (MG_EntryProperty)entryPropertyLibrary.FindTileProp(_id);//получить свойство        

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptEntries(): Свойство '" + _id + "' не найдено!</color>");
                _property = entryPropertConstructor.ConstructTemp(_id, _entryTypeE);
            }

            entryConstuctor.Construct(_pos, _property, Direction.East, _map, _floorN);
        }

        if (!_entryTypeS.Equals(EntryType.None))
        {
            string _id = _jsonTile.entryS;//получить id свойства        
            MG_EntryProperty _property = (MG_EntryProperty)entryPropertyLibrary.FindTileProp(_id);//получить свойство

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptEntries(): Свойство '" + _id + "' не найдено!</color>");
                _property = entryPropertConstructor.ConstructTemp(_id, _entryTypeS);
            }

            entryConstuctor.Construct(_pos, _property, Direction.South, _map, _floorN);
        }
        if (!_entryTypeW.Equals(EntryType.None))
        {
            string _id = _jsonTile.entryW;//получить id свойства        
            MG_EntryProperty _property = (MG_EntryProperty)entryPropertyLibrary.FindTileProp(_id);//получить свойство

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptEntries(): Свойство '" + _id + "' не найдено!</color>");
                _property = entryPropertConstructor.ConstructTemp(_id, _entryTypeW);
            }

            entryConstuctor.Construct(_pos, _property, Direction.West, _map, _floorN);
        }
    }

    /// <summary>
    /// Конвертировать данные для объектов
    /// </summary>
    /// <param name="_jsonTile"></param>
    /// <param name="_map"></param>
    /// <param name="_floorN"></param>
    private void DecryptProps(MGJSON_Tile _jsonTile, MG_TileMap _map, int _floorN)
    {
        Vector3Int _pos = _jsonTile.pos;//получить позицию
        Direction _dir = _jsonTile.propDir;//направление объекта
        if (!_dir.Equals(Direction.None))
        {
            string _id = _jsonTile.prop;//получить id свойства    
            MG_PropProperty _property = (MG_PropProperty)propPropertyLibrary.FindTileProp(_id);//получить свойство

            if (_property == null)//если свойство не найдено, создать временное свойство
            {
                Debug.Log("<color=yellow>MGSJON_TileDecryptor DecryptEntries(): Свойство '" + _id + "' не найдено!</color>");
                PropSize _type = _jsonTile.propSize;
                _property = propPropertyConstructor.ConstructTemp(_id, _type);
            }

            propConstuctor.Construct(_pos, _property, _dir, _map, _floorN);
        }
    }

    /// <summary>
    /// Сгенерировать карту
    /// </summary>
    /// <param name="_container"></param>
    /// <param name="_map"></param>
    public void GenerateMap(MGJSON_TileContainer _container, MG_TileMap _map)
    {
        List<MGJSON_Tile> _list = _container.list;
        foreach (MGJSON_Tile _tile in _list)
        {
            DecryptJsonTile(_tile, _map);//Конвертация MGJSON_Tile в MG_Tile
        }
    }
}