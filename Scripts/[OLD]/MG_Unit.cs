using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MG_Unit : MonoBehaviour
{
    public MG_Tile MGtile { get; set; }
    public GameObject GameObj { get; set; }
    public int TU = 12;

    public static MG_Unit MainHero;

    //public MG_Unit(MG_Tile _mgtile, int _tu)
    //{
    //    GetComponent<MG_WalkableArea>().ShowWalkableArea(MGtile, TU);
    //}




    //public void ShowWalkableArea()
    //{
    //    MGtile.MGmap.CursorObject.GetComponent<SpriteRenderer>().sprite = MG_UIplaceUnit.SpriteSelectS;//курсор
    //    Tilemap _visibleArea = MGtile.MGmap.VisibleArea;
    //    Tile _tile = MGtile.MGmap.walkableTile;

    //    //var path = ConstantPath.Construct(GameObj.transform.position, TU * 1000 + 1);
    //    var path = ConstantPath.Construct(GameObj.transform.position, (TU + 1) * TU_def);
    //    //path.traversalProvider = unit.traversalProvider;
    //    // Schedule the path for calculation
    //    AstarPath.StartPath(path);

    //    // Force the path request to complete immediately
    //    // This assumes the graph is small enough that
    //    // this will not cause any lag
    //    path.BlockUntilCalculated();


    //    MG_TileMapIso _map = MGtile.MGmap as MG_TileMapIso;
    //    GameObject _nodeObj = _map.node;
    //    foreach (var node in path.allNodes)
    //    {
    //        if (node != path.startNode)
    //        {
    //            // Create a new node prefab to indicate a node that can be reached
    //            // NOTE: If you are going to use this in a real game, you might want to
    //            // use an object pool to avoid instantiating new GameObjects all the time


    //            //possibleMoves.Add(go);



    //            //Vector3 _worldPos = (Vector3)node.position;//получаем координаты клика мыши
    //            //Vector3Int _pos = _visibleArea.WorldToCell(_worldPos);
    //            //_visibleArea.SetTile(_pos, _tile);

    //            GameObject _newNode = Instantiate(_nodeObj);
    //            _newNode.transform.position = (Vector3)node.position;


    //            //go.GetComponent<Astar3DButton>().node = node;



    //        }
    //    }
    //}  
}
