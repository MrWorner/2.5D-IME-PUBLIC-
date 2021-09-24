using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MG_WinI_Prop : MonoBehaviour
{
    protected string id;//Id окна (название)
    [SerializeField]
    protected string propertyName;//имя свойства в данном объекте
    protected MG_Property property;//свойство

    [SerializeField]
    protected Image icon;//[R]иконка
    [SerializeField]
    protected Image background;//[R] задний фон
    [SerializeField]
    protected TextMeshProUGUI Text;//[R] заголовок свойства

    [SerializeField]
    protected MGWN_PropertyViewer win_PropViewer;//[R] Окно выбора свойств 

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init(MG_Property _prop, Sprite _icon, string _id, MGWN_PropertyViewer _win_PropViewer)
    {
        id = _id;//задаем имя
        SetAllAttrs(_prop, _icon);
        win_PropViewer = _win_PropViewer;    
        if (win_PropViewer == null)
            Debug.Log("<color=red>" + id + " Init(): MG_Win_PropViewer не прикреплен!</color>");
        if (icon == null)
            Debug.Log("<color=red>" + id + " Init(): объект для icon не прикреплен!</color>");
        if (background == null)
            Debug.Log("<color=red>" + id + " Init(): объект для background не прикреплен!</color>");
        if (Text == null)
            Debug.Log("<color=red>" + id + " Init(): объект для Text не прикреплен!</color>");
    }

    /// <summary>
    /// Изменить цвет бэкграунда
    /// </summary>
    /// <param name="_chosen"></param>
    public void SetChosen(bool _chosen)
    {
        if (_chosen)
        {
            background.color = Color.green;
            GetComponent<Button>().interactable = false;//выключить интеракцию. Чтобы двойной клик не поменял на серый цвет
        }            
        else
        {
            background.color = new Color(0F, 0F, 0F, 1.00f);
            GetComponent<Button>().interactable = true;//включить интеракцию
        }         
    }

    /// <summary>
    /// Установить свойство
    /// </summary>
    /// <param name="_prop"></param>
    /// <param name="_icon"></param>
    protected void SetAllAttrs(MG_Property _prop, Sprite _icon)
    {
        property = _prop;
        propertyName = _prop.Name;
        icon.sprite = _icon;
        Text.text = _prop.Name;
    }

    /// <summary>
    /// Совершен клик на данное свойство. Нужно сообщить также самому окну
    /// </summary>
    public void Click()
    {
        SetChosen(true);//задаем что выбран данное свойство
        win_PropViewer.SetChosenItem(this);      
    }

    /// <summary>
    /// Получить имя
    /// </summary>
    /// <returns></returns>
    public string GetId()
    {
        return id;
    }

    /// <summary>
    /// Получить свойство
    /// </summary>
    /// <returns></returns>
    public MG_Property GetProperty()
    {
        return property;
    }

    /// <summary>
    /// Установить видимость
    /// </summary>
    /// <param name="_isVisible"></param>
    public void SetVisible(bool _isVisible)
    {
        this.gameObject.SetActive(_isVisible);
    }

}
