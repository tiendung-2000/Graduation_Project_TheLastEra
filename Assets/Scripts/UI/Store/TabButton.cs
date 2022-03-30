using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TabButton : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image backGround;

    public UnityEvent onTabSelected, onTabDeselected;

    public TabGroup tabGroup;

    void Start() {
        tabGroup.AddButton(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit();
    }

    public void Select() {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }
    public void Deselected() {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }
}
