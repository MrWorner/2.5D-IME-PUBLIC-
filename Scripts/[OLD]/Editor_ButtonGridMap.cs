//using UnityEngine;
//using UnityEditor;
//using System.Collections.Generic;
//using UnityEngine.Tilemaps;

//[CustomEditor(typeof(MG_TileMap), true)]
//public class Editor_ButtonGridMap : Editor
//{
//    //public override void OnInspectorGUI()
//    //{
//    //    base.OnInspectorGUI();
//    //    MG_TileMap mgMap = (MG_TileMap)target;

//    //    if (GUILayout.Button("Generate Grid"))
//    //    {
//    //        //mgMap.tileMap_floor = mgMap.GetComponent<Tilemap>();
//    //        ////-----------MG_TileMap.dictionary_MG_tiles = new Dictionary<Vector3Int, MG_Tile>();
//    //        //mgMap.ClearMap();
//    //        //mgMap.GenerateMap();
//    //    }

//    //    if (GUILayout.Button("Clear Grid"))
//    //    {
//    //        //mgMap.tileMap_floor = mgMap.GetComponent<Tilemap>();
//    //        //mgMap.ClearMap();
//    //    }

//    //    if (GUILayout.Button("Save Map JSON"))
//    //    {
//    //        //------------------------mgMap.SaveMapJSON(true);
//    //    }

//    //    if (GUILayout.Button("Load Map JSON"))
//    //    {
//    //        //------------------------mgMap.LoadMapJSON();
//    //    }

//    //}
//}

////[CustomEditor(typeof(MG_TileMapIso), true)]
////public class Editor_ButtonGridMapIso : Editor
////{
////    public override void OnInspectorGUI()
////    {
////        base.OnInspectorGUI();
////        MG_TileMapIso mgMap = (MG_TileMapIso)target;

////        if (GUILayout.Button("Generate Grid"))
////        {
////            mgMap.tileMap_floor = mgMap.GetComponent<Tilemap>();
////            //-----------MG_TileMap.dictionary_MG_tiles = new Dictionary<Vector3Int, MG_Tile>();
////            mgMap.ClearMap();
////            mgMap.GenerateMap();
////        }

////        if (GUILayout.Button("Clear Grid"))
////        {
////            mgMap.tileMap_floor = mgMap.GetComponent<Tilemap>();
////            mgMap.ClearMap();
////        }

////        if (GUILayout.Button("Save Map JSON"))
////        {
////            //------------------------mgMap.SaveMapJSON(true);
////        }

////        if (GUILayout.Button("Load Map JSON"))
////        {
////            //------------------------mgMap.LoadMapJSON();
////        }

////    }
////}

//[CustomEditor(typeof(MG_MapRotator), true)]
//public class Editor_ButtonMapRotator : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        MG_MapRotator mapRotator = (MG_MapRotator)target;

//        if (GUILayout.Button("Calculate size"))
//        {
//            //mapRotator.CalculateMapSize();
//        }
//        if (GUILayout.Button("Rotate map"))
//        {
//            //mapRotator.RotateMap(true);
//        }
//    }
//}

//[CustomEditor(typeof(MG_WalkableArea), true)]
//public class Editor_ButtonUnit : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        MG_WalkableArea _walkArea = (MG_WalkableArea)target;

//        if (GUILayout.Button("ShowWalkableArea"))
//        {
//           //----------- _walkArea.ShowWalkableArea();
//        }   
//    }
//}