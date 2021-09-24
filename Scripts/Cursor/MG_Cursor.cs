using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Cursor : MonoBehaviour
{
    private static MG_Cursor instance;//одиночка

    //Спрайты курсоров
    public Sprite Sprite_Floor;
    public Sprite Sprite_Grass;

    public Sprite Sprite_DoorN;
    public Sprite Sprite_DoorE;
    public Sprite Sprite_DoorS;
    public Sprite Sprite_DoorW;
    public Sprite Sprite_DoorEraser;
    
    public Sprite Sprite_WindowN;
    public Sprite Sprite_WindowE;
    public Sprite Sprite_WindowS;
    public Sprite Sprite_WindowW;
    public Sprite Sprite_WindowEraser;

    public Sprite Sprite_PropN;
    public Sprite Sprite_PropE;
    public Sprite Sprite_PropS;
    public Sprite Sprite_PropW;
    public Sprite Sprite_PropEraser;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MG_Cursor Awake(): MG_Cursor может быть только один компонент на Сцене, другие не нужны.</color>");
    }

    public void SetSprite(Sprite _sprite)//Смена спрайта у курсора
    {
        GetComponent<SpriteRenderer>().sprite = _sprite;
    }

    public void SetColor(Color _color)
    {
        GetComponent<SpriteRenderer>().color = _color;
    }
}
