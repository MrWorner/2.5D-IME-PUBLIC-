using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum EntryType { None, Door, Window};//Вид входа для стены (BlockWindow, Hole, SmallHole)
public class MG_EntryProperty : MG_Property
{
    ///public string ID { get; set; }
    ///public string Name { get; set; }
    public Tile Tile2DIso_H { get; set; }
    public Tile Tile2DIso_V { get; set; }
    public Tile Tile2D_H { get; set; }
    public Tile Tile2D_V { get; set; }
    public EntryType Type { get; set; }
}
