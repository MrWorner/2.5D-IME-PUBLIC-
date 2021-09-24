using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIiconChanger : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> s_WallN;
    [SerializeField]
    private List<Sprite> s_WallS;
    [SerializeField]
    private List<Sprite> s_WallE;
    [SerializeField]
    private List<Sprite> s_WallW;

    [SerializeField]
    private List<Sprite> s_EntryN;
    [SerializeField]
    private List<Sprite> s_EntryS;
    [SerializeField]
    private List<Sprite> s_EntryE;
    [SerializeField]
    private List<Sprite> s_EntryW;

    [SerializeField]
    private List<Sprite> s_WindowN;
    [SerializeField]
    private List<Sprite> s_WindowS;
    [SerializeField]
    private List<Sprite> s_WindowE;
    [SerializeField]
    private List<Sprite> s_WindowW;

    [SerializeField]
    private List<Sprite> s_PropN;
    [SerializeField]
    private List<Sprite> s_PropS;
    [SerializeField]
    private List<Sprite> s_PropE;
    [SerializeField]
    private List<Sprite> s_PropW;
  
    [SerializeField]
    private MG_UItools uiTools;

    public void IcoFor2D()
    {
        ChangeUIicons(0);
    }
    public void IcoForIso()
    {
        ChangeUIicons(1);
    }

    private void ChangeUIicons(int i)
    {
        uiTools.Img_WallN.sprite = s_WallN[i];
        uiTools.Img_WallS.sprite = s_WallS[i];
        uiTools.Img_WallE.sprite = s_WallE[i];
        uiTools.Img_WallW.sprite = s_WallW[i];

        uiTools.Img_DoorN.sprite = s_EntryN[i];
        uiTools.Img_DoorS.sprite = s_EntryS[i];
        uiTools.Img_DoorE.sprite = s_EntryE[i];
        uiTools.Img_DoorW.sprite = s_EntryW[i];

        uiTools.Img_WindowN.sprite = s_WindowN[i];
        uiTools.Img_WindowS.sprite = s_WindowS[i];
        uiTools.Img_WindowE.sprite = s_WindowE[i];
        uiTools.Img_WindowW.sprite = s_WindowW[i];

        uiTools.Img_PropN.sprite = s_PropN[i];
        uiTools.Img_PropS.sprite = s_PropS[i];
        uiTools.Img_PropE.sprite = s_PropE[i];
        uiTools.Img_PropW.sprite = s_PropW[i];
    }

}
