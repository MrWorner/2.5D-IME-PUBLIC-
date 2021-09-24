using UnityEngine;

public class MG_PropMissing : MG_TileMissing
{
    [SerializeField]
    private Sprite SpriteIso_N;//[R] Спрайт Iso
    [SerializeField]
    private Sprite SpriteIso_E;//[R] Спрайт Iso
    [SerializeField]
    private Sprite SpriteIso_S;//[R] Спрайт Iso
    [SerializeField]
    private Sprite SpriteIso_W;//[R] Спрайт Iso

    [SerializeField]
    private Sprite Sprite2D_N;//[R] Спрайт 2D TD
    [SerializeField]
    private Sprite Sprite2D_E;//[R] Спрайт 2D TD
    [SerializeField]
    private Sprite Sprite2D_S;//[R] Спрайт 2D TD
    [SerializeField]
    private Sprite Sprite2D_W;//[R] Спрайт 2D TD

    [SerializeField]
    private PropSize size;//размер

    private void Awake()
    {
        if (SpriteIso_N == null)
            Debug.Log("<color=red>MG_PropMissing Awake(): спрайт для SpriteIso_N не прикреплен!</color>");
        if (SpriteIso_E == null)
            Debug.Log("<color=red>MG_PropMissing Awake(): спрайт для SpriteIso_E не прикреплен!</color>");
        if (SpriteIso_S == null)
            Debug.Log("<color=red>MG_PropMissing Awake(): спрайт для SpriteIso_S не прикреплен!</color>");
        if (SpriteIso_W == null)
            Debug.Log("<color=red>MG_PropMissing Awake(): спрайт для SpriteIso_W не прикреплен!</color>");
        if (Sprite2D_N == null)
            Debug.Log("<color=red>MG_PropMissing Awake(): спрайт для Sprite2D_N не прикреплен!</color>");
        if (Sprite2D_E == null)
            Debug.Log("<color=red>MG_PropMissing Awake(): спрайт для Sprite2D_E не прикреплен!</color>");
        if (Sprite2D_S == null)
            Debug.Log("<color=red>MG_PropMissing Awake(): спрайт для Sprite2D_S не прикреплен!</color>");
        if (Sprite2D_W == null)
            Debug.Log("<color=red>MG_PropMissing Awake(): спрайт для Sprite2D_W не прикреплен!</color>");
    }

    /// <summary>
    /// Получить спрайт 2D Top Down
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite2D(Direction _dir)
    {
        Sprite _sprite = null;
        switch (_dir)
        {
            case Direction.North:
                _sprite = Sprite2D_N;
                break;
            case Direction.East:
                _sprite = Sprite2D_E;
                break;
            case Direction.South:
                _sprite = Sprite2D_S;
                break;
            case Direction.West:
                _sprite = Sprite2D_W;
                break;
            default:
                Debug.Log("MG_PropMissing GetSprite2D(): нереализованное направление: " + _dir);
                break;
        }
        return _sprite;
    }

    /// <summary>
    /// Получить спрайт Isometric 2D
    /// </summary>
    /// <returns></returns>
    public Sprite GetSpriteIso(Direction _dir)
    {
        Sprite _sprite = null;
        switch (_dir)
        {
            case Direction.North:
                _sprite = SpriteIso_N;
                break;
            case Direction.East:
                _sprite = SpriteIso_E;
                break;
            case Direction.South:
                _sprite = SpriteIso_S;
                break;
            case Direction.West:
                _sprite = SpriteIso_W;
                break;
            default:
                Debug.Log("MG_PropMissing GetSpriteIso(): нереализованное направление: " + _dir);
                break;
        }
        return _sprite;
    }

}
