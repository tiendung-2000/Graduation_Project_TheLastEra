using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotStore : SlotUI
{
    public Item myItem;
    public int price;
    public TextMeshProUGUI priceText;

    BagManager bagController;
    BudgetManager budget;

    private void Start()
    {
        if (myItem != null)
        {
            FirstSetUp();
        }
        bagController = BagManager.instance;
        budget = BudgetManager.instance;
    }

    void FirstSetUp() {
        icon.sprite = myItem.icon;
        price = myItem.price;
        priceText.text = price.ToString()+"$";
    }

    public override void OnClickSlot() {
        if (CheckCoin(myItem.price))
        {
            Debug.Log("Add item to bag controller!");
            budget.m_coin = -myItem.price;
            bagController.AddItem(myItem, 1);
        }
        else {
            Debug.Log("Not enough moneys!");
        }
    }

    public bool CheckCoin(int price) {
        if (budget.m_coin >= price)
            return true;
        else return false;
    }
}
