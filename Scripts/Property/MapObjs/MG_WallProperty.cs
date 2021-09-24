using UnityEngine.Tilemaps;

public enum WallType { None, Wall, Fence, SmallFence};//Вид пола

public class MG_WallProperty : MG_Property
{
    ///public string ID { get; set; }
    ///public string Name { get; set; }
    public Tile Tile2DIso_H { get; set; }
    public Tile Tile2DIso_V { get; set; }
    public Tile Tile2D_H { get; set; }
    public Tile Tile2D_V { get; set; }
    public WallType Type { get; set; }
}
