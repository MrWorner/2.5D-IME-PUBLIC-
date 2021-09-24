using System.Collections.Generic;
using UnityEngine;

public class MG_RoomConstructor : MonoBehaviour
{
    ///ОПИСАНИЕ КЛАССА: данный класс нужен для создание комнат, где конструкторы стен и полов взаимодействуют
    ///
    //[SerializeField]
    //private MG_FloorConstructor floorConstructor;//конструктор пола
    [SerializeField]
    private MG_WallConstructor wallConstructor;//конструктор стен
    [SerializeField]
    private MG_NeighbourManager neighbourManager;//менеджер соседей

    void Awake()
    {
        //if (floorConstructor == null)
        //    Debug.Log("<color=red>MG_RoomConstructor Awake(): MG_FloorConstructor не прикреплен!</color>");
        if (wallConstructor == null)
            Debug.Log("<color=red>MG_RoomConstructor Awake(): MG_WallConstructor не прикреплен!</color>");
        if (neighbourManager == null)
            Debug.Log("<color=red>MG_NeighbourManager Awake(): MG_NeighbourManager не прикреплен!</color>");
    }

    /// <summary>
    /// Создание комнаты по выбраному цветовому участку
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_property"></param>
    /// <param name="_color"></param>
    /// <param name="_map"></param>
    public void ConstructRoom(Vector3Int _pos, MG_WallProperty _property, Color _color, MG_TileMap _map, int _floorN)
    {
        foreach (var _dir in MG_Direction.Basic)//берем все простые направления Север, Юг, Запад, Восток и начинаем искать соседей
        {
            MG_Tile _mgTileN = neighbourManager.GetNeigbourByDir(_pos, _dir, _map);
            if (_mgTileN != null)//если сосед существует
            {
                Direction _dirN = MG_Direction.ReverseDir(_dir);//получаем противоположное направление со стороны соседа
                bool _hasWall = _mgTileN.HasWall(_dirN);//получаем логику есть ли стена у соседа в противоположном направлении
                Color _colorN = _mgTileN.Color;//получаем цвет комнаты/пола
                if (_colorN.Equals(_color))//сравниваем цвета, если одинаковый, то нужно убрать стену если они есть
                {
                    if(_hasWall)
                    {
                        wallConstructor.Remove(_pos, _dir, _map, _floorN);//удаляем стену
                    }
                }
                else//если цвета разные, то нужно создать стену если её нет
                {
                    if (!_hasWall)//если нет стены
                    {
                        wallConstructor.Construct(_pos, _property, _dir, _map, _floorN);//создаем стену
                    }
                }
            }
            else//Если нет соседа, по идеи все равно создаем стену
            {
                wallConstructor.Construct(_pos, _property, _dir, _map, _floorN);//создаем стену
            }
        }
    }
}
