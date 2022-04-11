using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite icon;
    public int price;
    public ItemType itemType;
    public enum ItemType
    {
        health,
        mana
    }

    public virtual void Used()
    {

    }
}
