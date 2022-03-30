using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotBag : MonoBehaviour
{
    private Item item;
    private int count;

    BagManager bagController;

    public TextMeshProUGUI textCount;
    public Image icon;
    public Button clearButton;

    private void Start()
    {
        bagController = BagManager.instance;
        clearButton.onClick.AddListener(ClearButton);
    }

    void ClearButton() {
        SetCount(-count);
        item = null;
    }

    public Item GetItem() { return item; }
    public int GetCount() { return count; }
    public void SetItem(Item _item) { 
        item = _item;
        icon.sprite = item.icon;
    }
    public void SetCount(int _count) { 
        count += _count;
        if (count > 99) count = 99;
        if (count <= 0) Active(false, 0);
        else Active(true, 255);
        textCount.text = count.ToString();
    }

    public void Active(bool active, float aColor) {
        textCount.gameObject.SetActive(active);
        clearButton.gameObject.SetActive(active);
        icon.color = new Color(255, 255, 255, aColor);
    }
}
