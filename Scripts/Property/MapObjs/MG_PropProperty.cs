using UnityEngine.Tilemaps;

public enum PropSize { None, Small, Big};//размер

public class MG_PropProperty : MG_Property
{
    ///public string ID { get; set; }
    ///public string Name { get; set; }
    public Tile Tile2DIso_N { get; set; }
    public Tile Tile2DIso_E { get; set; }
    public Tile Tile2DIso_S { get; set; }
    public Tile Tile2DIso_W { get; set; }
    public Tile Tile2D_N { get; set; }
    public Tile Tile2D_E { get; set; }
    public Tile Tile2D_S { get; set; }
    public Tile Tile2D_W { get; set; }
    public PropSize Size { get; set; }

    //public bool IsMovable { get; set; }
}
