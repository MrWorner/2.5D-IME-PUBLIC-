using System.IO;
using UnityEngine;
//https://gamedev.stackexchange.com/questions/146104/is-it-possible-to-not-pack-asset-files-into-archives-when-building-a-unity-game
//https://answers.unity.com/questions/990637/loading-in-sprites-outside-of-unity.html
//https://coffeebraingames.wordpress.com/2017/09/26/unity-game-considerations-for-modding-support/
//https://www.raywenderlich.com/479-using-streaming-assets-in-unity

public class MG_SpriteLoader : MonoBehaviour
{
    [SerializeField]
    private bool showError;//показывать предупреждение о ненахождении пользовательского спрайта (LoadSprite)
    
    /// <summary>
    /// Загрузить спрайт
    /// </summary>
    /// <param name="_filePath"></param>
    /// <param name="_pivotX"></param>
    /// <param name="_pivotY"></param>
    /// <param name="_pixelsPerUnit"></param>
    /// <returns></returns>
    public Sprite LoadSprite(string _filePath, float _pivotX, float _pivotY, float _pixelsPerUnit)//метод загрузки спрайта из директории
    {
        //----------Debug.Log("MG_LoadFile LoadSprite(): SPRITE LOADED! " + _filePath);
        byte[] _fileData;//для содержания самого файла
        Sprite _sprite = null;//для хранения спрайта
        Texture2D texture = null;//для хранения текстуры
        if (File.Exists(_filePath))//проверяем что путь к файлу существует
        {
            _fileData = File.ReadAllBytes(_filePath);//считываем файл
            texture = new Texture2D(20, 20);//создаем новую текстуру
            texture.LoadImage(_fileData);//загружаем изображение, которое уже в памяти
            _sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(_pivotX, _pivotY), _pixelsPerUnit);//создаем спрайт с нужными размерами
        }
        else
        {
            if (showError)
                Debug.Log("<color=yellow>MG_LoadFile LoadSprite(): FILE NOT FOUND! " + _filePath + "</color>");
        }
        return _sprite;
    }
}
