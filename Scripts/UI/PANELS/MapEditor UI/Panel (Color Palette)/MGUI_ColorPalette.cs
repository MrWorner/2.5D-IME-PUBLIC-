using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MGUI_ColorPalette : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPanelObj;//[R] объект палитры
    [SerializeField]
    private GameObject markerObj;//[R] объект, для показа выбранного цвета на палитре
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    [SerializeField]
    private Color chosenColor = Color.white;//выбранный цвет

    private void Awake()
    {
        if (editor == null)
            Debug.Log("<color=red>MG_UIcolorPalette Awake(): MG_Editor не прикреплен!</color>");
        if (mainPanelObj == null)
            Debug.Log("<color=red>MG_UIcolorPalette Awake(): объект для mainPanelObj не прикреплен!</color>");
        if (markerObj == null)
            Debug.Log("<color=red>MG_UIcolorPalette Awake(): объект для chosenColorObj не прикреплен!</color>");
    }

    /// <summary>
    /// [B] Поменять цвет 
    /// </summary>
    /// <param name="colorImage"></param>
    public void Pressed_ChangeColor(Image colorImage)
    {
        Color _color = colorImage.color;//выбранный цвет
        markerObj.transform.position = colorImage.gameObject.transform.position;
        chosenColor = _color;
        editor.SetFloorColor(_color);
    }

    /// <summary>
    /// [B] Задать видимость панели
    /// </summary>
    /// <param name="_enable"></param>
    //public void Pressed_SetVisible(bool _enable)
    //{
    //    mainPanelObj.SetActive(_enable);
    //    markerObj.SetActive(_enable);
    //}
}
