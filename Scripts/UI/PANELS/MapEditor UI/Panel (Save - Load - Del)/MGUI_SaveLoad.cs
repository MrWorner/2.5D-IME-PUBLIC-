using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGUI_SaveLoad : MonoBehaviour
{
    [SerializeField]
    private MG_SaveMapManager saveMapManager;//[R] Менеджер загрузки и сохранения карты
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    private void Awake()
    {
        if (editor == null)
            Debug.Log("<color=red>MGUI_SaveLoad Awake(): MG_Editor не прикреплен!</color>");
        if (saveMapManager == null)
            Debug.Log("<color=red>MGUI_SaveLoad Awake(): MG_SaveMapManager не прикреплен!</color>");
    }

    /// <summary>
    /// Загрузить (КНОПКА)
    /// </summary>
    public void PressedLoadMap()
    {
        MG_TileMap _map = editor.GetCurrentMap();//получаем активную карту
        _map.ClearMap();//очищаем карту
        saveMapManager.Load(_map);
    }

    /// <summary>
    /// Сохранить (КНОПКА)
    /// </summary>
    public void PressedSaveMap()
    {
        MG_TileMap _map = editor.GetCurrentMap();//получаем активную карту
        saveMapManager.Save(_map);
    }

    /// <summary>
    /// Очистить (КНОПКА)
    /// </summary>
    public void PressedClearMap()
    {
        MG_TileMap _map = editor.GetCurrentMap();//получаем активную карту
        _map.ClearMap();//очищаем карту
    }
}
