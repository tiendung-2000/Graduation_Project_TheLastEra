using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/New Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public int price;
    public float health;
    public float mana;
    public float damageUpgrade;
    public float healthUprade;
}
