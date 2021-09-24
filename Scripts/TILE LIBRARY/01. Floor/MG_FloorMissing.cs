using UnityEngine;

public class MG_FloorMissing : MG_TileMissing
{

    [SerializeField]
    private Sprite Sprite2D;//[R] Спрайт 2D TD
    [SerializeField]
    private Sprite SpriteIso;//[R] Спрайт Iso

    private void Awake()
    {
        if (SpriteIso == null)
            Debug.Log("<color=red>MG_FloorMissing Awake(): спрайт для SpriteIso не прикреплен!</color>");
        if (Sprite2D == null)
            Debug.Log("<color=red>MG_FloorMissing Awake(): спрайт для Sprite2D не прикреплен!</color>");
    }

    /// <summary>
    /// Получить спрайт 2D Top Down
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite2D()
    {
        return Sprite2D;
    }

    /// <summary>
    /// Получить спрайт Isometric 2D
    /// </summary>
    /// <returns></returns>
    public Sprite GetSpriteIso()
    {
        return SpriteIso;
    }
}
