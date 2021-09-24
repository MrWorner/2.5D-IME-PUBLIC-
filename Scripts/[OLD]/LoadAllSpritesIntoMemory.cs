using System.IO;
using UnityEngine;
using UnityEngine.UI;

//https://gamedev.stackexchange.com/questions/146104/is-it-possible-to-not-pack-asset-files-into-archives-when-building-a-unity-game
//https://answers.unity.com/questions/990637/loading-in-sprites-outside-of-unity.html
//https://coffeebraingames.wordpress.com/2017/09/26/unity-game-considerations-for-modding-support/
//https://www.raywenderlich.com/479-using-streaming-assets-in-unity

public class LoadAllSpritesIntoMemory : MonoBehaviour//ДАННЫЙ КЛАСС НЕ ИСПОЛЬЗУЕТСЯ!
{
    void Start() // Just doing one temporary sprite for now to learn...
    {
        string filePath = "Data\\1.png";//Откуда загрузить спрайт
        Texture2D texture = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            Debug.Log("FILE EXISTS");
            fileData = File.ReadAllBytes(filePath);
            texture = new Texture2D(20, 20);
            texture.LoadImage(fileData);

            Sprite sprite;
            //Sprite Create(Texture2D texture, Rect rect, Vector2 pivot);
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().sprite = sprite;
            else if (GetComponent<Image>() != null)
                GetComponent<Image>().sprite = sprite;
        }
        else
        {
            Debug.Log("FILE NOT FOUND!");
        }
    
    }

}