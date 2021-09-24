using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_MapRotator : MonoBehaviour
{
    //[SerializeField]
    //private MG_Editor editor;//[R] редактор

    [SerializeField]
    private MGJSON_TileConstructor json_TileConstructor;//[R]
    [SerializeField]
    private MGSJON_TileDecryptor json_TileDecryptor;//[R]

    [SerializeField]
    private int sizeX = 0;
    [SerializeField]
    private int sizeY = 0;
    [SerializeField]
    private int emptyCells;
    [SerializeField]
    private int amountCells;

    private List<Vector3Int> PosList;

    private int XMaxF = 0;
    private int YMaxF = 0;
    private int XMinF = 0;
    private int YMinF = 0;


    private void Awake()
    {
        //if (editor == null)
        //    Debug.Log("<color=red>MG_MapRotator Awake(): MG_Editor не прикреплен!</color>");
        if (json_TileConstructor == null)
            Debug.Log("<color=red>MG_MapRotator Awake(): MGJSON_TileConstructor не прикреплен!</color>");
        if (json_TileDecryptor == null)
            Debug.Log("<color=red>MG_MapRotator Awake(): MGSJON_TileDecryptor не прикреплен!</color>");
    }


    /// <summary>
    /// Повернуть карту по часовой или против часовой
    /// </summary>
    /// <param name="_clockwise"></param>
    /// <param name="_map"></param>
    public void RotateMap(MG_TileMap _map, bool _clockwise)
    {
        CalculateMapSize(_map);
        MG_Tile[,]  _mgTileArray = Create2DArray(PosList, _map);
        if (_clockwise)
        {
            _mgTileArray = RotateArrayClockwise(_mgTileArray);// Перевернуть массив по часовой
            RotateTilesCW(_mgTileArray);// Повернуть тайлы по часовой
        }
        else
        {
            _mgTileArray = RotateArrayCounterClockwise(_mgTileArray);// Перевернуть массив против часовой
            RotateTilesCCW(_mgTileArray);// Повернуть тайлы против часовой
        }
        MGJSON_TileContainer _container = json_TileConstructor.ConstructContainer(_map);//Сконструировать контейнер
        _map.ClearMap();//Очистить карту
        json_TileDecryptor.GenerateMap(_container,  _map);//Сгенерировать карту
    }


    /// <summary>
    /// Подсчитываем размер по одной из запутанных формул
    /// </summary>
    /// <param name="_map"></param>
    [Button(ButtonSizes.Large)]
    private void CalculateMapSize(MG_TileMap _map)
    {
        bool _emtpy = true;
        int _xMax;
        int _yMax;
        int _xMin;
        int _yMin;

        PosList = new List<Vector3Int>();
        List<MG_Tile> _mgTileList = _map.GetAllMgTiles();

        foreach (MG_Tile _mgTile in _mgTileList)
        {
            Vector3Int _pos = _mgTile.Pos;
            PosList.Add(_pos);
        }


        foreach (Vector3Int _pos in PosList)
        {
            _xMax = _pos.x;
            _xMin = _xMax;
            _yMax = _pos.y;
            _yMin = _yMax;

            foreach (Vector3Int _pos2 in PosList)
            {
                int _xTile = _pos2.x;
                int _yTile = _pos2.y;

                SetMinOrMax(ref _xMax, _xTile, false);
                SetMinOrMax(ref _yMax, _yTile, false);
                SetMinOrMax(ref _xMin, _xTile, true);
                SetMinOrMax(ref _yMin, _yTile, true);
            }

            if (_emtpy)
            {
                XMaxF = _xMax;
                YMaxF = _yMax;
                XMinF = _xMin;
                YMinF = _yMin;
                _emtpy = false;
            }
            else
            {
                SetMinOrMax(ref XMaxF, _xMax, false);
                SetMinOrMax(ref YMaxF, _yMax, false);
                SetMinOrMax(ref XMinF, _xMin, true);
                SetMinOrMax(ref YMinF, _yMin, true);
            }
        }

        sizeX = Math.Abs(XMaxF - XMinF) + 1;
        sizeY = Math.Abs(YMaxF - YMinF) + 1;
        emptyCells = (sizeX * sizeY - PosList.Count);
        amountCells = PosList.Count;
    }

    /// <summary>
    /// Создаем 2Д массив клеток
    /// </summary>
    /// <param name="_posList"></param>
    /// <param name="_map"></param>
    /// <returns></returns>
    private MG_Tile[,] Create2DArray(List<Vector3Int> _posList, MG_TileMap _map)
    {
        MG_Tile[,] _mgTileArray = new MG_Tile[sizeX, sizeY];
        for (int _i = 0; _i < sizeX; _i++)
        {
            for (int _ii = 0; _ii < sizeY; _ii++)
            {
                foreach (Vector3Int _pos in _posList)
                {
                    MG_Tile _mgTile = _map.GetMgTile(_pos);
                    int _x = _mgTile.Pos.x - XMinF;
                    int _y = _mgTile.Pos.y - YMinF;
                    if (_x == _i && _y == _ii)
                    {
                        _mgTileArray[_i, _ii] = _mgTile;
                        break;
                    }
                }
            }
        }
        return _mgTileArray;
    }

    /// <summary>
    /// Перевернуть массив по часовой
    /// </summary>
    /// <param name="_A"></param>
    /// <returns></returns>
    private MG_Tile[,] RotateArrayCounterClockwise(MG_Tile[,] _A)
    {
        MG_Tile[,] _A2 = new MG_Tile[_A.GetLength(1), _A.GetLength(0)];
        int _ii2, _i2 = 0;
        for (int _ii = _A.GetLength(1) - 1; _ii >= 0; _ii--)
        {
            _ii2 = 0;
            for (int _i = 0; _i < _A.GetLength(0); _i++)
            {
                _A2[_i2, _ii2] = _A[_i, _ii];
                _ii2++;
            }
            _i2++;
        }


        //int _tempX = 
        sizeX = _A.GetLength(1);
        sizeY = _A.GetLength(0);
        return _A2;
    }

    /// <summary>
    /// Перевернуть массив против часовой
    /// </summary>
    /// <param name="_A"></param>
    /// <returns></returns>
    private MG_Tile[,] RotateArrayClockwise(MG_Tile[,] _A)
    {
        MG_Tile[,] _A2 = new MG_Tile[_A.GetLength(1), _A.GetLength(0)];
        int _ii2, _i2 = 0;
        //for (int _ii = _A.GetLength(1) - 1; _ii >= 0; _ii--)
        for (int _ii = 0; _ii < _A.GetLength(1); _ii++)
        {
            _ii2 = 0;
            //for (int _i = 0; _i < _A.GetLength(0); _i++)
            for (int _i = _A.GetLength(0) - 1; _i >= 0; _i--)
            {
                _A2[_i2, _ii2] = _A[_i, _ii];
                _ii2++;
            }
            _i2++;
        }


        //int _tempX = 
        sizeX = _A.GetLength(1);
        sizeY = _A.GetLength(0);
        return _A2;
    }

    /// <summary>
    /// Повернуть тайлы по часовой
    /// </summary>
    /// <param name="_A"></param>
    private void RotateTilesCW(MG_Tile[,] _A)
    {
        for (int _ii = 0; _ii < sizeY; _ii++)
        {
            for (int _i = 0; _i < sizeX; _i++)
            {
                MG_Tile _mgtile = _A[_i, _ii];
                if (_mgtile != null)
                {
                    _mgtile.Pos = new Vector3Int(_i, _ii, 0);
                    RotateMgTileProperties(_mgtile, true);//Повернуть тайл по часовой или против часовой
                }

            }
        }
    }

    /// <summary>
    /// Повернуть тайлы против часовой
    /// </summary>
    /// <param name="_A"></param>
    private void RotateTilesCCW(MG_Tile[,] _A)
    {
        for (int _ii = 0; _ii < sizeY; _ii++)
        {
            for (int _i = 0; _i < sizeX; _i++)
            {
                MG_Tile _mgtile = _A[_i, _ii];
                if (_mgtile != null)
                {
                    _mgtile.Pos = new Vector3Int(_i, _ii, 0);
                    RotateMgTileProperties(_mgtile, false);//Повернуть тайл по часовой или против часовой
                }

            }
        }
    }

    /// <summary>
    /// Найти Max или Min
    /// </summary>
    /// <param name="_i"></param>
    /// <param name="_ii"></param>
    /// <param name="_findMin"></param>
    private void SetMinOrMax(ref int _i, int _ii, bool _findMin)
    {
        if (_findMin)
        {
            if (_i > _ii)
                _i = _ii;
        }
        else
          if (_i < _ii)
            _i = _ii;
    }

    /// <summary>
    /// Повернуть тайл по часовой или против часовой
    /// </summary>
    /// <param name="_mgtile"></param>
    /// <param name="_clockwise"></param>
    private void RotateMgTileProperties(MG_Tile _mgtile, bool _clockwise)
    {
        //СВОЙСТВА СТЕН
        MG_WallProperty _wall_E;
        MG_WallProperty _wall_S;
        MG_WallProperty _wall_W;
        MG_WallProperty _wall_N;
        //СВОЙСТВА ДВЕРЕЙ, ОКОН = ВХОДА
        MG_EntryProperty _entry_E;
        MG_EntryProperty _entry_S;
        MG_EntryProperty _entry_W;
        MG_EntryProperty _entry_N;
        //СВОЙСТВО И НАПРАВЛЕНИЕ ОБЪЕКТА
        Direction _propDir = Direction.None;

        if (_clockwise)
        {
            _wall_E = _mgtile.PropWall_N;
            _wall_S = _mgtile.PropWall_E;
            _wall_W = _mgtile.PropWall_S;
            _wall_N = _mgtile.PropWall_W;
            _entry_E = _mgtile.PropEntry_N;
            _entry_S = _mgtile.PropEntry_E;
            _entry_W = _mgtile.PropEntry_S;
            _entry_N = _mgtile.PropEntry_W;
            if (_mgtile.HasProp())
            {
                _propDir = MG_Direction.RotateDir(_mgtile.PropDir, true);
            }
        }
        else
        {
            _wall_W = _mgtile.PropWall_N;
            _wall_N = _mgtile.PropWall_E;
            _wall_E = _mgtile.PropWall_S;
            _wall_S = _mgtile.PropWall_W;
            _entry_W = _mgtile.PropEntry_N;
            _entry_N = _mgtile.PropEntry_E;
            _entry_E = _mgtile.PropEntry_S;
            _entry_S = _mgtile.PropEntry_W;
            if (_mgtile.HasProp())
            {
                _propDir = MG_Direction.RotateDir(_mgtile.PropDir, false);
            }
        }

        //СВОЙСТВА СТЕН
        _mgtile.PropWall_N = _wall_N;
        _mgtile.PropWall_E = _wall_E;
        _mgtile.PropWall_S = _wall_S;
        _mgtile.PropWall_W = _wall_W;
        //СВОЙСТВА ДВЕРЕЙ, ОКОН = ВХОДА
        _mgtile.PropEntry_N = _entry_N;
        _mgtile.PropEntry_E = _entry_E;
        _mgtile.PropEntry_S = _entry_S;
        _mgtile.PropEntry_W = _entry_W;
        //СВОЙСТВО И НАПРАВЛЕНИЕ ОБЪЕКТА
        _mgtile.PropDir = _propDir;//НИЧЕГО НЕ МЕНЯЕТСЯ У ОБЪЕКТА, КРОМЕ НАПРАВЛЕНИЯ
    }

  
}
