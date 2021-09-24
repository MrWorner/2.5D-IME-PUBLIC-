using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class MG_PathConstructor : MonoBehaviour
{
    private static MG_PathConstructor instance;//в редакторе только один объект должен быть создан

    //[SerializeField]
    //private AstarPath astarPath;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("MG_PathConstructor Awake(): MG_PathConstructor может быть только один компонент на Сцене, другие не нужны.");
    }

    public void InitPointNode(MG_Tile _mgtile, MapType _type)
    {
        float _x = 0f;
        float _y = 0f;
        if (_type.Equals(MapType.TopDown2D))
        {
            _x = _mgtile.WorldLocation.x + 0.5f;
            _y = _mgtile.WorldLocation.y + 0.5f;
        }
        else if (_type.Equals(MapType.Isometric2D))
        {
            //_x = _mgtile.LocalPlace.x * 0.1f - 15;
            //_y = _mgtile.LocalPlace.y * 0.1f + 15;
            _x = _mgtile.WorldLocation.x;
            _y = _mgtile.WorldLocation.y + 0.225f;
            // _x = _mgtile.LocalPlace.x;
            // _y = _mgtile.LocalPlace.y;
        }
        else
            Debug.Log("MG_PathConstructor CreatePointNode(): Неизвестный тип карты.");

        var _graphLock = AstarPath.active.PausePathfinding();
        var _nodeNew = AstarPath.active.data.pointGraph.AddNode((Int3)new Vector3(_x, _y, 0));
        _nodeNew.LocalPlace = _mgtile.Pos;
        // var _nodeNew = AstarPath.active.data.pointGraph.AddNode((Int3)new Vector3(_mgtile.LocalPlace.x, _mgtile.LocalPlace.y, 0));
        //var _nodeNew = AstarPath.active.data.pointGraph.AddNode((Int3)new Vector3(0, 0, 0));
        _mgtile.NodePro = _nodeNew;

        _graphLock.Release();
    }

    /// <summary>
    /// метод для удаления нода
    /// </summary>
    /// <param name="_mgTile"></param>
    public void DestroyNode(MG_Tile _mgTile)
    {
        PointNode _node = _mgTile.NodePro;
        _node.Destroy();
    }

    /// <summary>
    /// Метод для соединения нода. Здесь присутствует подсчет расстояние
    /// </summary>
    /// <param name="_node"></param>
    /// <param name="_nodeN"></param>
    public void LinkNode(PointNode _node, PointNode _nodeN)
    {
        //_node.AddConnection(_nodeN, 1);
        //var _cost = (uint)(_nodeN.position - _node.position).costMagnitude;
        //Debug.Log("_cost = " + _cost);
        _node.AddConnection(_nodeN, (uint)(_nodeN.position - _node.position).costMagnitude);
        //_node.AddConnection(_nodeN, (uint)(_nodeN.position - _node.position).costMagnitude);
        //_node.AddConnection(_nodeN, (uint)1000);
    }

    /// <summary>
    /// Удалить связь между нодами
    /// </summary>
    /// <param name="_node"></param>
    /// <param name="_nodeN"></param>
    public void UnLinkNode(PointNode _node, PointNode _nodeN)
    {
        _node.RemoveConnection(_nodeN);
    }
}
