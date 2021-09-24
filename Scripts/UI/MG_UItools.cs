using System;
using UnityEngine;
using UnityEngine.UI;

public class MG_UItools : MonoBehaviour
{
    public delegate void ClickAction();
    public event ClickAction OnDisableButs;


    public MG_TileMap MapChosen { get; set; }
    public MG_UIiconChanger UiChanger { get; set; }
    public MG_CameraControl ControlComp;
    //public MG_UIcolorPalette UIColorPalette { get; set; }
    public MGUI_FGE UIfloorType { get; set; }
    //public MG_UIchosenFloorTile UIchosenFloorTile;
    //public MG_UIchosenWallTile UIchosenWallTile;

    public Image Img_Grass;
    public Image Img_Floor;
    public Image Img_Eraser;


    public Image Img_WallN;
    public Image Img_WallE;
    public Image Img_WallS;
    public Image Img_WallW;
    public Image Img_WallEraser;
    public Image Img_DoorN;
    public Image Img_DoorE;
    public Image Img_DoorS;
    public Image Img_DoorW;
    public Image Img_DoorEraser;
    public Image Img_WindowN;
    public Image Img_WindowE;
    public Image Img_WindowS;
    public Image Img_WindowW;
    public Image Img_WindowEraser;
    public Image Img_PropN;
    public Image Img_PropE;
    public Image Img_PropS;
    public Image Img_PropW;

    //[SerializeField]
    public Image Img_PropEraser;
    //[SerializeField]
    public Image Image_NeedWall;
    //[SerializeField]
    public Image Image_NoWall;
    [SerializeField]
    private GameObject tileMap2D;
    [SerializeField]
    private GameObject tileMapISO;

    //public MG_UnitContainer UnitContainer;

    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    private void Awake()
    {
        //CamComp = Camera.main.GetComponent<MG_KeyboardMouseControl>();
        //UiChanger = GetComponent<MG_UIiconChanger>();
        //UIColorPalette = GetComponent<MG_UIcolorPalette>();
        //UIfloorType = GetComponent<MGUI_FGE>();
        //if (editor == null)
        //    Debug.Log("<color=red>MG_TL_Floor Awake(): MG_Editor не прикреплен!</color>");
    }

    public void DisableOtherButtons(string _text)
    {
        //Debug.Log(_text);//КТО ОТКЛЮЧАЕТ, КАКОЙ КЛАСС
        if (OnDisableButs != null)
            OnDisableButs();
        else
            Debug.Log("DisableOtherButtons EMPTY!");
    }

    public void ChangeEditorModeToFloor()
    {       
        //UIchosenWallTile.SetVisible(false);
        editor.SetEditorMode(EditorMode.Floor);
    }

    public void ChangeEditorModeToWalls()
    {
        //UIColorPalette.ColorPalette(false);
        //UIchosenFloorTile.SetVisible(false);
        //UIchosenWallTile.SetVisible(true);
        //editor.SetEditorMode(EditorMode.Walls);
    }

    public void ChangeEditorModeToEntry()
    {
        //UIColorPalette.ColorPalette(false);
        //UIchosenFloorTile.SetVisible(false);
        //UIchosenWallTile.SetVisible(false);
        //editor.SetEditorMode(EditorMode.Entry);
    }

    public void ChangeEditorModeToPros()
    {
        //UIColorPalette.ColorPalette(false);
        //UIchosenFloorTile.SetVisible(false);
        //UIchosenWallTile.SetVisible(false);
        //editor.SetEditorMode(EditorMode.Prop);
    }

    public void SetEntryType(EntryType _type)
    {
        //----------MapChosen.ChosenEntryType = _type;
    }


    //-----------Кнопки Сохранения и тд

    public void Button_S_ClearAll()
    {
        MapChosen.ClearMap();
    }

    //-----------------------Кнопки Включения и выключения стен

    public void CreateWithWalls(bool _create)
    {
        //----------MapChosen.CreateWalls = _create;
        if (_create)
        {
            Image_NeedWall.color = Color.green;
            Image_NoWall.color = Color.grey;
        }
        else
        {
            Image_NeedWall.color = Color.grey;
            Image_NoWall.color = Color.magenta;
        }
    }



}
