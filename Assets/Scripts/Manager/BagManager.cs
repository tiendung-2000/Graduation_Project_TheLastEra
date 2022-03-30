using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public static BagManager instance;
    public List<SlotBag> slotBags;
    public GameObject slotBag;
    public int countSlotBag;
    public Transform bagParentTransform;

    #region singleton
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    #endregion

    private void Start()
    {
        CreateSlot();
    }

    void CreateSlot() {
        for (int i = 0; i <= countSlotBag; i++)
        {
            GameObject slotCreate = Instantiate(
                slotBag, bagParentTransform.position, Quaternion.identity, bagParentTransform
            );

            AddSlot(slotCreate.GetComponent<SlotBag>());
        }
    }

    void AddSlot(SlotBag slot) {
        slotBags.Add(slot);
    }


    public void AddItem(Item item, int itemCount) {
        int indexCurrentSlot = slotBags.FindIndex(slot => slot.GetItem() == item);

        if (indexCurrentSlot != -1)
        {
            slotBags[indexCurrentSlot].SetItem(item);
            slotBags[indexCurrentSlot].SetCount(itemCount);
            return;
        }

        int indexNullSlot = slotBags.FindIndex(slot => slot.GetItem() == null);
        slotBags[indexNullSlot].SetItem(item);
        slotBags[indexNullSlot].SetCount(itemCount);
    }
}
