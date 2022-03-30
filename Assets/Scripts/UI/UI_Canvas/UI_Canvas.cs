using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Canvas : MonoBehaviour
{
    public UIName uiName;

    public virtual void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        UI_Manager.instance.AddUICanvas(this);
        OnClose();
    }

    public virtual void OnOpen()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnClose()
    {
        gameObject.SetActive(false);
    }
}
