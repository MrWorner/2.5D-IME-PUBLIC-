using System.Collections.Generic;

/// <summary>
/// Класс матрица https://stackoverflow.com/questions/36667409/how-to-create-a-2d-array-of-unknown-length-in-c
/// </summary>
public class Matrix
{
    private Dictionary<string, string> Data = new Dictionary<string, string>();
    private int sizeX = 0;
    public int SizeX
    {
        get
        {
            return sizeX;
        }
        set
        {
            if (value > sizeX)
                sizeX = value;
        }
    }
    private int sizeY = 0;
    public int SizeY
    {
        get
        {
            return sizeY;
        }
        set
        {
            if (value > sizeY)
                sizeY = value;
        }
    }

  
    public string this[int x, int y]
    {
        get
        {
            string key = this.GetKey(x, y);
            return Data.ContainsKey(key) ? Data[key] : null;
        }
        set
        {
            string key = this.GetKey(x, y);
            if (SizeX > x) SizeX = x;
            if (SizeY > y) SizeY = y;
            if (value == null)
                Data.Remove(key);
            else
                Data[key] = value;
        }
    }
    private string GetKey(int x, int y)
    {

        return string.Join(",", new[] { x, y });
    }

}
