using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGUI_GeneralTools : MonoBehaviour
{
    [SerializeField]
    private List<MGUI_Tool> listOfUItools;
    [SerializeField]
    private List<GameObject> listOfTilePreviewer;

    private MGUI_Tool curUI_Tool;//выбранный при нажатии

    private void Awake()
    {
        if (listOfUItools.Count == 0)
            Debug.Log("<color=red>MGUI_GeneralTools Awake(): listOfUItools пустой!</color>");
    }

    /// <summary>
    /// Перекрасить во всех блоках кнопок цвет кнопок на необходимый
    /// </summary>
    public void RecolorButtons(MGUI_Tool _uiTool)
    {
        foreach (var _UItool in listOfUItools)
        {
            if (!_uiTool.Equals(_UItool))//если данный блок не выбранный, то красный
                _UItool.DisableVisuallyButtons();//Перекрасить блок кнопок на красный цвет
        }

        if (_uiTool != curUI_Tool)//если выбран данный блок все еще не перекрашен в белый, то перекрашиваем
        {
            _uiTool.EnableVisuallyButtons();//Перекрасить блок кнопок на белый цвет
        }
        curUI_Tool = _uiTool;
    }

    /// <summary>
    /// Перекрасить все кнопки в красный
    /// </summary>
    public void Pressed_RecolorAllButtonsToRed()
    {
        foreach (var _UItool in listOfUItools)
        {
            _UItool.DisableVisuallyButtons();//Перекрасить блок кнопок на красный цвет
        }
        curUI_Tool = null;
    }

    /// <summary>
    /// Менеджер видимости TileViewer'ов
    /// </summary>
    /// <param name="_num"></param>
    public void Pressed_ShowTileViewer(int _num)
    {
        int i = 0;
        foreach (var _tilePreviewer in listOfTilePreviewer)
        {
            if (i == _num)
                _tilePreviewer.SetActive(true);
            else
                _tilePreviewer.SetActive(false);
            i++;
        }
        
    }

}
