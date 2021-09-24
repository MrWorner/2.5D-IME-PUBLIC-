using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_EntryMissing : MG_TileMissing
{
    //Door
    [SerializeField]
    private Sprite SpriteDoorIsoH;//[R] Спрайт Iso по горизонтали
    [SerializeField]
    private Sprite SpriteDoorIsoV;//[R] Спрайт по горизонтали
    [SerializeField]
    private Sprite SpriteDoor2DH;//[R] Спрайт по вертикали
    [SerializeField]
    private Sprite SpriteDoor2DV;//[R] Спрайт по вертикали

    //Window
    [SerializeField]
    private Sprite SpriteWindowIsoH;//[R] Спрайт Iso по горизонтали
    [SerializeField]
    private Sprite SpriteWindowIsoV;//[R] Спрайт по горизонтали
    [SerializeField]
    private Sprite SpriteWindow2DH;//[R] Спрайт по вертикали
    [SerializeField]
    private Sprite SpriteWindow2DV;//[R] Спрайт по вертикали
    [SerializeField]


    private void Awake()
    {
        if (SpriteDoorIsoH == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpriteDoorIsoH не прикреплен!</color>");
        if (SpriteDoorIsoV == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpriteDoorIsoV не прикреплен!</color>");
        if (SpriteDoor2DH == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpriteDoor2DH не прикреплен!</color>");
        if (SpriteDoor2DV == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpriteDoor2DV не прикреплен!</color>");

        if (SpriteWindowIsoH == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpriteWindowIsoH не прикреплен!</color>");
        if (SpriteWindowIsoV == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpritWindowIsoV не прикреплен!</color>");
        if (SpriteWindow2DH == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpritWindow2DH не прикреплен!</color>");
        if (SpriteWindow2DV == null)
            Debug.Log("<color=red>MG_EntryMissing Awake(): спрайт для SpriteWindow2DV не прикреплен!</color>");
    }


    /// <summary>
    /// Получить спрайт
    /// </summary>
    /// <param name="_lineDir"></param>
    /// <param name="_projectionType"></param>
    /// <param name="_entryType"></param>
    /// <returns></returns>
    public Sprite GetSprite(LineDirection _lineDir, ProjectionType _projectionType, EntryType _entryType)
    {

        Sprite _sprite = null;
        switch (_entryType)
        {
            case EntryType.Door:

                if (_lineDir.Equals(LineDirection.Horizontal))
                {
                    if (_projectionType.Equals(ProjectionType.Isometric2D))
                    {
                        _sprite = SpriteDoorIsoH;
                    }
                    else if (_projectionType.Equals(ProjectionType.TopDown2D))
                    {
                        _sprite = SpriteDoor2DH;
                    }
                    else
                        Debug.Log("MG_EntryMissing GetSprite(): тип ProjectionType неизвестен " + _projectionType);
                }
                else if (_lineDir.Equals(LineDirection.Vertical))
                {
                    if (_projectionType.Equals(ProjectionType.Isometric2D))
                    {
                        _sprite = SpriteDoorIsoV;
                    }
                    else if (_projectionType.Equals(ProjectionType.TopDown2D))
                    {
                        _sprite = SpriteDoor2DV;
                    }
                    else
                        Debug.Log("MG_EntryMissing GetSprite(): тип ProjectionType неизвестен " + _projectionType);
                }
                else
                    Debug.Log("MG_EntryMissing GetSprite(): тип LineDirection неизвестен " + _lineDir);

                break;
            case EntryType.Window:

                if (_lineDir.Equals(LineDirection.Horizontal))
                {
                    if (_projectionType.Equals(ProjectionType.Isometric2D))
                    {
                        _sprite = SpriteWindowIsoH;
                    }
                    else if (_projectionType.Equals(ProjectionType.TopDown2D))
                    {
                        _sprite = SpriteWindow2DH;
                    }
                    else
                        Debug.Log("MG_EntryMissing GetSprite(): тип ProjectionType неизвестен " + _projectionType);
                }
                else if (_lineDir.Equals(LineDirection.Vertical))
                {
                    if (_projectionType.Equals(ProjectionType.Isometric2D))
                    {
                        _sprite = SpriteWindowIsoV;
                    }
                    else if (_projectionType.Equals(ProjectionType.TopDown2D))
                    {
                        _sprite = SpriteWindow2DV;
                    }
                    else
                        Debug.Log("MG_EntryMissing GetSprite(): тип ProjectionType неизвестен " + _projectionType);
                }
                else
                    Debug.Log("MG_EntryMissing GetSprite(): тип LineDirection неизвестен " + _lineDir);

                break;
            default:
                Debug.Log("MG_EntryMissing GetSprite(): тип EntryType нереализован! " + _entryType);
                break;
        }
        return _sprite;
    }
}