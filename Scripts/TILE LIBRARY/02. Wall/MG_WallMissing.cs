using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_WallMissing : MG_TileMissing
{
    [SerializeField]
    private Sprite SpriteIsoH;//[R] Спрайт Iso по горизонтали
    [SerializeField]
    private Sprite SpriteIsoV;//[R] Спрайт по горизонтали
    [SerializeField]
    private Sprite Sprite2DH;//[R] Спрайт по вертикали
    [SerializeField]
    private Sprite Sprite2DV;//[R] Спрайт по вертикали

    private void Awake()
    {
        if (SpriteIsoH == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpriteIsoH не прикреплен!</color>");
        if (SpriteIsoV == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpriteIsoV не прикреплен!</color>");
        if (Sprite2DH == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для Sprite2DH не прикреплен!</color>");
        if (Sprite2DV == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для Sprite2DV не прикреплен!</color>");
    }

    /// <summary>
    /// Получить спрайт 2D Top Down Горизонтальный
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite2DH()
    {
        return Sprite2DH;
    }

    /// <summary>
    /// Получить спрайт 2D Top Down Вертикальный
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite2DV()
    {
        return Sprite2DV;
    }

    /// <summary>
    /// Получить спрайт Isometric 2D Горизонтальный
    /// </summary>
    /// <returns></returns>
    public Sprite GetSpriteIsoH()
    {
        return SpriteIsoH;
    }

    /// <summary>
    /// Получить спрайт Isometric 2D Вертикальный
    /// </summary>
    /// <returns></returns>
    public Sprite GetSpriteIsoV()
    {
        return SpriteIsoV;
    }

}
