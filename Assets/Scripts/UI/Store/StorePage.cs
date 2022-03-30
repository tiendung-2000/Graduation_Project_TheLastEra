using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePage : MonoBehaviour
{
    public List<Item> items;

    public GameObject slotStore;

    public Transform slotParent;

    private void Start()
    {
        CreateSlot();
    }

    void CreateSlot() {
        for (int i = 0; i < items.Count; i++)
        {
            SlotStore slot = slotStore.GetComponent<SlotStore>();
            slot.myItem = items[i];
            Instantiate(slotStore, slotParent.position, Quaternion.identity, slotParent);
        }
    }
}
