using UnityEngine;
using UnityEngine.Tilemaps;

public enum FloorType { None, Carpet, Grass};//Вид пола
public class MG_FloorProperty  : MG_Property
{
    ///public string ID { get; set; }//айди свойства
    ///public string Name { get; set; } //имя свойства
    public Tile Tile2D { get; set; }//тайл 2D (TOP DOWN)
    public Tile Tile2DIso { get; set; }//тайл 2.5D
    public FloorType Type { get; set; }//тип
}
