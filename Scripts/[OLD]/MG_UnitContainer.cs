using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class MG_UnitContainer : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject _pref_Unit;
//    [SerializeField]
//    private Image ico;

//    [SerializeField]
//    private MG_Editor editor;//[R] Настройки редактора

//    private void Awake()
//    {
//        if (editor == null)
//            Debug.Log("<color=red>MG_UnitContainer Awake(): MG_Editor не прикреплен!</color>");
//    }

//    public void Create(MG_Tile _mgtile)
//    {
//        GameObject _newUnit = Instantiate(_pref_Unit);

//        MG_Unit.MainHero = _newUnit.GetComponent<MG_Unit>();
//        editor.SetEditorMode(EditorMode.UnitControl);
//        ico.gameObject.SetActive(true);

//        _newUnit.transform.parent = this.transform;        
//        _newUnit.GetComponent<MG_Unit>().MGtile = _mgtile;
//        _newUnit.GetComponent<MG_Unit>().GameObj = _newUnit;
//        _newUnit.transform.position = new Vector3 (_mgtile.WorldLocation.x, _mgtile.WorldLocation.y + 0.225f, _mgtile.WorldLocation.z);

//        //---------_newUnit.GetComponent<MG_Unit>().ShowWalkableArea();
//    }

//    public void Clear()
//    {
//        MG_Unit.MainHero = null;
//        if (transform.childCount > 0)
//        {
//            foreach (Transform child in transform)
//            {
//                Destroy(child.gameObject);
//            }
//        }
//    }
//}
