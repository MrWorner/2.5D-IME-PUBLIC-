using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGUI_Rotate : MonoBehaviour
{
    [SerializeField]
    private MG_MapRotator mapRotator;//[R]
    [SerializeField]
    private MG_Editor editor;//[R]

    private void Awake()
    {
        if (mapRotator == null)
            Debug.Log("<color=red>MGUI_Rotate Awake(): MG_MapRotator не прикреплен!</color>");
        if (editor == null)
            Debug.Log("<color=red>MGUI_Rotate Awake(): MG_Editor не прикреплен!</color>");
    }

    //-----------КНОПКИ поворота карт на 90 градусов
    public void RotateMap(bool _clockwise)
    {
        MG_TileMap _map = editor.GetCurrentMap();//получить активную карту
        mapRotator.RotateMap(_map, true);
    }
}
