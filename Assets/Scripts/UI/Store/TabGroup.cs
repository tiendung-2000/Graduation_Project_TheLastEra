using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public Sprite tabHover, tabActive, tabIdle;
    public List<TabButton> listTabButtons;
    public List<GameObject> listSwap;

    public TabButton selectedTab;

    public void AddButton(TabButton button) {
        if (listTabButtons == null)
            listTabButtons = new List<TabButton>();

        listTabButtons.Add(button);

        if (listTabButtons.Count > 1)
        {
            
            listTabButtons.Sort(delegate (TabButton x, TabButton y) {
                return x.transform.GetSiblingIndex() < y.transform.GetSiblingIndex() ? -1 : 1;
            });

            listTabButtons[0].Select();
            OnTabSelected(listTabButtons[0]);
        }     
    }

    public void OnTabEnter(TabButton button) {
        ResetTab();
        if (selectedTab == null || button != selectedTab)
        {
            button.backGround.sprite = tabHover;
        }


    }

    public void OnTabSelected(TabButton button) {
        if (selectedTab != null)
            selectedTab.Deselected();


        selectedTab = button;
        selectedTab.Select();
        ResetTab();

        button.backGround.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < listSwap.Count; i++)
        {
            if (i == index)
                listSwap[i].SetActive(true);
            else listSwap[i].SetActive(false);
        }
    }

    public void OnTabExit() {
        ResetTab();
    }

    public void ResetTab() {
        foreach (TabButton button in listTabButtons)
        {
            if (selectedTab != null && button == selectedTab)
                continue;
            button.backGround.sprite = tabIdle;
        }
    }
}
