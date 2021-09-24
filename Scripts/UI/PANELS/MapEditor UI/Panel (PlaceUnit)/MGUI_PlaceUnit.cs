using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MGUI_PlaceUnit : MonoBehaviour
{
    private MG_UItools uiTool;
    public Sprite SpritePlace;
    public Sprite SpriteSelect;
    public static Sprite SpriteSelectS;
    [SerializeField]
    private Image ico;

    [SerializeField]
    private GameObject UnitContainerObj;

    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    //private void Awake()
    //{
    //    if (editor == null)
    //        Debug.Log("<color=red>MG_UIplaceUnit Awake(): MG_Editor не прикреплен!</color>");
    //    uiTool = GetComponent<MG_UItools>();
    //    uiTool.OnDisableButs += Disable;
    //    SpriteSelectS = SpriteSelect;
    //}

    //public void Disable()
    //{
    //    ico.color = Color.white;
    //}

    //public void Pressed()
    //{
    //    uiTool.UIColorPalette.ColorPalette(false);
    //    if(MG_Unit.MainHero == null)
    //    {
    //        editor.SetEditorMode(EditorMode.PlaceUnit);
    //        ico.color = Color.green;
    //        uiTool.MapChosen.CursorObject.GetComponent<SpriteRenderer>().sprite = SpritePlace;
    //    }
    //   else
    //    {
    //        editor.SetEditorMode(EditorMode.UnitControl);
    //        ico.color = Color.magenta;
    //        uiTool.MapChosen.CursorObject.GetComponent<SpriteRenderer>().sprite = SpriteSelect;
    //        MG_Unit.MainHero.GetComponent<MG_WalkableArea>().ShowWalkableArea();
    //    }
        
    //    DisableOtherB();        
    //} 

    //public void DisableOtherB()
    //{
    //    uiTool.OnDisableButs -= Disable;
    //    uiTool.DisableOtherButtons("MG_UIplaceUnit");
    //    uiTool.OnDisableButs += Disable;
    //}
}
